# Scripts
A repository of scripts for ModernUO

### Getting Started
1. Clone this repository
1. Run `git submodule update --init`

### Creating a drop-in script
1. Make a copy of the `Scripts\Template` folder and name it the name of your feature.
1. Rename `Template.csproj` in your new folder to the same name as the folder.
1. Open `Visual Studio` or `Rider` and add an existing project. Choose the new csproj file you just renamed.
1. Start building the code!

### Submitting your drop-in
1. [Fork](https://github.com/modernuo/Scripts/fork) this repository.
1. Create your drop-in script on a branch.
1. Push the branch and changes to your fork.
1. Make sure you update the MODIFICATIONS file.
   * Join our [Discord](https://discord.gg/DHkNUsq) and ask for help if you are unsure.
1. Submit a [Pull Request](https://github.com/modernuo/Scripts/pulls)


### Tracking changes (GPL compliance)
If your drop-in makes modifications to a `ServUO` or `ModernUO` file or another project that requires tracking changes
then you will need to create a `MODIFICATIONS` file. See the modifications file in the `Template` project as an example.

This uses the following format:
```<path to file>  <git repo>/<commit>/<path to original file>```

You can find the hash of the file in Github by clicking the partial hash next to the `History` button:
<img width="341" src="https://user-images.githubusercontent.com/3953314/118382716-f25b3c80-b5ac-11eb-9991-cb207d8035dd.png">

You can find the path to the file by clicking the clipboard button for that file on the same page:
<img width="253" src="https://user-images.githubusercontent.com/3953314/118382727-1a4aa000-b5ad-11eb-815e-e4d46fcf9706.png">
