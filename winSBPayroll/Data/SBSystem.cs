﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace winSBPayroll.Forms
{
    public class SBSystem
    {
        public string Name { get; set; }
        public string Application { get; set; }
        public string Database { get; set; }
        public string Server { get; set; }
        public string AttachDB { get; set; }
        public string Metadata { get; set; }
        public string Version { get; set; }
        public bool Default { get; set; }

        public SBSystem(string name, string app, string database, string server, string attach, string metadata, string ver, bool def)
        {
            this.Name = name;
            this.Application = app;
            this.Database = database;
            this.Server = server;
            this.AttachDB = attach;
            this.Metadata = metadata;
            this.Version = ver;
            this.Default = def;
        }
    }

    public class SBSystem_Exp
    {
        public string Name { get; set; }
        public string Application { get; set; }

        public SBSystem_Exp(string name, string app)
        {
            this.Name = name;
            this.Application = app;
        }
    }

    public class SBSystem_DTO
    {
        public string Name { get; set; }
        public string Application { get; set; }

        public SBSystem_DTO(string name, string app)
        {
            this.Name = name;
            this.Application = app;
        }
    }

    public class SBSystem_DCP_DTO
    {
        public string Name { get; set; }
        public string Application { get; set; }
        public string Database { get; set; }
        public bool Defaultsn { get; set; }
        public bool Defaultun { get; set; }
        public bool Defaultpd { get; set; }

        public SBSystem_DCP_DTO(string name, string app, string database, bool defaultsn, bool defaultun, bool defaultpd)
        {
            this.Name = name;
            this.Application = app;
            this.Database = database;
            this.Defaultsn = defaultsn;
            this.Defaultun = defaultun;
            this.Defaultpd = defaultpd;
        }
    }

}
