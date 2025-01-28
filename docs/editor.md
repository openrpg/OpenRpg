# OpenRpg Data Editor

![Editor Example](images/editor-item-example.png)

This application provides you a streamlined way to create and store `Templates` for your games/apps.

## How do I use it?

If you go to the latest github release you should see an option to download the `editor-app` there, which will be a zip file containing the application.

Run the `OpenRpg.Editor.exe` which will ask you to Create/Load a project, for now you wont have a project but just go ahead and make a folder where you want to store your project data and it will automatically create a few things:
- `project.json` - Contains configuration data
- `/assets` - Optional folder for providing visual assets (i.e sword icon shown in screenshot)
- `/templates` - Folder for storing template `json` files
- `/locales` - Folder for storing locale strings related to templates

You can then use the editor to create/edit templates and locale information, then you can save all the changes using the `Save` button at the top.

## FAQs

### Why does it use separate `json` files for everything instead of a database?
The choice for this was more for ease of use within teams and source control systems.

To expand on this imagine you have a project in git and two people both adding content to the editor project in their own branches. The first editor pushes up, and the second editor now has to merge those changes in.

When the templates are all in their own files in raw `json` it is quite easy for the 2nd person to see what has changed and merge the changes in, however if we were using a database (i.e sqllite, litedb) you would not be able to easily handle these merge scenarios, and everyone would cry real tears.

> It is planned to add support for exporting project data as a self-contained database at some point.

### What are the `template`, `assets` and `locale` folders for in the project directory?
When you create your project you have a `project.json` which contains project data i.e where directories are etc, feel free to change these as needed.

The folders created by default are for:
- `templates` - all your template data is stored here in a `json` file per template type
- `assets/*` - any assets you want to show up in the editor, i.e if you have an item with an `asset-code` it will look in `/assets/items/{asset-code}.png` and show it in the editor.
- `locales` - any locale data with a file per locale, i.e `en-gb.json`, `fr.json`

### Why doesnt it auto save?
This functionality may get added but due to use using potentially large data sets which are changing frequently it would put a lot of strain on your computer if we saved every change you made, so instead it's left to you to decide when you want to save your changes.

> This may be updated in the future to auto save every `n` minutes or something

### How do I add my own functionality to it?
You fork it and make your own changes, but going forward we will try to look into supporting a plugin based mechanism to let you write plugins that can just be included, as well as configure what genres should be used, as right now it only uses the `Fantasy` genre out the box.