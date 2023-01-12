
                              README.TXT File
                     README file for Contents of Memory
       Copyright(C), FANUC CORPORATION, 1997-2011. All Rights Reserved.

  This document provides the information about "Contents of Memory".
  Please read this document before using the SOFTWARE.

------------------------------------------------------------------------------
[1] Purpose
[2] Environment
[3] Execution
[4] Operation
------------------------------------------------------------------------------


==============================================================================
[1] Purpose
==============================================================================

  This software "Contents of Memory" is provided to read and write data in NC
  memory from a personal computer(*) on Open CNC system.

(*) A personal computer connected with the High Speed Serial Bus(HSSB),
    FS160i/180i/210i with PC function, the Intelligent Terminal type 2, the
    FANUC PANEL i and CNC Display Unit with Personal Computer Function.

==============================================================================
[2] Environment
==============================================================================

  This software is available on a personal computer connected with the High
  Speed Serial Bus (HSSB), FS160i/180i/210i with PC function, the
  Intelligent Terminal type 2, the FANUC PANEL i and CNC Display Unit with
  Personal Computer Function.
  It should be executed on Windows 95/98, Windows NT 4.0,
  Windows 2000 Professional, Windows XP Professional, Windows Vista Business
  and Windows 7 Professional.

  You can use this software on the following CNCs.
    * FS150-B
    * FS150i
    * FS160-B/C
    * FS180-B/C
    * FS210-B
    * FS160i/180i/210i-A/B
    * FS160i/180i-W
    * FS0i-A/B
    * FS0i-D
    * FS30i/31i/32i-A/BÅAFS35i-B
    * Power Mate-D/H
    * Power Mate i-D/H
    * Power Motion i-A


==============================================================================
[3] Execution
==============================================================================

  This software uses the HSSB driver (MMCNCD) at run-time. So, confirm that
  the HSSB driver has been installed in your hard disk.

  You can start this software by the following procedure.
  1) Run "ContMem.exe" from this CD-ROM.
  or
  2) Copy "ContMem.exe" from this floppy disk to a proper folder of a hard disk
     and then run it.


==============================================================================
[4] Operation
==============================================================================

(1) Select Node
     Do one of the following procedure to display "Node" dialogue window. Then
     select node and click [OK] button.
      * Point [Node] menu and click [Select Node...].
      * Click [Change Node] button on the tool bar.
      * Press function key "F2" of the full keyboard.

(2) Move Cursor
      * Press cursor key.
      * Click [Previous Line] button or [Next Line] button on the tool bar.

     Do one of the following procedure to turn over the page.
      * Point [Jump] menu and click [Previous Page] or [Next Page].
      * Click [Previous Page] button or [Next Page] button on the tool bar.
      * Press "Page Up" key or "Page Down" key of the full keyboard.

(3) Specify the address
     Do one of the following procedure to display "Address" dialog window.
     Then specify the address which you want to display and click [OK] button.
      * Point [Jump] menu and click [Address...].
      * Click [Address] button on the tool bar.
      * Press function key "F9" of the full keyboard.

(4) Change the display unit
     You can change the unit displaying the NC memory, such as byte, word, and
     double word. Only in case of byte, text is displayed on the screen.
      * Point [View] menu and click [Byte], [Word], or [Dword].
      * Click [Byte] button, [Word] button, or [Double Word] button on the
        tool bar.
      * Press function key "F6", "F7", or "F8" of the full keyboard.

(5) Input data
     You cannot write data in NC memory by default. Do one of the following
     procedure to switch from "Read Only" mode to "Write Possible" mode.
      * Point [View] menu and click [Read Only].
      * Click [Write Enable / Reed Only] button on the tool bar.
      * Press function key "F4" of the full keyboard.
     In case of "Write Possible" mode, press numeric key or "A" to "F" key to
     input data. "Data" dialogue window will appear. Click [OK] button.

(6) HSSB I/O Registers
     Do one of the following procedure to switch from the NC memory display to
     HSSB I/O Registers display. You can also change node in case of HSSB I/O
     Registers display.
      * Point [View] menu and click [HSSB I/O].
      * Click [HSSB I/O Registers] button on the tool bar.
      * Press function key "F3" of the full keyboard.

(7) Physical Slot Access
     When the NC stop in trouble as soon as you turn on the power, point [View]
     and click [Physical Slot].

   Note) Usually do not set "Physical Slot Access" mode.

