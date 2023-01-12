
                             README.TXT  File
           README file for FANUC Drivers and Libraries (FOCAS1/2)
       (C) Copyright FANUC CORPORATION, 1996-2015. All Rights Reserved.


This document provides the information about "FANUC Drivers and Libraries 
(FOCAS1/2). Please read this document information before using the software 
in this disk.

  (*) FOCAS1/2 : FANUC Open CNC API Specifications version 1 or 2

--------------------------------------------------------------------------
[1] Constitution of the CD-ROM
[2] Contents of the CD-ROM
[3] File summary
[4] Environment
[5] Installation
[6] Redistributable software
[7] History
--------------------------------------------------------------------------

  Microsoft, MS, MS-DOS, Visual Basic, Visual C++, Windows, Win32, Win32s,
  Windows NT, Windows Vista are registered trademarks of Microsoft
  corporation, USA.
  Company name and product name mentioned in this manual are registered
  trademark or trademark of each company. 

  (*) The software for HSSB in this CD-ROM can be used on a personal computer
      connected with the High Speed Serial Bus (HSSB), FS160i/180i/210i,
      FANUC PANEL i and CNC Display Unit with Personal Computer Function.
      The software in this disk are not designed for MMC-IV, Personal computer
      connected with the HSSB type 1, Intelligent Terminal.


