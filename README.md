# Slayers Online Importer

Slayers Online Importer est un projet permettant d'importer les maps du jeu dans Unity. Le projet est au début de son développement,
ce repo permet juste de sauvegarder mon avancement et peut permettre au gens de la communuaté d'apporter leur aide.

## Documentation

### Les projets

Les projets sont codé de la manière suivante :

- (octet 1) La taille du nom de la map (X)
- (pendant X octet) Le nom de la map
- (Jusque octet 155) 0x00
- (octet 156) la taille du chipset (Y)
- (pendant Y octet) chemin du chipset
- (Jusque octet 206) 0x00
- (octet 207) width
- (octet 208) 0x00
- (octet 209) height
- (jusque octet 777) 0x00

### Les cartes

Les cartes sont codé de la manière suivante :

- X couche basse
- X couche haute
- Y couche basse
- Y couche haute