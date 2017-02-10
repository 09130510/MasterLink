using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PriceLib.Capital
{
    public class PageCollection
    {
        #region Variable
        private List<SubPage> m_Pages = new List<SubPage>();
        #endregion

        #region Property
        public List<SubPage> Pages { get { return m_Pages; } }
        #endregion

        #region Public
        public SubPage GetSubPage(string substr)
        {
            //訂過了
            foreach (var page in m_Pages)
            {
                if (page.Contains(substr))
                {
                    page.Add(substr);
                    return page;
                }
            }
            //沒訂過, 找空的page
            var unfilled = m_Pages.FirstOrDefault(e => e.Counts < 100);
            if (unfilled != null)
            {
                unfilled.Add(substr);
                return unfilled;
            }
            else
            {
                //沒有空Page, 建一個
                SubPage sub = new SubPage();
                sub.Add(substr);
                m_Pages.Add(sub);
                return sub;
            }
        }
        public SubPage GetUnsubPage(string substr)
        {
            foreach (var page in m_Pages)
            {
                if (page.Contains(substr))
                {
                    page.Remove(substr);
                    return page;
                }
            }

            return null;
        }
        #endregion

    }
}