==========================================================================
[1] Constitution of the CD-ROM
==========================================================================

  This disk contains the libraries which are used by the following development
  environment, e.g.
    - 32 bit version
        Visual C++ 2.x/4.x/5.x/6.x, Visual studio 2002/2003/2005/2008/2010
        Visual Basic 4.x/5.x/6.x
    - 64 bit versionÅifor Winows 7 Professional 64-bit Edition onlyÅj
        Visual studio 2010
    - Linux (Ubuntu 12.04 LTS, Fedora 17, CentOS 6.3 onlyÅj
        GNU Compiler Collection (version 4.6.3 or later)

==========================================================================
[2] Contents of the CD-ROM
==========================================================================

  This CD set has the following folder structures.
  Please copy the necessary files to your hard disk.

  See the "[5] Installation" for detailed description.

    README.TXT  : This file
    READMEJ.TXT : Japanese document file


    * HSSB driver (32/64 bit version)

     <HSSB>\README2.TXT   : English document of HSSB driver
     <HSSB>\README2J.TXT  : Japanese document of HSSB driver
     <HSSB>\NCBOOT32.DOC  : English document of NCBOOT32.EXE(32/64 bit common)
     <HSSB>\NCBOOT32J.DOC : Japanese document of NCBOOT32.EXE(32/64 bit common)
     <HSSB>\UPDATE.EXE    : HSSB driver update utility program
     <HSSB\WIN95>\*.*     : Windows 95/98 HSSB driver
     <HSSB\NT40>\*.*      : Windows NT 4.0 HSSB driver
     <HSSB\WIN2K>\*.*     : Windows 2000 HSSB driver
     <HSSB\WINXP>\*.*     : Windows XP HSSB driver
     <HSSB\COMMON>\*.*    : Common files for Windows 95/98/NT/2000 and XP
     <HSSB\VISTA>\*.*     : Windows Vista/7 32-bit Edition HSSB driver
     <HSSB\WIN7_64>\*.*   : Windows 7 64-bit Edition HSSB driver

        Note) Please use the Microsoft Word 2000 or later for the document
              files.
              When you used the Wordpad, the document files maybe not open.


    * CNC/PMC Data window library(32 bit version)

     <FWLIB>\FWLIB32.DLL  : CNC/PMC Data window control library
     <FWLIB>\FWLIBE1.DLL  : Processing library for TCP/IP
     <FWLIB>\FWLIB30i.DLL : Processing library for 30i/31i/32i/35i/0i-F, PMi-A
     <FWLIB>\FWLIB150.DLL : Processing library for 150-B
     <FWLIB>\FWLIB15i.DLL : Processing library for 150i
     <FWLIB>\FWLIB160.DLL : Processing library for 160/180/210
     <FWLIB>\FWLIB16W.DLL : Processing library for 160i/180i-W
     <FWLIB>\FWLIB0i.DLL  : Processing library for 0i-A
     <FWLIB>\FWLIB0iB.DLL : Processing library for 0i-B
     <FWLIB>\FWLIB0iD.DLL : Processing library for 0i-D
     <FWLIB>\FWLIBPM.DLL  : Processing library for Power Mate-D/H
     <FWLIB>\FWLIBPMi.DLL : Processing library for Power Mate i-D/H
     <FWLIB>\FWLIBNCG.DLL : Processing library for FS31i/32i/35i NCGuidePro
     <FWLIB>\FWLIB0DN.DLL : Processing library for FS0i-D NCGuidePro
     <FWLIB>\FWLIB32.LIB  : Import library
     <FWLIB>\FWPMCALM.INI : Alarm message file for PMC
     <FWLIB\LINUX\ARM>\libfwlib32.so.1.0.1 
     <FWLIB\LINUX\X86>\libfwlib32.so.1.0.1 
                          : Processing library for Linux


    * header files of C/C++ and Visual Basic for CNC/PMC Data window functions
                                                               (32 bit version)

     <FWLIB>\FWSYMBOL.H       : Symbol definition file(Common)
     <FWLIB\e1>\FWLIB32.H     : C/C++/C++ .NET header file for CNC/PMC Data
                                window functions and structures for TCP/IP.
                     (For FS15i/16i/18i/21i/30i/31i/32i/35i/0i-B/0i-C/0i-D/0i-F,
                         Power Mate i, PMi-A, FS31/32i/35i/0i-D NCGuidePro)
     <FWLIB\e1>\FWLIB32.BAS   : Visual Basic header file for CNC/PMC Data
                                window functions and structures for TCP/IP.
                     (For FS15i/16i/18i/21i/30i/31i/32i/35i/0i-B/0i-C/0i-D/0i-F,
                         Power Mate i, PMi-A, FS31/32i/35i/0i-D NCGuidePro)
     <FWLIB\30i>\FWLIB32.H    : C/C++ header file for CNC/PMC Data window
                                functions and structures for FS30i/31i/32i/
                                35i/0i-F, PMi-A, FS31/32i/35i NCGuidePro
     <FWLIB\30i>\FWLIB32.BAS  : Visual Basic header file for CNC/PMC Data
                                window functions and structures for
                                FS30i/31i/32i/35i/0i-F, PMi-A, 
                                FS31/32i/35i NCGuidePro
     <FWLIB\150>\FWLIB32.H    : C/C++ header file for CNC/PMC Data window
                                functions and structures for FS150-B
     <FWLIB\150>\FWLIB32.BAS  : Visual Basic header file for CNC/PMC Data
                                window functions and structures for FS150-B
     <FWLIB\150i>\FWLIB32.H   : C/C++ header file for CNC/PMC Data window
                                functions and structures for FS150i
     <FWLIB\150i>\FWLIB32.BAS : Visual Basic header file for CNC/PMC Data
                                window functions and structures for FS150i
     <FWLIB\160>\FWLIB32.H    : C/C++ header file for CNC/PMC Data window
                                functions and structures for FS160/180-B/C,
                                FS210-B, FS160i/180i/210i
     <FWLIB\160>\FWLIB32.BAS  : Visual Basic header file for CNC/PMC Data
                                window functions and structures for
                                FS160/180-B/C, FS210-B, FS160i/180i/210i
     <FWLIB\16iW>\FWLIB32.H   : C/C++ header file for CNC/PMC Data window
                                functions and structures for FS160i/180i-W
     <FWLIB\16iW>\FWLIB32.BAS : Visual Basic header file for CNC/PMC Data
                                window functions and structures for
                                FS160i/180i-W
     <FWLIB\0i>\FWLIB32.H     : C/C++ header file for CNC/PMC Data window
                                functions and structures for FS0i-A
     <FWLIB\0i>\FWLIB32.BAS   : Visual Basic header file for CNC/PMC Data
                                window functions and structures for FS0i-A
     <FWLIB\0iB>\FWLIB32.H    : C/C++ header file for CNC/PMC Data window
                                functions and structures for FS0i-B
     <FWLIB\0iB>\FWLIB32.BAS  : Visual Basic header file for CNC/PMC Data
                                window functions and structures for FS0i-B
     <FWLIB\0iD>\FWLIB32.H    : C/C++ header file for CNC/PMC Data window
                                functions and structures for FS0i-D,
                                FS0i-D NCGuidePro
     <FWLIB\0iD>\FWLIB32.BAS  : Visual Basic header file for CNC/PMC Data
                                window functions and structures for FS0i-D,
                                FS0i-D NCGuidePro
     <FWLIB\PM>\FWLIB32.H     : C/C++ header file for CNC/PMC Data window
                                functions and structures for Power Mate-H and
                                Power Mate-D
     <FWLIB\PM>\FWLIB32.BAS   : Visual Basic header file for CNC/PMC Data
                                window functions and structures for
                                Power Mate-H and Power Mate-D
     <FWLIB\PMi>\FWLIB32.H    : C/C++ header file for CNC/PMC Data window
                                functions and structures for Power Mate i
     <FWLIB\PMi>\FWLIB32.BAS  : Visual Basic header file for CNC/PMC Data
                                window functions and structures for
                                Power Mate i

     <FWLIB\Dot NET sample>\FWLIB32.CS : Sample for Visual C# .NET
               This file is a sample of the header file for Visual C# .NET.
               Please correct and use the content if necessary.
     <FWLIB\Dot NET sample>\FWLIB32.VB : Sample for Visual Basic .NET
               This file is a sample of the header file for Visual  Basic .NET.
               Please correct and use the content if necessary.
     <FWLIB\Dot NET sample>\DotNet.TXT  : English document file of the header
                                          file for .NET.
     <FWLIB\Dot NET sample>\DotNetJ.TXT : Japanese document file of the header
                                          file for .NET.

     <FWLIB\LINUX>\FWLIB32.H   : C/C++ header file for CNC/PMC Data window
                                 functions and structures for Linux.
                                 (For FS30i/31i/32i/35i/0i-D, PMi-A)


    * CNC/PMC Data window library (64 bit version)

     <FWLIB64>\FWLIB64.DLL    : CNC/PMC Data window control library
     <FWLIB64>\FWLIBE64.DLL   : Processing library for TCP/IP
     <FWLIB64>\FWLIB30i64.DLL : Processing library for 30i/31i/32i/35i/0i-F,
                                PMi-A
     <FWLIB64>\FWLIB0iD64.DLL : Processing library for 0i-D
     <FWLIB64>\FWLIBNCG64.DLL : Processing library for FS31i/32i/35i NCGuidePro
     <FWLIB64>\FWLIB0DN64.DLL : Processing library for FS0i-D NCGuidePro
     <FWLIB64>\FWLIB64.LIB    : Import library

        * The 64 bit version supports FOCAS2 (for FS30i/31i/32i/35i/0i-F, PMi-A,
          FS0i-D) only.


    * Header files of C/C++ and Visual Basic for CNC/PMC Data window functions
                                                               (64 bit version)
     <FWLIB64>\FWSYMBOL.H    : Symbol definition file(Common)
     <FWLIB64\e1>\FWLIB64.H  : C/C++/ header file for CNC/PMC Data window
                                functions and structures for TCP/IP.
                                (For FS30i/31i/32i/35i/0i-D/0i-F, PMi-A, 
                                                 FS31i/32i/35i/0i-D NCGuidePro)
     <FWLIB64\30i>\FWLIB64.H : C/C++ header file for CNC/PMC Data window
                                functions and structures for FS30i/31i/32i/35i/
                                0i-F, PMi-A, FS31i/32i/35i NCGuidePro
     <FWLIB64\0iD>\FWLIB64.H : C/C++ header file for CNC/PMC Data window
                                functions and structures for FS0i-D,
                                FS0i-D NCGuidePro


    * MDI key driver(32 bit version only)

     The MDI key driver is not included in this CD-R.
     The MDI key driver exist in A02B-0207-K793 and A08B-0084-K790 of Drivers
     Disk.
     When MDI key driver is used on Windows Vista Business or, Windows 7
     Professional, A08B-0084-K790 version 01.8 or later is necessary.


    * Documentation

     <DOCUMENT\SPECE>\*.*       : English document file of data window library
                                  specification
     <DOCUMENT\SPECJ>\*.*       : Japanese document file of data window library
                                  specification
     <DOCUMENT\PM>\FWLIBPM.TXT  : English document file of Power Mate library
                                  specification
     <DOCUMENT\PM>\FWLIBPMJ.TXT : Japanese document file of Power Mate library
                                  specification
     <DOCUMENT\NCG>\(Japanese file name).pdf
                                : Japanese document file of NCGuidePro FOCAS2
                                  Function OPERATORÅfS MANUAL
     <DOCUMENT\NCG>\NCGuidePro FOCAS2 Function.pdf
                                : English document file of NCGuidePro FOCAS2
                                  Function OPERATORÅfS MANUAL
     <DOCUMENT\LINUX>\FOCAS2_Linuxj.pdf
                                : Japanese document file of FOCAS2/Ethernet for
                                  Linux OPERATORÅfS MANUAL
     <DOCUMENT\LINUX>\FOCAS2_Linux.pdf
                                : English document file of FOCAS2/Ethernet for
                                  Linux OPERATORÅfS MANUAL


    * Utility

     <UTILITY>\CONTMEM.EXE  : The CNC memory dump utility program
     <UTILITY>\READMEC.TXT  : English document of CNC memory dump utility
     <UTILITY>\READMECJ.TXT : Japanese document of CNC memory dump utility
     <UTILITY>\OPTSET.EXE   : The CNC option setting utility
     <UTILITY>\README4.TXT  : English document of option setting utility
     <UTILITY>\README4J.TXT : Japanese document of option setting utility
     <UTILITY>\NUMLOCK.EXE  : This tool enables change of the state of NumLock
     <UTILITY>\NUMLOCK.TXT  : English document of NUMLOCK.EXE
     <UTILITY>\NUMLOCKJ.TXT : Japanese document of NUMLOCK.EXE


    * Log output

     <LOG>\FWLOG.EXE    : Communication log output display utility for Data
                          Window Library
     <LOG>\LOGLEVEL.EXE : Communication log output control utility for Data
                          Window Library

    * Program operation on large capacity memory

     <NCPROG>\FBINBUILD\FBINBUILD.EXE   : Program storage file editing tool
     <NCPROG>\NCPROGCTRL\NCPROGCTRL.EXE : Program operation control software
     <NCPROG>\NCPROGDLL\WIN32\          : Program storage file edit library
                                                               (32 bit version)
     <NCPROG>\NCPROGDLL\X64\            : Program storage file edit library
                                                               (64 bit version)

    * Serial/FCA communication

     <SERIAL>\FCA32.DLL   : Serial/FCA communication dynamic link library
     <SERIAL>\FCA32.LIB   : Serial/FCA communication import library
     <SERIAL>\FCA32.H     : C/C++ header file for serial/FCA communication
                            functions and structures
     <SERIAL>\FCA32.BAS   : Visual basic header file for serial/FCA
                            communication functions and structures
     <SERIAL>\SERIAL.TXT  : English document file of serial/FCA communication
                            library specification
     <SERIAL>\SERIALJ.TXT : Japanese document file of serial/FCA communication
                            library specification


   * Unsolicited Messaging Function

     <UNMSG>\UMSGSERV.EXE : Server for Unsolicited Messaging Function


