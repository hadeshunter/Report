using ModelClass.model.lang.impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelClass.model.lang
{
    public interface ITranslate
    {
        Item LoadJson(string lang);
        ItemLogin loadJsonLogin(string lang);
    }
}
