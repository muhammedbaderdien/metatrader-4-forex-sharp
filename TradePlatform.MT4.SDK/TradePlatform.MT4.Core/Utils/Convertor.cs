﻿using System;
using System.Diagnostics;
using TradePlatform.MT4.Engine.Log;

namespace TradePlatform.MT4.Core.Utils
{
  public static class Convertor
  {
    public static int ToInt(string value)
    {
      try
      {
        return int.Parse(value);
      }
      catch (Exception ex)
      {
        Trace.Write((object) new LogInfo(LogType.Execption, ex, "Value cannot be parsed: " + value));
        throw;
      }
    }

    public static double ToDouble(string value)
    {
      try
      {
        return double.Parse(value);
      }
      catch (Exception ex)
      {
        Trace.Write((object) new LogInfo(LogType.Execption, ex, "Value cannot be parsed: " + value));
        throw;
      }
    }

    public static DateTime ToDateTime(string value)
    {
      try
      {
        return DateTime.Parse(value);
      }
      catch (Exception ex)
      {
        Trace.Write((object) new LogInfo(LogType.Execption, ex, "Value cannot be parsed: " + value));
        throw;
      }
    }

    public static bool ToBoolean(string value)
    {
      try
      {
        return value == "1";
      }
      catch (Exception ex)
      {
        Trace.Write((object) new LogInfo(LogType.Execption, ex, "Value cannot be parsed: " + value));
        throw;
      }
    }
  }
}
