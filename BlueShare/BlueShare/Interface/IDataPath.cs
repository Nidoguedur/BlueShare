using System;
using System.Collections.Generic;
using System.Text;

namespace BlueShare.Interface
{
    public interface IDataPath
    {
        string GetPath(string datasetName);
    }
}
