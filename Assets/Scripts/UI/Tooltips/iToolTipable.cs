using System;
using System.Collections;
using System.Collections.Generic;
using Items;
using UnityEngine;

public interface iToolTipable
{
   public event Action<TooltipData> ToolTipDataChanged;
   public TooltipData GetTooltipData();
}

public class TooltipData
{
   public string header;
   public string content;
   public TooltipData(string header, string content)
   {
      this.header = header;
      this.content = content;
   }
   public TooltipData(ItemBase item)
   {
      if(item != null)
      {
         header = item.itemName;
         content = item.description;
      }
    
   }
}
