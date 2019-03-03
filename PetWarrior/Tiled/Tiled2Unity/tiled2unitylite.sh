#!/bin/sh
 
THIS_DIR=`dirname $0`
pushd $THIS_DIR > /dev/null 2>&1
 
CSSCRIPT_DIR=/usr/share/CS-Script
mono $CSSCRIPT_DIR/cscs.exe -nl Tiled2UnityLite.cs "$@" &> tiled2unitylite.log
 
popd > /dev/null 2<&1
