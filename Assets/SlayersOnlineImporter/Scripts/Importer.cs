using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Importer : MonoBehaviour
{
    Project project;

    void Start() {
        ReadProject("Assets/SlayersOnlineImporter/Files/test2.prj");
        Debug.Log(project.maps[0].name + ", " + project.maps[0].chipset + ", " + project.maps[0].width + ", " + project.maps[0].height);
    }

    void ReadMaps() {
        Map[] maps = project.maps;
        
        for(int i = 0; i < maps.Length; i++) {
            StreamReader streamReader = new StreamReader("Assets/SlayersOnlineImporter/Files/" + maps[i].name + ".map");
            for(int x = 0; x < maps[i].width; x++) {
                for(int y = 0; y < maps[i].height; y++) {
                    while(!streamReader.EndOfStream) {
                        int xBasse = int.Parse(streamReader.ReadLine());
                        int xHaute = int.Parse(streamReader.ReadLine());
                        int yBasse = int.Parse(streamReader.ReadLine());
                        int yHaute = int.Parse(streamReader.ReadLine());

                        maps[i].SetLow(0, 0, xBasse, yBasse);
                        maps[i].SetHigh(0, 0, xHaute, yHaute);
                    }
                }
            }
            streamReader.Close();
        }
    }

    void ReadProject(string name) {
        int nbMaps = (int)new System.IO.FileInfo(name).Length / 777;
        project = new Project(nbMaps);

        StreamReader streamReader = new StreamReader(name);
        
        for(int i = 0; i < nbMaps; i++) {
            int pos = 0;
            byte nameLength = (byte)streamReader.Read();
            pos++;
            string mapName = "";
            string chipset = "";
            
            // Nom de  la map
            for(int j = 0; j < nameLength; j++) {
                mapName += (char)streamReader.Read();
                pos++;
            }
            for(; pos < 155; pos++) {
                streamReader.Read();
            }

            // Nom du chipset
            byte chipsetLength = (byte)streamReader.Read();
            pos++;
            for(int j = 0; j < chipsetLength; j++) {
                chipset += (char)streamReader.Read();
                pos++;
            }
            Debug.Log(pos);
            for(; pos < 206; pos++) {
                streamReader.Read();
            }

            //Taille
            int width = streamReader.Read();
            pos++;
            streamReader.Read();
            pos++;
            int height = streamReader.Read();
            pos++;

            for(; pos < 777; pos++) {
                streamReader.Read();
            }

            project.maps[i] = new Map(mapName, chipset, width, height);
        }

        streamReader.Close();
    }
}