==========================================================================
[3] File summary
==========================================================================

    Please refer to README2.TXT for the explanation of the files contained
    in <HSSB> folder, respectively.

    * 32 bit version FOCAS1/2 library.

      < FWLIB32.DLL >
        This is a library of data exchange functions between CNC/PMC and HSSB
        and Ethernet system.
        This is a dynamic link library, which is required at run-time of HSSB
        and Ethernet system.

      < FWLIBE1.DLL >
        This is the DLL for TCP/IP, which is called by FWLIB32.DLL at run-time.
        This library is available to FOCAS2/Ethernet for NCGuidePro.

      < FWLIB30i.DLL >
        This is the DLL for FS30i/31i/32i/35i/0i-F and PMi-A, which is called
        by FWLIB32.DLL at run-time.

      < FWLIB150.DLL >
        This is the DLL for FS150-B, which is called by FWLIB32.DLL at run-time.

      < FWLIB15i.DLL >
        This is the DLL for FS150i, which is called by FWLIB32.DLL at run-time.

      < FWLIB160.DLL >
        This is the DLL for FS160/180-B/C, FS210-B, FS160i/180i/210i, which is
        called by FWLIB32.DLL at run-time.

      < FWLIB16W.DLL >
        This is the DLL for FS160i/180i-W, which is called by FWLIB32.DLL at
        run-time.

      < FWLIB0i.DLL >
        This is the DLL for FS0i-A, which is called by FWLIB32.DLL at run-time.

      < FWLIB0iB.DLL >
        This is the DLL for FS0i-B, which is called by FWLIB32.DLL at run-time.

      < FWLIB0iD.DLL >
        This is the DLL for FS0i-D, which is called by FWLIB32.DLL at run-time.

      < FWLIBPM.DLL >
        This is the DLL for Power Mate-H and Power Mate-D, which is called by
        FWLIB32.DLL at run-time.

      < FWLIBPMI.DLL >
        This is the DLL for Power Mate i-H and Power Mate i-D, which is called
        by FWLIB32.DLL at run-time.

      < FWLIBNCG.DLL >
        This is the DLL for FOCAS2/HSSB of FS31i/32i/35i NCGuidePro, which is
        called by FWLIB32.DLL at run-time.

      < FWLIB0DN.DLL >
        This is the DLL for FOCAS2/HSSB of FS0i-D NCGuidePro, which is called
        by FWLIB32.DLL at run-time.

      < FWLIB32.LIB >
        The import library to link with CNC/PMC Data window library
        (FWLIB32.DLL) in C/C++ language.

      < FWPMCALM.INI >
        The alarm message file for PMC which is used by pmc_rdalmmsg()
        function.

      < FWLIB32.H >
        C/C++ header file for CNC/PMC Data window functions and structures.
        This file is divided for each CNC model.

      < FWSYMBOL.H >
        In this file, the symbols which are used in the CNC/PMC data window
        function is defined.  This file is not divided for each CNC model.

      < FWLIB32.BAS >
        Visual Basic header file for CNC/PMC Data window functions and
        structures.
        This file is divided for each CNC model.

      < FWLIB32.CS >
        Visual C# .NET header file for CNC/PMC Data window functions and
        structures.

      < FWLIB32.VB >
        Visual Basic .NET header file for CNC/PMC Data window functions and
        structures.

      < libfwlib32.so.1.0.1 >
        This is a library of data exchange functions between CNC/PMC and 
        Ethernet system.
        This is a library for Linux

    * 64 bit version FOCAS2 library.

      < FWLIB64.DLL >
        This is a library of data exchange functions between CNC/PMC and HSSB
        and Ethernet system.
        This is a dynamic link library, which is required at run-time of HSSB
        and Ethernet system.

      < FWLIBE64.DLL >
        This is the DLL for TCP/IP, which is called by FWLIB64.DLL at run-time.

      < FWLIB30i64.DLL >
        This is the DLL for FS30i/31i/32i/35i/0i-F and PMi-A, which is called
        by FWLIB64.DLL at run-time.

      < FWLIB0iD64.DLL >
        This is the DLL for FS0i-D, which is called by FWLIB64.DLL at run-time.

      < FWLIBNCG64.DLL >
        This is the DLL for FOCAS2/HSSB of FS31i-A/B NCGuidePro, which is
        called by FWLIB64.DLL at run-time.

      < FWLIB0DN64.DLL >
        This is the DLL for FOCAS2/HSSB of FS0i-D NCGuidePro, which is called
        by FWLIB64.DLL at run-time.

      < FWLIB64.LIB >
        The import library to link with CNC/PMC Data window library
        (FWLIB64.DLL) in C/C++ language.

      < FWLIB64.H >
        C/C++ header file for CNC/PMC Data window functions and structures.
        This file is divided for each CNC model.

    * Other file.

      < DOCUMENT\SPECE\FWLIB32.HTM >
        Top page of English document of data window library specification.
        * The browser of Internet Explorer 6.0 or later is necessary to see
          this specifications.

      < DOCUMENT\SPECJ\FWLIB32.HTM >
        Top page of Japanese document of data window library specification.
        * The browser of Internet Explorer 6.0 or later is necessary to see
         this specifications.

      < FWLIBPM.TXT >
        English document file of Power Mate library specification

      < FWLIBPMJ.TXT >
        Japanese document file of Power Mate library specification

      < FCA32.DLL >
        The library for use of serial port and FCA( FANUC Cassette Adapter ).

      < FCA32.LIB >
        The import library to link with serial/FCA communication library
        (FCA32.DLL) in C/C++ language.

      < FCA32.H >
        C/C++ header file for serial/FCA communication functions and structures.

      < FCA32.BAS >
        Visual basic header file for serial/FCA communication functions and
        structures.

      < CONTMEM.EXE >
        The CNC memory dump utility program for maintenance.
        Please copy this file onto the backup floppy disk which is shipped with
        your machine.

      < OPTSET.EXE >
        The CNC option setting utility program for maintenance.
        Please copy this file onto the backup floppy disk which is shipped with
        your machine.

      < FWLOG.EXE >
        Communication log output display utility for Data Window Library.

      < LOGLEVEL.EXE >
        This is a utility to control the output level of the communication log
        for Data window library.

      < NUMLOCK.EXE >
        An application program that enables to change the state of NumLock 
        without a full-keyboard.

      < UMSGSERV.EXE >
        It is a server on the PC side to use the Unsolicited Messaging
        Function.
        Please refer to the data window library specifications to use the
        Unsolicited Messaging Function.  This function is only for "Fast
        Ethernet board".


