using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace KRv1;

[SuppressMessage("ReSharper", "CommentTypo")]
public interface IAdditional //Интерфейс в котором есть список методов
{
    List<Func<string>> DelegateList();
}