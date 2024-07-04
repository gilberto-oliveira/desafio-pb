using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace DesafioPB.Common;

public class DesafioPBException : Exception
{
  public DesafioPBException(string message) : base(message)
  {  }

  public static void ThrowIfNullOrEmpty([NotNull] string? argument, [CallerArgumentExpression("argument")] string? paramName = null)
  {
    if (string.IsNullOrWhiteSpace(argument))
      throw new DesafioPBException($" O parametro '{paramName}' tem um valor invalido: '{argument}'");
  }
}