﻿#region License

/*
    Copyright [2011] [Jeffrey Cameron]

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
*/

#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Pickles.Parser;

namespace Pickles.Formatters
{
    public class HtmlScenarioOutlineFormatter
    {
        private readonly XNamespace xmlns;
        private readonly HtmlStepFormatter htmlStepFormatter;
        private readonly HtmlDescriptionFormatter htmlDescriptionFormatter;
        private readonly HtmlTableFormatter htmlTableFormatter;

        public HtmlScenarioOutlineFormatter(HtmlStepFormatter htmlStepFormatter, HtmlDescriptionFormatter htmlDescriptionFormatter, HtmlTableFormatter htmlTableFormatter)
        {
            this.htmlStepFormatter = htmlStepFormatter;
            this.htmlDescriptionFormatter = htmlDescriptionFormatter;
            this.htmlTableFormatter = htmlTableFormatter;
            this.xmlns = XNamespace.Get("http://www.w3.org/1999/xhtml");
        }

        public XElement Format(ScenarioOutline scenario, int id)
        {
            return new XElement(xmlns + "li",
                       new XAttribute("id", id),
                       new XAttribute("class", "scenario"),
                       new XElement(xmlns + "div",
                           new XAttribute("class", "scenario-heading"),
                           new XElement(xmlns + "h2", scenario.Name),
                           this.htmlDescriptionFormatter.Format(scenario.Description)
                       ),
                       new XElement(xmlns + "div",
                           new XAttribute("class", "steps"),
                           new XElement(xmlns + "ul", scenario.Steps.Select(step => this.htmlStepFormatter.Format(step)))
                       ),
                       new XElement(xmlns + "div",
                           new XAttribute("class", "examples"),
                           new XElement(xmlns + "h3",  "Examples"),
                           this.htmlDescriptionFormatter.Format(scenario.Example.Description),
                           this.htmlTableFormatter.Format(scenario.Example.TableArgument)
                       )
                   );
        }
    }
}