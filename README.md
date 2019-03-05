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

### Les fichiers .zon

Les fichiers .zon sont nommé en fonction des différentes maps. Leur codage est le suivant :

- Xstart de la zone (2 octets)
- 0x00 x2
- Xend de la zone (2 octets)
- 0x00 x2
- Ystart de la zone (2 octets)
- 0x00 x2
- Yend de la zone (2 octets)
- 0x00 x2
- 0x00 x4
- Nombre max de mob (2 octets)
- 0x00 x4
- Type (4 octets, à noté que "spawn interdit" est codé comme ceci : ffff ffff)
- ???? x4 (character qui semblent aléatoire, peut être ils ont une signification, mais je vois pas)
- Vitesse spawn (2 octets)
- taille du nom variable server (Y) (1 octets)
- nom de variable (Y octets)
- 0x00 jusque 292
- taille de la valeur assigné à la variable (Z) (1 octets)
- valeur (Z octets)
- 0x00 jusque 547

Chaque zone est donc codée sur 548 caractères, il peut y avoir plusieurs zone dans le même fichier,
le codage ne change pas en fonction du nombre de zone.