using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using winSBPayroll.Utilities;
using CommonLib;
using System.Text.RegularExpressions;

namespace winSBPayroll.Forms
{
    public partial class CalculatorForm : Form
    {
        public string TAG;
        public List<notificationdto> _lstnotificationdto = new List<notificationdto>();
        //Event declaration:
        //event for publishing messages to output
        public event EventHandler<notificationmessageEventArgs> _notificationmessageEventname;
        public event EventHandler<notificationmessageEventArgs> _notificationmessageEventname_from_parent;

        public CalculatorForm(EventHandler<notificationmessageEventArgs> notificationmessageEventname_from_parent)
        {
            InitializeComponent();

            TAG = this.GetType().Name;

            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhandledException);
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(ThreadException);

            //Subscribing to the event: 
            //Dynamically:
            //EventName += HandlerName;
            _notificationmessageEventname += notificationmessageHandler;
            _notificationmessageEventname_from_parent = notificationmessageEventname_from_parent;

            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("finished CalculatorForm initialization", TAG));

        }

        private void UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = (Exception)e.ExceptionObject;
            Log.Write_To_Log_File_temp_dir(ex);
            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
        }

        private void ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            Exception ex = e.Exception;
            Log.Write_To_Log_File_temp_dir(ex);
            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
        }

        //Event handler declaration:
        private void notificationmessageHandler(object sender, notificationmessageEventArgs args)
        {
            try
            {
                /* Handler logic */


                notificationdto _notificationdto = new notificationdto();

                DateTime currentDate = DateTime.Now;
                String dateTimenow = currentDate.ToString("dd-MM-yyyy HH:mm:ss tt");

                String _logtext = Environment.NewLine + "[ " + dateTimenow + " ]   " + args.message;

                _notificationdto._notification_message = _logtext;
                _notificationdto._created_datetime = dateTimenow;
                _notificationdto.TAG = args.TAG;

                _lstnotificationdto.Add(_notificationdto);

                Console.WriteLine(args.message);
                _notificationmessageEventname_from_parent.Invoke(this, new notificationmessageEventArgs(args.message, TAG));

                var _lstmsgdto = from msgdto in _lstnotificationdto
                                 orderby msgdto._created_datetime descending
                                 select msgdto._notification_message;

                String[] _logflippedlines = _lstmsgdto.ToArray();

                if (_logflippedlines.Length > 5000)
                {
                    _lstnotificationdto.Clear();
                }

                txtlog.Lines = _logflippedlines;
                txtlog.ScrollToCaret();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        private void CalculatorForm_Load(object sender, EventArgs e)
        {
            SetupCalculatorGrid();
            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("finished CalculatorForm load", TAG));

        }

        private void SetupCalculatorGrid()
        {
            // Button matrix configuration (5 rows x 4 columns)
            string[,] grid = new string[,]
            {
                { "C", "(", ")", "⌫" },
                { "7", "8", "9", "/" },
                { "4", "5", "6", "*" },
                { "1", "2", "3", "-" },
                { "0", ".", "=", "+" }
            };

            pnlButtons.Controls.Clear();

            for (int r = 0; r < 5; r++)
            {
                for (int c = 0; c < 4; c++)
                {
                    string btnText = grid[r, c];
                    Button btn = new Button();
                    btn.Text = btnText;
                    btn.Dock = DockStyle.Fill;
                    btn.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
                    btn.Margin = new Padding(2);

                    // Event handler wiring
                    btn.Click += new EventHandler(btncalculate_Click);

                    pnlButtons.Controls.Add(btn, c, r);
                }
            }
        }

        private void btncalculate_Click(object sender, EventArgs e)
        {
            lblResult.Text = string.Empty;

            Button btn = sender as Button;
            if (btn == null) return;

            string value = btn.Text;

            switch (value)
            {
                case "=":
                    // Commit final result into the input box for chaining further operations
                    ExecuteEquals();
                    break;

                case "C":
                    txtExpression.Clear();
                    lblResult.ForeColor = Color.Black;
                    lblResult.Text = "0";
                    break;

                case "⌫":
                    if (txtExpression.Text.Length > 0)
                    {
                        txtExpression.Text = txtExpression.Text.Substring(0, txtExpression.Text.Length - 1);
                        txtExpression.SelectionStart = txtExpression.Text.Length;
                    }
                    break;

                default:
                    // Append digit or operator to textbox
                    txtExpression.Text += value;
                    txtExpression.SelectionStart = txtExpression.Text.Length;
                    break;
            }
        }

        private void ExecuteEquals()
        {
            if (string.IsNullOrEmpty(txtExpression.Text)) return;

            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(txtExpression.Text, TAG));

            // 1. Call modular calculation engine
            // Evaluate the exact current expression
            CalculationResult result = CalculatorEngine.Evaluate(txtExpression.Text);

            // 2. Render response to UI based on status
            if (result.IsSuccess)
            {
                lblResult.ForeColor = Color.Black;
                lblResult.Text = string.Format("{0:N2}", result.Value);
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(string.Format("Result: {0:N2}", result.Value), TAG));
            }
            else
            {
                lblResult.ForeColor = Color.Red;
                //lblResult.Text = string.Format("Error: {0}", result.ErrorMessage);
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(string.Format("Result: {0:N2}", result.ErrorMessage), TAG));
            }
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtExpression_TextChanged(object sender, EventArgs e)
        {
            CalculateInstant();
            txtExpression.ScrollToCaret();
        }
        /// <summary>
        /// Evaluates the expression on the fly without altering the user's current input.
        /// </summary>
        private void CalculateInstant()
        {
            string expression = txtExpression.Text;

            // 1. Reset if input is empty
            if (string.IsNullOrEmpty(expression) || expression.Trim().Length == 0)
            {
                lblResult.ForeColor = Color.Black;
                lblResult.Text = "0";
                return;
            }

            // 2. Temporarily strip trailing dangling operators (e.g., "12 +" evaluates "12" live)
            string evaluatableExpr = Regex.Replace(expression.Trim(), @"[\+\-\*\/\.]+$", "");

            if (string.IsNullOrEmpty(evaluatableExpr))
            {
                lblResult.ForeColor = Color.Black;
                lblResult.Text = "0";
                return;
            }

            // 3. Compute live result
            CalculationResult result = CalculatorEngine.Evaluate(evaluatableExpr);

            if (result.IsSuccess)
            {
                lblResult.ForeColor = Color.DarkGreen;
                lblResult.Text = string.Format("{0:N2}", result.Value);
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(string.Format("Result: {0:N2}", result.Value), TAG));
            }
            else
            {
                // Soft status while typing incomplete terms (e.g., open parentheses)
                lblResult.ForeColor = Color.Gray;
                lblResult.Text = "...";
            }
        }
         
         


    }
}
