# OpenRpg Editor

The OpenRpg editor is a simple editor to allow you to create and edit content for your OpenRpg games/apps.

## How to use it?

You open the editor and create or open a project, if you do not have a project just create a new one and it will automatically setup everything needed for you.

You can then use the editor to create/edit templates and locale information, then you can save all the changes using the Save button at the top.

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
Currently you fork it and make your own changes, but going forward we will try to look into supporting a plugin based mechanism to let you write plugins that can just be included.