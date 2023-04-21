# instalace

# Linux

ubuntu:

```bash
sudo apt install mono-complete -y
```

fedora:

```bash
sudo dnf install mono-complete -y
```

pokud neje:

```bash
mono out.exe
```

potom (pro res/reseni.zip):

## moznost 1

```bash
mcs -r:System.Windows.Forms.dll -r:System.Drawing.dll res.cs -out:linux.exe -debug /nowarn:1591 -warn:0 && mono linux.exe
```

## moznost 2
```bash
cd source_code; cp ../init.sh init.sh; sudo sh init.sh && source /etc/profile.d/00-aliases.sh;cs
```

samostatne pak:

```bash
cs <arg_1> <arg_2> ... <arg_n>
```

- res/out.exe spustis samostatne:

```bash
mono res/out.exe
```

- pokud je res/linked.cs modifikovano:

```bash
cs-linked
```

- obnoveni:

```bash
cs-init
```

## Windows

pro spusteni je *win.exe*, ten je ale podle windowsu virus. Tim padem ke kompilaci: *res.cs* obalit namespacem a vlozit ho a kompilovat v novym projektu ve VS… 

Knihovny: System.Windows.Forms.dll, System.Drawing.dll
