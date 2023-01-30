## Lighting instructions
The lighting script can be found at `python_req_test.py`, which contains an argument 
parser for using on the command line as a standalone script. The code connects to the
Helvar lights in the Aistikattila room and calls the appropriate API points to change
the light settings in the room.

## Usage
### Scheme names
The available scheme names can be found from the USee UI delivered by the Helvar lights
server in Aistikattila. To create a new scene needed for the script to call, it should be
done in the USee UI for ease-of-use, and is out of scope for this script. The script is 
not case-sensitive. Some scene names available at the time of making this documentation 
are:
### TODO
```
Tilanne 1 (Perustilanne)
Tilanne 2 or Pinkki
Tilanne 3 or Sini-punainen
Tilanne 4 or Spotti
Tilanne 5 or Joulu
Tilanne 6 or Aamu-usva
Tilanne 7 or Hapan vihre\u00e4
Tilanne 8 or Mets\u00e4
Tilanne 9 or Art Work
Tilanne 10 or Keitti\u00f6p\u00e4\u00e4dyn valaistus
Tilanne 11
Tilanne 12
Off
Scene 2.1 or Beige
Scene 2.2 or Puna-oranssi
Scene 2.3 or Beige (takasein\u00e4)
Scene 2.4 or Art Work 2
Scene 2.5 or Vihre\u00e4 (takasein\u00e4)
Scene 2.8 or Vihre\u00e4t pallovalaisimet
Scene 2.9 or Merenalainen maailma
Scene 2.10 or REMU
Scene 2.12 or Halloween
Scene 2.13 or Barcelona
```

### As a standalone script
The script can simply be called over the command line with `python3 python_req_test.py` 
or using the python cmd. The script requires a passed argument to it, which is the name 
of the lighting scene to which you wish to switch to. These can be found on the USee 
interface in Aistikattila. An example of how this can be called can be seen below.
```
usage: python_req_test.py [-h] [--scheme-name SCHEME_NAME]

example: python3 python_req_test.py off
```

### As part of Unity code
The argument parsing part of the code can be omitted and the name of the scene called can
simply be hard-coded into the call, or the whole script can be called, whatever preferred.
The easier option however is to run the whole script as a unit, and avoid hard-coding the
whole script multiple times. An example of how to achieve this can be found below.
```
using UnityEditor.Scripting.Python;
using UnityEditor;
using UnityEngine;
using System.IO;

string scriptPath = Path.Combine(Application.dataPath,"python_req_test.py");
PythonRunner.RunFile(scriptPath);
```
The code `Path.Combine(Application.dataPath,"python_req_test.py");` searches for the script
under the pathing to our project, and then the `Assets` folder.

Notice that this implementation does not allow for additional argument passing to the script,
so the script additionally searches for a text file called `scheme_option.txt` in the same
directory. If no additional argument and no scheme_option.txt file is found, or if the contents
are empty, the script will explicitly fail. Hence, we can still use the script fully and just 
change the contents of the `scheme_option.txt` file accordingly to our needs.
