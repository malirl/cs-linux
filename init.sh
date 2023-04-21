#!$SHELL
if [ -f /etc/fedora-release ]; then
  dnf install mono-complete -y
else
  apt install mono-complete -y
fi
build_path=$(pwd)/res
chmod 777 $build_path $build_path/source_code
cd $build_path 

echo "" > .err.txt
echo "" > linked.cs
echo "#!/usr/bin/bash
if [ ! -f \"\$1\" ]; then
dmcs_args=\$1
shift
else
dmcs_args=\"\"
fi
script=\$1
shift
input_cs=\"\$(mktemp)\"
output_exe=\"\$(mktemp)\"
output_exe_win=\"\$(mktemp)\"
args=\"-debug -doc:readme.xml /nowarn:1591 -warn:0\"
tail -n +2 \$script > \$input_cs
echo -e \"\\nkompiluju\\n------------------------------------\"
mcs \$dmcs_args \$input_cs -out:\${output_exe} \$args > .err.txt

if [ \"\$(cat .err.txt)\" != \"\" ]; then
  echo -e \"\\nneuspesna kompilace\\n------------------------------------\"
  exit
fi

echo -e \"\\nkompiluju pro win x86\\n------------------------------------\"
mcs \$dmcs_args \$input_cs -out:\${output_exe_win} -target:winexe -platform:x86 \$args

cp \$output_exe out.exe; cp \$output_exe_win win.exe; 

echo -e \"zipuju: res/reseni.zip\\n------------------------------------\"
cp linked.cs res.cs
sed -i '1d' res.cs
to_zip=\"source_code res.cs out.exe win.exe README.md init.sh \"
zip -r reseni \$to_zip

echo -e \"\\nspoustim\\n------------------------------------\\n\"
rm -f \$input_cs \$output_exe
mono out.exe \$@
" > /usr/bin/csexec
echo ".cs.sh
----------------------------------------
"
echo "#!/usr/bin/bash
clear
cd res
mv linked.cs linked.txt
linked=\"#!/usr/bin/csexec -r:System.Windows.Forms.dll -r:System.Drawing.dll\n\"
files=""

linked+=\$(cat -e ../main.cs | sed 's/\\\$/\\\\n/')

for f in ../*.cs; do 
  files+=\$f\" \"
  if [ \"\$f\" != \"../main.cs\" ]; then
    linked+=\$(cat -e \$f | sed 's/\\\$/\\\\n/')
  fi
done;

echo -e \$linked > linked_pre.txt

if [ \"\$(md5sum linked.txt | cut -d' ' -f1)\" == \"\$(md5sum linked_pre.txt | cut -d' ' -f1)\" ] && [ \"\$(cat .err.txt)\" == \"\" ]; then
  echo -e \"bez zmeny, spoustim\\n------------------------------------\"
  mv linked.txt linked.cs
  rm linked_pre.txt
  mono out.exe \$@
  exit
fi
echo \"\$files-> linked.cs -> (args: \"\$@\") out.exe\"
mv linked_pre.txt linked.cs > linked.cs && sudo chmod +x linked.cs
rm linked.txt
cp \$files source_code 
./linked.cs \$@" > ../.cs.sh && chmod 777 linked.cs ../.cs.sh .err.txt
cp ../init.sh  init.sh
mv ../init.sh ../.init.sh
chmod 777 $build_path $build_path/source_code

echo "alias cs='./.cs.sh'
alias cs-init='sudo rm -r res && sudo rm .cs.sh .err.txt && mv .init.sh init.sh && sudo sh ./init.sh'
alias cs-linked='res/linked.cs'
" >> /etc/profile.d/00-aliases.sh

