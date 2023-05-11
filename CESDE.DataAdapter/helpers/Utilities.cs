namespace CESDE.DataAdapter.helpers
{
      public static class Utilities
      {
            public static string UpperFirstChar(this string input)
            {

                  if (string.IsNullOrEmpty(input))
                  {
                        return "";
                  }

                  input.ToLower();

                  char[] chars = input.ToCharArray();
                  chars[0] = char.ToUpper(chars[0]);
                  return new string(chars);
            }
      }
}