==========================================================================
[4] Environment
==========================================================================

  All software in the disk is for Windows 95/98, Windows NT 4.0,
  Windows 2000 Professional, Windows XP Professional, Windows Vista Business,
  Windows 7 Professional and Linux (Ubuntu 12.04 LTS, Fedora 17, CentOS 6.3).
  They cannot be used on Win32S of Windows 3.1.

  HSSB driver and FOCAS2 library support Windows 7 Professional 64-bit Edition.

  Before using the TCP/IP software in this disk, see the manual "FANUC Ethernet
  Board Operating Manual (B-63354EN)", "FANUC Fast Ethernet Board/Fast Data
  Server Operating Manual (B-63644EN)" or "FANUC 16i/18i/21i-MODEL B Connection
  Manual(B-63523-1EN)", and confirm the following items.

    * Settings of TCP/IP in your personal computer side.
    * Settings of the Ethernet Board and Embedded Ethernet function in the CNC
      side.
    * Physical network connection between your personal computer and the CNC.


  Following system/version supports Punch press and Laser functions.

        Ethernet Board          16i/18i/21i     6561/06   or later
        Fast Ethernet Board     16i/18i/21i     6567/01   or later

  Following system/version supports FS150i.

        Ethernet Board          15i             6563/05   or later
        Fast Ethernet Board     15i-B           6568/01   or later

  Following system/version supports FS0i-B.

        Fast Ethernet Board     0i-B            6567/07   or later

  Following version of FS31i/32i-A NCGuidePro supports FOCAS2/HSSB and
  FOCAS2/Ethernet function.

        NCGuidePro (1User)       A08B-9010-J735#ZZ11 3.00 or later
        NCGuidePro(10User)       A08B-9010-J736#ZZ11 3.00 or later
        NCGuidePro(20User)       A08B-9010-J737#ZZ11 3.00 or later
        NCGuidePro(30User)       A08B-9010-J738#ZZ11 3.00 or later

  Following version of FS31i/32i/35i-B NCGuidePro supports FOCAS2/HSSB and
  FOCAS2/Ethernet function.

        NCGuidePro (1User)       A08B-9010-J735#ZZ12 5.00 or later
        NCGuidePro(10User)       A08B-9010-J736#ZZ12 5.00 or later
        NCGuidePro(20User)       A08B-9010-J737#ZZ12 5.00 or later
        NCGuidePro(30User)       A08B-9010-J738#ZZ12 5.00 or later

  Following version of FS0i-D NCGuidePro supports FOCAS2/HSSB function.

        NCGuidePro (1User)       A08B-9010-J735#ZZ11 4.30 or later
        NCGuidePro(10User)       A08B-9010-J736#ZZ11 4.30 or later
        NCGuidePro(20User)       A08B-9010-J737#ZZ11 4.30 or later
        NCGuidePro(30User)       A08B-9010-J738#ZZ11 4.30 or later

  Following version of FS0i-D NCGuidePro supports FOCAS2/Ethernet function.

        NCGuidePro (1User)       A08B-9010-J735#ZZ11 4.00 or later
        NCGuidePro(10User)       A08B-9010-J736#ZZ11 4.00 or later
        NCGuidePro(20User)       A08B-9010-J737#ZZ11 4.00 or later
        NCGuidePro(30User)       A08B-9010-J738#ZZ11 4.00 or later


