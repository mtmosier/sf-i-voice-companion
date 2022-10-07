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
    private int totalCodexItemsFound;
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
        totalCodexItemsFound = 0;

        LoadStarSystemData();
        LoadOrganizationData();

        VA.SetInt(">>codexDescriptionsCombined.len", totalCodexItemsFound);
        // VA.WriteToLog("Codex items found " + VA.GetInt(">>codexDescriptionsCombined.len").ToString(), "Red");
    }





    private string GetDisplayLocation(Dictionary<String, Object> location)
    {
        int realSysId = (int)location["starSystemID"] + 1;
        return systemPrefixLookup[realSysId.ToString()] + "-" + location["x"].ToString() + "-" + location["y"].ToString();
    }

    private void LoadOrganizationData()
    {
        int i = 0;
        List<string> codexOrgList = new List<string>();

        WebRequest request = WebRequest.Create("https://www.benoldinggames.co.uk/sfi/gamedata/files/races.jsonp");
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        Stream dataStream = response.GetResponseStream();
        StreamReader reader = new StreamReader(dataStream);

        string json = reader.ReadToEnd();

        reader.Close();
        dataStream.Close();
        response.Close();

        string jsonpRegexStr = ".*?\\(\\s*([\"']).*?\\1\\s*,\\s*(.*)\\)";
        Regex jsonpRegex = new Regex(jsonpRegexStr, RegexOptions.IgnoreCase);

        if (!string.IsNullOrEmpty(json)) {
            Match m = jsonpRegex.Match(json);
            if (m.Success) {
                json = m.Groups[2].ToString();

                List<Dictionary<string, object>> orgList = new JavaScriptSerializer().Deserialize<List<Dictionary<string, object>>>(json);
                foreach (Dictionary<string, object> orgInfo in orgList) {
                    string raceName = orgInfo["name"].ToString();
                    string desc = orgInfo["info"].ToString();

                    if ((int)orgInfo["race"] < 2 && string.IsNullOrEmpty(desc))
                        continue;

                    if (string.IsNullOrEmpty(desc))
                        desc = "No Information Available";
                    desc = raceName + ", " + desc;

                    if (!codexOrgList.Contains(raceName))
                        codexOrgList.Add(raceName);

                    VA.SetText(">>codexOrgDescription[" + i.ToString() + "]", desc);
                    VA.SetText(">>codexOrgNameLookup[" + raceName + "]", i.ToString());

                    string plural = orgInfo["plural"].ToString();
                    if (string.IsNullOrEmpty(plural))
                        plural = raceName + "s";
                    VA.SetText(">>codexOrgNameLookup[" + plural + "]", i.ToString());
                    if (!codexOrgList.Contains(plural))
                        codexOrgList.Add(plural);

                    if (raceName.IndexOf("Ghost") > 0) {
                        //*** This will actually happen twice, once for each ghost race.
                        //*** But it doesn't matter, their descriptions are the same.
                        VA.SetText(">>codexOrgNameLookup[Ghost]", i.ToString());
                        if (!codexOrgList.Contains("Ghost"))
                            codexOrgList.Add("Ghost");
                    }

                    i++;
                    totalCodexItemsFound++;
                }
            }
        }


        WebRequest orgRequest = WebRequest.Create("https://www.benoldinggames.co.uk/sfi/gamedata/files/orgs.jsonp");
        HttpWebResponse orgResponse = (HttpWebResponse)orgRequest.GetResponse();
        Stream orgDataStream = orgResponse.GetResponseStream();
        StreamReader orgReader = new StreamReader(orgDataStream);

        json = orgReader.ReadToEnd();

        orgReader.Close();
        orgDataStream.Close();
        orgResponse.Close();


        if (!string.IsNullOrEmpty(json)) {
            Match m = jsonpRegex.Match(json);
            if (m.Success) {
                json = m.Groups[2].ToString();

                List<Dictionary<string, object>> orgList = new JavaScriptSerializer().Deserialize<List<Dictionary<string, object>>>(json);
                foreach (Dictionary<string, object> orgInfo in orgList) {
                    string raceName = orgInfo["name"].ToString();
                    string desc = orgInfo["intro"].ToString();

                    if (((int)orgInfo["race"] > 1 || raceName == "Nobody") && string.IsNullOrEmpty(desc))
                        continue;

                    if (string.IsNullOrEmpty(desc))
                        desc = "No Information Available";
                    desc = raceName + ", " + desc;

                    if (!codexOrgList.Contains(raceName))
                        codexOrgList.Add(raceName);

                    VA.SetText(">>codexOrgDescription[" + i.ToString() + "]", desc);
                    VA.SetText(">>codexOrgNameLookup[" + raceName + "]", i.ToString());

                    i++;
                    totalCodexItemsFound++;
                }
            }
        }


        VA.SetInt(">>codexOrgDescription.len", i);
        VA.SetText(">>codexOrgList", string.Join<string>(";", codexOrgList));

        // VA.WriteToLog("Orgs found " + VA.GetInt(">>codexOrgDescription.len").ToString(), "Red");
    }


    private void LoadStarSystemData()
    {
        WebRequest request = WebRequest.Create("https://www.benoldinggames.co.uk/sfi/gamedata/files/systems.jsonp");
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        Stream dataStream = response.GetResponseStream();
        StreamReader reader = new StreamReader(dataStream);
        int i = 0;

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


            List<string> codexPlanetList = new List<string>();
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

                    if (!codexPlanetList.Contains(name))
                        codexPlanetList.Add(name);

                    VA.SetText(">>codexPlanetDescription[" + i.ToString() + "]", desc);
                    VA.SetText(">>codexPlanetNameLookup[" + name + "]", i.ToString());
                    VA.SetText(">>codexPlanetSectorLookup[" + GetDisplayLocation((Dictionary<string, Object>)planetInfo["location"]) + "]", i.ToString());
                    i++;
                    totalCodexItemsFound++;
                }
            }
            VA.SetInt(">>codexPlanetDescription.len", i);
            VA.SetText(">>codexPlanetList", string.Join<string>(";", codexPlanetList));

            List<string> codexObjectList = new List<string>();
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
                    if (String.IsNullOrEmpty(desc))  desc = name;
                    else  desc = name + ", " + desc;

                    if (!codexObjectList.Contains(name))
                        codexObjectList.Add(name);

                    VA.SetText(">>codexObjectDescription[" + i.ToString() + "]", desc);
                    VA.SetText(">>codexObjectNameLookup[" + name + "]", i.ToString());
                    VA.SetText(">>codexObjectSectorLookup[" + GetDisplayLocation((Dictionary<string, Object>)objectInfo["location"]) + "]", i.ToString());
                    i++;
                    totalCodexItemsFound++;
                }
            }
            VA.SetInt(">>codexObjectDescription.len", i);
            VA.SetText(">>codexObjectList", string.Join<string>(";", codexObjectList));

            // VA.WriteToLog("Planets found " + VA.GetInt(">>codexPlanetDescription.len").ToString(), "Red");
            // VA.WriteToLog("Objects found " + VA.GetInt(">>codexObjectDescription.len").ToString(), "Red");
        }
        reader.Close();
        dataStream.Close();
        response.Close();
    }
}
