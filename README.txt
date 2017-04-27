Readme- Holoture App

The scripts are found in /Assets

The most notable folders and files are:

Resources/ - This folder contain the majority of the models, materials, shaders and prefabs used in the application

CatalogManager.cs - Determines which furniture pieces exist on the HoloLens and sorts them according to their type (chair, table, etc.)
ChangePref.cs - Handles logic for adding or removing furniture from menu
CustomerFurnitureMenu.cs - Sets up the furniture GUI objects that are displayed in the Furniture menu/catalog.
CustomerManager.cs - Gets and parses the customer registry from Holoture.com endpoint
CyclesTexturesButt.cs - Handles logic to cycle through textures using "Cycle Textures" Button in the menu
DeleteButton.cs - Handles removal of furniture objects from the scene.
FurnitureManager - Handles the furniture stored on the HoloLens. Matches furniture to materials and textures
MaterialManager - Matches the materials with their IDs from the database.
MenuOpener - Handles the logic for opening the menu interface when user clicks on a furniture hologram.
Rotater.cs - Handles the logic to rotate holograms around the y-axis.
TextureCatalog.cs - Displays the texture GUI objects in the Texture Catalog Menu.
TextureMenu.cs - Displays the texture GUI objects in the Texture Main Menu. 







