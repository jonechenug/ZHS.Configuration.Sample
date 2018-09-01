using System;

namespace ZHS.Configuration.Core
{
    public interface IConfigurationGeter
    {
        TConfig Get<TConfig>(string key);
        TConfig Get<TConfig>();
        String this[string key] { get;}
    }
}