==========================================================================
[5] Installation
==========================================================================

  About the detailed description for install procedure for drivers, please
  refer to README2.TXT.

  Install the HSSB driver for the personal computer connected with the
  High Speed Serial Bus(HSSB), FS160i/180i/210i, FANUC PANEL i and CNC Display
  Unit with Personal Computer Function.

< FWLIB32.DLL  > < FWLIBE1.DLL  > < FWLIB30i.DLL > < FWLIB150.DLL >
< FWLIB15i.DLL > < FWLIB160.DLL > < FWLIB16W.DLL > < FWLIB0i.DLL  >
< FWLIB0iB.DLL > < FWLIB0iD.DLL > < FWLIBPM.DLL  > < FWLIBPMi.DLL >
< FWLIBNCG.DLL > < FWLIB0ND.DLL >
< FWLIB64.DLL  > < FWLIBE64.DLL  > < FWLIB30i64.DLL > < FWLIB0iD64.DLL >
< FWLIBNCG64.DLL > < FWLIB0ND64.DLL >
< libfwlib32.so.1.0.0 >
  Copy all DLLs to the appropriate folder of your application can access at
  run-time. Installing to the Windows System folder is recommended.

< FWLIB32.LIB > < FWLIB32.H > < FWLIB32.BAS > < FWLIB32.CS > < FWLIB32.VB >
< FWSYMBOL.H >
< FWLIB64.LIB > < FWLIB64.H >
  Copy to the appropriate folder of your application development.

< FWPMCALM.INI >
  Copy to the application folder or the Windows system folder.

< FCA32.DLL >
  Copy to the appropriate folder of your application can access at run-time.

< FCA32.LIB > < FCA32.H > < FCA32.BAS >
  Copy to the appropriate folder of your application development.


==========================================================================
[6] Redistributable software
==========================================================================

  You can redistribute the required drivers and run-time modules with
  your application.

  The drivers and run-time modules which you can redistribute have the
  following extensions in the file names.

        *.EXE, *.DLL, *.SYS, *.VXD, *.DEF, *.INI, *.CPL, *.INF, *.CAT


==========================================================================
[7] History
==========================================================================

    Please refer to the "Update History" of specification(html format) of
    data window library.

