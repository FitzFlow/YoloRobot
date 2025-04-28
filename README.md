
# YOLO PIZZA VR
---

## Présentation
**Yolo Pizza** est un jeu en Réalité Virtuelle où vous incarnez un robot pizzaïolo chargé de préparer des pizzas dans un temps imparti. Armé d'un **grappin**, vous devez assembler des recettes variées, entre tradition et originalité, tout en respectant les étapes clés de la confection.

**Type de jeu:** Jeu VR (Réalité Virtuelle)  
**Plateforme:** Unity (+XR Toolkit)  
**Inspirations:** Pizza Master VR, Cook-Out VR, Cooking Simulator VR  
**Particularités:** Pas de clients, livraison automatisée, gameplay axé sur le scoring et la rapidité avec des interactions atypiques.

---
## Structure du Projet

```
Assets/
├── Accessoires/            -> Contient des accessoires pour les bornes etc 
├── DLAssets/               -> Assets téléchargés du Unity Store (props, modèles, etc.)
├── Food Pack-Demo/         -> Pack d'ingrédients et objets liés à la nourriture
├── main/                   -> Contient des fichiers de base d'Unity VR
├── Pizza/                  -> Éléments liés au gameplay Pizza (prefabs, textures)
├── Resources/              -> même chose de main/ (contient des éléments d'Unity VR)
├── Samples/                -> Fichier de XR Toolkit (XR Hands, Interaction toolkit, ...)
├── Scripts/                -> Scripts C# (grappin, gestion pizza, timer, scoring)
├── Settings/               -> Configurations du projet Unity
├── TextMesh Pro/           -> Fichier pour le texte
├── VRTemplateAssets/       -> Template VR de base utilisé
├── XR/                     -> Configurations XR
├── XRI/                    -> XR Interaction Toolkit (inputs/actions)
Packages/                   -> Dépendances et packages Unity
```

---

## Lancement du Projet

### Pré-requis
- **Editor Version :** _6000.0.33f1_
- **SDK VR :** XR Toolkit / Oculus / SteamVR

### Comment lancer:

Lancer le logiciel Unity avec le projet installé avec la bonne version. Ensuite se connecter via un cable link ou wifi Link avec Oculus Quest et ensuite appuyer sur play pour lancer le jeu.

## Crédits

- Joshua MARTINOLI
- Alexandre BONEFONS
- Loic CHAYLA
- Florian ETHEVE
