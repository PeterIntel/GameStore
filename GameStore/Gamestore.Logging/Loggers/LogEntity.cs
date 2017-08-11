﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GameStore.Logging.Loggers
{
    public class LogEntity<TDomain>
    {
        public string CurrentEntity { set; get; }
        public string UpdatedEntity { set; get; }
        public Operation Operation { set; get; }

        public string EntityType { set; get; }

        public DateTime DataTimeLogging { set; get; }

        public LogEntity(TDomain currentEntity, TDomain updatedEntity)
        {
            CurrentEntity = JsonConvert.SerializeObject(currentEntity);
            UpdatedEntity = JsonConvert.SerializeObject(updatedEntity);
        }

        public LogEntity(TDomain currentEntity)
        {
            CurrentEntity = JsonConvert.SerializeObject(currentEntity);
        }
    }
}
