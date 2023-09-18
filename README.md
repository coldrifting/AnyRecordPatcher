# Any Record Patcher
A Synthesis Patcher for Skyrim that allows users to edit records with YAML config files, for more granular patching then ESPs alone.

See the Examples folder for examples of patches.

While you can create a patch manually, it will be much easier 
to create a plugin in SSEEdit containing only the changes that you want to patch.

Then run the AnyRecordExporter program (Located on the Releases Page) to generate a patch.

Finally, import AnyRecordPatcher into the Synthesis GUI and copy any patch folders you created into
the user settings folder of synthesis, usually located at 

    <Synthesis Location>\Data\AnyRecordPatcher

Then Run the patch in Synthesis and you're done!

# Supported Records
I may have been a bit hasty with the name of the project, 
but currently the following records are supported:
* Ammunition
* Armor
* Books
* Cells
    * Cell records only, like encounter zone and water type
    * No support for placed objects is planned
* Ingestibles
* Ingredients
* Lights
* Misc Items
* Perks
* Scrolls
* Shouts
* Soul Gems
* Spells
* Weapons

Let me know if a sub record that you'd like to patch in one of those categories 
is not getting patched properly or at all and I'll see if I can add it.
