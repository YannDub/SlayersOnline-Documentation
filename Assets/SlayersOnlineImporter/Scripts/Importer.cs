using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Tilemaps;
using UnityEditor;

[ExecuteInEditMode]
public class Importer : MonoBehaviour
{
    public string projectFile;

    Project project;
    Tilemap high;
    Tilemap low;

    void Start() {
        high = this.transform.Find("Haute").GetComponent<Tilemap>();
        low = this.transform.Find("Basse").GetComponent<Tilemap>();

        ReadProject(projectFile);
        ReadMaps();

        Render();
    }

    void ReadMaps() {
        Map[] maps = project.maps;
        string[] split = projectFile.Split('/');
        string folder = "";
        for(int i = 0; i < split.Length - 1; i++) {
            folder += split[i] + "/";
        }

        for(int i = 0; i < maps.Length; i++) {
            StreamReader streamReader = new StreamReader(folder + "Map/" + maps[i].name + ".map");
            for(int y = 0; y < maps[i].height; y++) {
                for(int x = 0; x < maps[i].width; x++) {
                    if(!streamReader.EndOfStream) {
                        int xBasse = int.Parse(streamReader.ReadLine());
                        int xHaute = int.Parse(streamReader.ReadLine());
                        int yBasse = int.Parse(streamReader.ReadLine());
                        int yHaute = int.Parse(streamReader.ReadLine());

                        maps[i].SetLow(x, y, xBasse, yBasse);
                        maps[i].SetHigh(x, y, xHaute, yHaute);
                    }
                }
            }
            streamReader.Close();
        }
    }

    void ReadProject(string name) {
        string[] split = projectFile.Split('/');
        string folder = "";
        for(int i = 0; i < split.Length - 1; i++) {
            folder += split[i] + "/";
        }

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
            chipset = folder + chipset;
            chipset = chipset.Replace('\\', '/');
            Debug.Log(chipset);
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

    void Render() {
        for(int i = 0; i < project.maps.Length; i++) {
            high.size = new Vector3Int(project.maps[i].width, project.maps[i].height, 0);
            low.size = new Vector3Int(project.maps[i].width, project.maps[i].height, 0);
            
            Object[] sprites = AssetDatabase.LoadAllAssetsAtPath(project.maps[i].chipset);
            string chipset = project.maps[i].chipset.Replace(".png", "");
            for(int x = 0; x < project.maps[i].width; x++) {
                for(int y = 0; y < project.maps[i].height; y++) {
                    Tile tile = new Tile();
                    int spriteNbr = (int) (project.maps[i].GetLow(x,y).x - 1 + (project.maps[i].GetLow(x,y).y - 1) * 30 );
                    tile.sprite = (Sprite) sprites[spriteNbr];

                    low.SetTile(new Vector3Int(x, y, 0), tile);
                }
            }
        }
    }
}
