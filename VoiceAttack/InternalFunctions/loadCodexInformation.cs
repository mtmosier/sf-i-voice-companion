//***  System.Web.Extensions.dll;System.Linq.dll;System.Core.dll;System.Text.RegularExpressions.dll;System.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;
using System.Text.RegularExpressions;
using System.Globalization;

public class VAInline
{
    private Dictionary<string, string> systemPrefixLookup;

    private Dictionary<int, string> orgNameDataLookup;
    private Dictionary<int, Dictionary<string, Object>> objectLocationDataLookup;
    private Dictionary<int, Dictionary<string, Object>> planetLocationDataLookup;


    public void main()
    {
        systemPrefixLookup = new Dictionary<string, string>();
        orgNameDataLookup = new Dictionary<int, string>();
        objectLocationDataLookup = new Dictionary<int, Dictionary<string, Object>>();
        planetLocationDataLookup = new Dictionary<int, Dictionary<string, Object>>();

        LoadStarSystemData();
    }





    private string GetDisplayLocation(Dictionary<String, Object> location)
    {
        int realSysId = (int)location["starSystemID"] + 1;
        return systemPrefixLookup[realSysId.ToString()] + "-" + location["x"].ToString() + "-" + location["y"].ToString();
    }

    private void LoadOrganizationData()
    {
    }

    private void LoadStarSystemData()
    {
        WebRequest request = WebRequest.Create("https://www.benoldinggames.co.uk/sfi/gamedata/files/systems.jsonp");
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        Stream dataStream = response.GetResponseStream();
        StreamReader reader = new StreamReader(dataStream);
        int i;

        string json = reader.ReadToEnd();
        if (!string.IsNullOrEmpty(json)) {
            bool matchesFound;

            //*** This is horrible practice, but I'm chopping up the json in to chunks to make it easier to deserialize
            //*** Changes to fields in the system object in the game data would break this section
            string planetRegexStr = ",\"planets\":" + @"(\[\{.*?\}\}\])," + '"';
            Regex planetRegex = new Regex(planetRegexStr, RegexOptions.IgnoreCase);

            string objectRegexStr = ",\"otherObjects\":" + @"(\[\{.*?\}\])," + '"';
            Regex objectRegex = new Regex(objectRegexStr, RegexOptions.IgnoreCase);

            string sysPrefixRegexStr = "\"capitalTeam\":\\d+,\"prefix\":\"(\\w+)\"";
            Regex sysPrefixRegex = new Regex(sysPrefixRegexStr, RegexOptions.IgnoreCase);

            string sysIdRegexStr = "\"id\":(\\d+),\"whiteHole\":";
            Regex sysIdRegex = new Regex(sysIdRegexStr, RegexOptions.IgnoreCase);

            List<string> sysPrefixList = new List<string>();
            foreach (Match match in sysPrefixRegex.Matches(json)) {
                sysPrefixList.Add(match.Groups[1].ToString());
            }

            i = 0;
            foreach (Match match in sysIdRegex.Matches(json)) {
                systemPrefixLookup.Add(match.Groups[1].ToString(), sysPrefixList[i]);
                i++;
            }


            i = 0;
            foreach (Match match in planetRegex.Matches(json)) {
                List<Dictionary<string, object>> planetList = new JavaScriptSerializer().Deserialize<List<Dictionary<string, object>>>(match.Groups[1].ToString());
                foreach (Dictionary<string, object> planetInfo in planetList) {
                    string name = planetInfo["name"].ToString();
                    string desc = null;
                    try {
                        desc = planetInfo["info"].ToString();
                    } catch (KeyNotFoundException) {}
                    try {
                        if (String.IsNullOrEmpty(desc))  desc = planetInfo["scanText"].ToString();
                    } catch (KeyNotFoundException) {}
                    if (String.IsNullOrEmpty(desc))  desc = name;
                    else  desc = name + ", " + desc;

                    // planetLocationDataLookup.Add(i, (Dictionary<string, Object>)planetInfo["location"]);
                    VA.SetText(">>codexPlanetDescription[" + i.ToString() + "]", desc);
                    VA.SetText(">>codexPlanetNameLookup[" + name + "]", i.ToString());
                    VA.SetText(">>codexPlanetSectorLookup[" + GetDisplayLocation((Dictionary<string, Object>)planetInfo["location"]) + "]", i.ToString());
                    i++;
                }
            }
            VA.SetInt(">>codexPlanetDescription.len", i);

            i = 0;
            foreach (Match match in objectRegex.Matches(json)) {
                List<Dictionary<string, object>> objectList = new JavaScriptSerializer().Deserialize<List<Dictionary<string, object>>>(match.Groups[1].ToString());
                foreach (Dictionary<string, object> objectInfo in objectList) {
                    string name = objectInfo["name"].ToString();
                    string desc = null;
                    try {
                        desc = objectInfo["info"].ToString();
                    } catch (KeyNotFoundException) {}
                    try {
                        if (String.IsNullOrEmpty(desc))  desc = objectInfo["scanText"].ToString();
                    } catch (KeyNotFoundException) {}
                    if (String.IsNullOrEmpty(desc))  desc = objectInfo["name"].ToString();
                    else  desc = objectInfo["name"].ToString() + ", " + desc;

                    // objectLocationDataLookup.Add(i, (Dictionary<string, Object>)objectInfo["location"]);
                    VA.SetText(">>codexObjectDescription[" + i.ToString() + "]", desc);
                    VA.SetText(">>codexObjectNameLookup[" + name + "]", i.ToString());
                    VA.SetText(">>codexObjectSectorLookup[" + GetDisplayLocation((Dictionary<string, Object>)objectInfo["location"]) + "]", i.ToString());
                    i++;
                }
            }
            VA.SetInt(">>codexObjectDescription.len", i);

            VA.WriteToLog("Planets found " + VA.GetInt(">>codexPlanetDescription.len").ToString(), "Red");
            VA.WriteToLog("Objects found " + VA.GetInt(">>codexObjectDescription.len").ToString(), "Red");
        }
        reader.Close();
        dataStream.Close();
        response.Close();
    }
}
