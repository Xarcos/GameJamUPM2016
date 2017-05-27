using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
public struct score
{
    public string name;
    public long points;
}
public class LeaderBoardManager : MonoBehaviour
{
    public const int maxScores = 10;
    public const string fileName = "Scores.xml";

    public static void saveNewScore(long score, string name)
    {
        string fullFilePath = Path.Combine(Application.persistentDataPath, fileName);
        XmlDocument xmlDoc = new XmlDocument();
        
        if (!File.Exists(fullFilePath))
        {
            createDefaultScores();
        }
        xmlDoc.Load(fullFilePath);
        XmlElement root = xmlDoc.DocumentElement;

        XmlNodeList allScoresList = root.GetElementsByTagName("UserScore");

        XmlNode newNode = xmlDoc.CreateElement("Scores");

        string lastNameFound = "";
        string lastScoreFound = "";
        string thisNodeName = "";
        string thisNodeScore = "";
        bool minfound = false;
        foreach (XmlNode currentNode in allScoresList)
        {
            if (!minfound && long.Parse(currentNode.Attributes["Score"].Value) < score)
            {
                lastNameFound = currentNode.Attributes["Name"].Value;
                lastScoreFound = currentNode.Attributes["Score"].Value;
                currentNode.Attributes["Name"].Value = name;
                currentNode.Attributes["Score"].Value = score.ToString();
                minfound=true;
            }
            else if(minfound)
            {
                thisNodeName = currentNode.Attributes["Name"].Value;
                thisNodeScore = currentNode.Attributes["Score"].Value;

                currentNode.Attributes["Name"].Value = lastNameFound;
                currentNode.Attributes["Score"].Value = lastScoreFound;

                lastNameFound = thisNodeName;
                lastScoreFound = thisNodeScore;
            }
        }
            xmlDoc.Save(fullFilePath);
    }

    public static score[] getOrderedScores() {
        List<score> records = new List<score>();
        string fullFilePath = Path.Combine(Application.persistentDataPath, fileName);
        XmlDocument xmlDoc = new XmlDocument();

        if (!File.Exists(fullFilePath))
        {
            createDefaultScores();
        }
        xmlDoc.Load(fullFilePath);
        XmlElement root = xmlDoc.DocumentElement;

        XmlNodeList allScoresList = root.GetElementsByTagName("UserScore");

        XmlNode newNode = xmlDoc.CreateElement("Scores");

        foreach (XmlNode currentNode in allScoresList) {
            score currentScore = new score();
            currentScore.name= currentNode.Attributes["Name"].Value;
            currentScore.points = long.Parse(currentNode.Attributes["Score"].Value);
            records.Add(currentScore);
        }
        return records.ToArray();

    }
    public static void createDefaultScores()
    {
        string fullFilePath = Path.Combine(Application.persistentDataPath, fileName);
        XmlDocument xmlDoc = new XmlDocument();
        XmlElement root = xmlDoc.CreateElement("Scores");

        for (int i = 0; i < maxScores; ++i)
        {
            XmlNode standardUserScore = xmlDoc.CreateElement("UserScore");

            XmlAttribute standardNameAtt = xmlDoc.CreateAttribute("Name");
            standardNameAtt.Value = "Player";
            XmlAttribute standardScoreAtt = xmlDoc.CreateAttribute("Score");
            standardScoreAtt.Value = "000000";

            standardUserScore.Attributes.Append(standardNameAtt);
            standardUserScore.Attributes.Append(standardScoreAtt);

            root.AppendChild(standardUserScore);
        }
        xmlDoc.AppendChild(root);
        xmlDoc.Save(fullFilePath);
    }
    
}