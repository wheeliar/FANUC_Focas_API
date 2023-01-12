
                             README2.TXT  File
                    README file for FANUC HSSB driver

       (C) Copyright FANUC CORPORATION, 1996-2014, All Rights Reserved.


This document provides the information about the HSSB driver.
Please read this document before using the software in this disk.

--------------------------------------------------------------------------
[1] File summary
[2] Installation (Windows 95/98)
[3] Installation (Windows NT)
[4] Installation (Windows 2000)
[5] Installation (Windows XP)
[6] Installation (Windows Vista/7 32-bit Edition)
[7] Installation (Windows 7 64-bit Edition)
[8] Installation (Upgrade to Windows 2000 from Windows 95/98/NT)
[9] Update method (Common)(except Windows Vista/7 32-bit Edition)
[10] Update method (Windows 95/98)
[11] Update method (Windows NT)
[12] Update method (Windows 2000)
[13] Update method (Windows XP)
[14] Update method (Windows Vista/7 32-bit Edition)
[15] Update method (Windows 7 64-bit Edition)
[16] Delete HSSB Driver (Windows 95/98)
[17] Delete HSSB Driver (Windows NT)
[18] Delete HSSB Driver (Windows 2000)
[19] Delete HSSB Driver (Windows XP)
[20] Delete HSSB Driver (Windows Vista/7 32-bit Edition)
[21] Delete HSSB Driver (Windows 7 64-bit Edition)
[22] Change in the registry information (Windows 95/98)
--------------------------------------------------------------------------


==========================================================================
[1] File summary
==========================================================================

    <HSSB> folder contains the HSSB driver.

<HSSB>\NCBOOT32.DOC  :  English document.
<HSSB>\NCBOOT32J.DOC :  Japanese document.
    Operation manual for NCBOOT32.EXE.
    Open these documents by Microsoft Word 97 or later.
    When you used the Wordpad, the document files might be not open.


<HSSB>\UPDATE.EXE : Utility program to update the HSSB driver.


<HSSB\WIN95> : Windows 95/98 driver.

<HSSB\WIN95>\HSSB.INF :
    Device information file for HSSB driver.  This is used by "Add New
    Hardware Wizard".

<HSSB\WIN95>\MMCNCD.VXD :
    Virtual device driver to run libraries provided by FANUC on Windows.

<HSSB\WIN95>\HSSBUI.DLL :
    Setting program for HSSB driver.


<HSSB\NT40> : Windows NT 4.0 driver.

<HSSB\NT40>\HSSB.INF :
    Device information file for HSSB driver.  This is used at the installation.

<HSSB\NT40>\MMCNCD.SYS :
    Device driver to run libraries provided by FANUC on Windows NT.

<HSSB\NT40>\HSSB2.CPL :
    The Control Panel applet to set the HSSB driver configuration.

<HSSB\NT40>\UNHSSB.INF :
    Device information file for HSSB driver.  This is used at the 
    uninstallation.


<HSSB\WIN2K> : Windows 2000 driver.

<HSSB\WIN2K>\HSSB.INF :
    Device information file for HSSB driver.  This is used at the installation.

<HSSB\WIN2K>\MMCNCD.SYS :
    Device driver to run libraries provided by FANUC on Windows 2000.

<HSSB\WIN2K>\HSSB2.CPL :
    The Control Panel applet to set the HSSB driver configuration.


<HSSB\WINXP> : Windows XP driver.

<HSSB\WINXP>\HSSB.INF :
    Device information file for HSSB driver.  This is used at the installation.

<HSSB\WINXP>\MMCNCD.SYS :
    Device driver to run libraries provided by FANUC on Windows XP.

<HSSB\WINXP>\HSSB2.CPL.MANIFEST :
    This file is used to construct Visual Style of Windows XP onto the Control
    Panel Applet for HSSB driver.


<HSSB\COMMON> : Common files for Windows 95/98/NT/2000 and XP.

<HSSB\COMMON>\NCBOOT32.EXE :
    HSSB driver support utility, which is resident on System tray and provides
    the following functions.
        - BOOT function (Maintain the CNC system file, Backup the SRAM, etc.)
        - IPL menu of CNC
        - Display of CNC power-on screen
        - Display of CNC alarm screen
        - Reconnection on the communication failure
        - Start up the registered application program using data window library


<HSSB\VISTA> : Windows Vista/7 32-bit Edition driver.

<HSSB\VISTA>\HSSB.INF :
    Device information file for HSSB driver.  This is used at the installation.

<HSSB\VISTA>\MMCNCD.SYS :
    Device driver to run libraries provided by FANUC on Windows Vista/7 32-bit
    edition.

<HSSB\VISTA>\MMCNCD.CAT :
    This is the certificate of the device driver.

<HSSB\VISTA>\HSSBSET.EXE :
    The Control Panel applet to set the HSSB driver configuration.

<HSSB\VISTA>\NCBOOT32.EXE
    It is the same file as <HSSB\COMMON>\NCBOOT32.EXE.

<HSSB\VISTA>\DelDrv.EXE
    This is used at the uninstallation.


<HSSB\WIN7_64> : Windows 7 64-bit Edition driver.

<HSSB\WIN7_64>\HSSB.INF :
    Device information file for HSSB driver.
    This is used at the installation.

<HSSB\WIN7_64>\MMCNCD.SYS :
    Device driver to run libraries provided by FANUC on Windows 7.

<HSSB\Win7_64>\MMCNCD.CAT :
    This is the certificate of the device driver.

<HSSB\WIN7_64>\HSSBSET.EXE :
    The Control Panel applet to set the HSSB driver configuration.

<HSSB\WIN7_64>\NCBOOT32.EXE
    It is the same file as <HSSB\COMMON>\NCBOOT32.EXE.


==========================================================================
[2] Installation (Windows 95/98)
==========================================================================

 (Note) The FS30i/31i/32i/35i, PMi-A, FS0i-D and FS0i-F are not supported by
        the HSSB driver for Windows 95/98.
 (Note) The HSSB interface board for PCI Express is not supported by the HSSB
        driver for Windows 95/98.

    In case of HSSB multi-connection, determine correspondence between nodes
    and connected CNCs.
    When two or more HSSB interface boards are used, each node is sequentially
    allotted from 0 to each board as follows.

     - When the ISA HSSB interface board and the PCI HSSB board are also
       installed, first, the nodes are allotted to the ISA HSSB interface
       board, then, the nodes are allotted to the PCI HSSB interface board.

     - When two or more PCI HSSB interface boards are installed, the nodes are
       allotted first from the PCI HSSB interface board inserted in lower PCI
       slot number.

    Next, do the following according to the kind of the HSSB interface board.
     - For the ISA HSSB interface board
       Decide the I/O port address to be allocated on the HSSB board and
       set the rotary switch on the HSSB board. (In case of PC integrated type
       i-series, Intelligent Terminal type 2 which do not have the ISA
       extension unit, I/O port address is fixed at 280H-28BH.)

     - For the PCI HSSB interface board
       Nothing need to be done. (The I/O port address is automatically set
       by the HSSB driver. FANUC PANEL i and CNC Display Unit with Personal
       Computer Function are also included in this case.)


    To install the HSSB driver to your system, use "Update Device Driver
    Wizard" or "Add New Hardware Wizard".

    There are following two methods of installation procedures for the driver.

        A.  Method by the automatic detection by Windows
        B.  Method by the manual operation


-------------------------------------------------------------------------------
 A. Method by the automatic detection by Windows

 1. When Windows is started, the PCI HSSB interface board is detected as
    "PCI Communication Device".
        [Update Device Driver Wizard] window is displayed.

     (When the PCI HSSB interface board is not detected automatically by
      Windows, install the driver according to following procedure,
     "B. Method by the manual operation".)

 2. Click [Next] button.
        "Windows was unable to locate a driver for this device" message is
        displayed.

 3. Click [Other Locations...] button. Set this CD onto the CD-ROM drive.

 4. Enter "d:\hssb\win95" on [Location] box, then click [OK] button.
    ("d:" is the drive letter for the CD-ROM.  If other drive letter is
    assigned to your drive, change it properly.)
        "Windows found the following updated driver for this device"
        message is displayed.

 5. Click [Finish] button.
        "Please insert the disk labeled "FANUC Open CNC Drivers and
        Libraries Disk #1"and the click [OK]." message is displayed.

 6. Click [OK] button.
        [Copying Files...] window is displayed.

 7. Enter "d:\hssb\win95" on [Copy files from] box, then click [OK] button.
        Files are copied.

        The installation is automatically done if there are two or more PCI
        HSSB interface boards.

        Afterwards, Windows is restarted.

 8. Click [Start] button.
        [Start] menu is displayed.

 9. Point [Setting], then click [Control Panel].
        [Control Panel] window is displayed.

10. Double-click [System] of [Control Panel].
        [System Properties] window is displayed.

11. Click [Device Manager] tab.

12. Double-click [FANUC HSSB Type 2 (PCI ? Channel)] (? is 1 or 2) of
    [FANUC Open CNC].
        [FANUC HSSB Type 2 (PCI ? Channel)] window is displayed.

13. Click [Settings] tab.

14. Enter proper name which indicates the machine connected to the HSSB board
   (for example, "FS160 - No.1", "Milling Machine") to [Node name] within 19
   characters.

15. Select connecting CNC type from [CNC type] and click it.

16. Click [OK] button.
        [System Setting Change] window is displayed, and "Do you want to
        shut down your computer now?" message is displayed.

17. When setting to the PCI HSSB interface board is required, click [No] and
    go back to step 12.
    Sets the all PCI HSSB interface boards.

    When you install the driver for the ISA HSSB interface board succeedingly,
    click [No] button in [System Setting Change] window.
    Install the driver for the ISA HSSB interface board according to
    "B. Method by the manual operation".

    When the installation on all the HSSB interface boards finishes, click
    [Yes] button in [System Setting Change] window.


-------------------------------------------------------------------------------
 B. Method by the manual operation

 1. Click [Start] button.
        [Start] menu is displayed.

 2. Point [Setting], then click [Control Panel].
        [Control Panel] window is displayed.

 3. Double-click [Add New Hardware].
        [Add New Hardware Wizard] window is displayed.

 4. Click [Next] button.
        "Do you want Windows to search for your new hardware?" message is
        displayed.

 5. Click [No], then click [Next] button.

 6. When the item of [FANUC Open CNC] is not displayed on [Hardware types]
    list box, click [Other devices], then click [Next] button.

    When the item of [FANUC Open CNC] is displayed on [Hardware types] list
    box, click [FANUC Open CNC], then click [Next] button.
        [Models] list box is displayed.

        When the following [Models] lists are displayed, go step 9.
          FANUC HSSB Type 2 (ISA 1 Channel)
          FANUC HSSB Type 2 (ISA 2 Channel)
          FANUC HSSB Type 2 (PCI 1 Channel)
          FANUC HSSB Type 2 (PCI 2 Channel)

        When the following [Models] list is displayed, go step 7.
          HSSB Type 2

 7. Click [Have Disk] button. Set this CD onto the CD-ROM drive.

 8. Enter "d:\hssb\win95" on [Copy manufacturer's files from] box, then
    click [OK] button. ("d:" is the drive letter for the CD-ROM.  If other
    drive letter is assigned to your drive, change it properly.)
        The following [Models] list box is displayed.
          FANUC HSSB Type 2 (ISA 1 Channel)
          FANUC HSSB Type 2 (ISA 2 Channel)
          FANUC HSSB Type 2 (PCI 1 Channel)
          FANUC HSSB Type 2 (PCI 2 Channel)

 9. Click the item corresponding to the HSSB interface board which should be
    used.
    In case of PC integrated type i-series, Intelligent Terminal type 2, select
    "FANUC HSSB Type 2 (ISA 1 Channel)".
    In case of FANUC PANEL i and CNC Display Unit with Personal Computer
    Function, select "FANUC HSSB Type 2 (PCI 1 Channel)".

10. Click [Next] button.
        "Windows can install your hardware, using the following settings"
        message is displayed.

11. Click [Next] button
        After copying files from the floppy disk, "Windows has finished
        installing the software necessary to support your new hardware."
        message is displayed.

12. Click [Finish] button.
        [System Setting Change] window is displayed, and "Do you want to
        shut down your computer now?" message is displayed.

13. Click [No] button. Remove this CD from the CD-ROM drive.

14. Double-click [System] of [Control Panel].
        [System Properties] window is displayed.

15. Click [Device Manager] tab.

16. Double-click [FANUC HSSB Type 2 (??? ? Channel)] ('??? ?' is 'ISA 1',
    'ISA 2', 'PCI 1' or 'PCI 2') of [FANUC Open CNC].
        [FANUC HSSB Type 2 (??? ? Channel) Properties] window is displayed.

17. Click [Settings] tab.

18. Enter proper name which indicates the machine connected to the HSSB board
   (for example, "FS160 - No.1", "Milling Machine") to [Node name] within 19
   characters.

19. Select connecting CNC type from [CNC type] and click it.

20. In case of the setting to the PCI HSSB interface board, go step 25.

21. Click [Resources] tab.
        [Resources] list is displayed.

22. Proceed to step 25 if the displayed I/O port address is the same with one
    which you decided previously.

23. Click [Change Setting] button.
        [Edit Input/Output Range] window is displayed.

24. Change [Value] with new address which you decided previously, then click
    [OK] button.

25. Click [OK] button.
        [System Setting Change] window is displayed, and "Do you want to
        shut down your computer now?" message is displayed.

26. If you install the driver for the next HSSB interface board, click [No]
    button and go back to step 3.

    If the installation on all the HSSB interface boards finishes,
    click [Yes] button.

 (Note) Differ from 16 bit version of driver, this driver does not require
        those 16 bit drivers.  If there are drivers of previous version
        (such as HSSBSYS.EXE, ITSYS.EXE, NCBOOT.EXE) in CONFIG.SYS or
        AUTOEXEC.BAT, they are removed automatically on the installation.

        If the NCALARM.EXE is registered to the startup folder, please
        delete it manually.

==========================================================================
[3] Installation (Windows NT)
==========================================================================

 (Note) The FS30i/31i/32i/35i, PMi-A, FS0i-D and FS0i-F are not supported by
        the HSSB driver for Windows NT.
 (Note) The HSSB interface board for PCI Express is not supported by the HSSB
        driver for Windows NT.

    In case of HSSB multi-connection, determine the kind  of the HSSB
    interface board (ISA/PCI) used by each node and correspondence between
    nodes and connected CNCs.
    Next, do the following according to the kind of the HSSB interface board.

     - For the ISA HSSB interface board
       Decide the I/O port address to be allocated on the HSSB board and
       set the rotary switch of the HSSB board. (In case of PC integrated type
       i-series, Intelligent Terminal type 2 which do not have the ISA
       extension unit, I/O port address is fixed at 280H-28BH.)

     - For the PCI HSSB interface board
       Nothing need to be done. (The I/O port address is automatically set
       by the HSSB driver. FANUC PANEL i and CNC Display Unit with Personal
       Computer Function are also included in this case.)

    When two or more PCI HSSB interface boards are installed, the nodes are
    allotted first from the PCI HSSB interface board inserted in lower PCI
    slot number.


    To install the HSSB driver to your system, use the Windows NT Explorer.
    The installation procedure of the driver is as follows.

 1. Logon to the Windows NT as the Administrator.

 2. Close all application programs. You must close the Control Panel if it
    is running.

 3. Run the Windows NT Explorer.

 4. Set this CD onto the CD-ROM drive and open \Hssb\Nt40 folder at CD-ROM by
    the Windows NT Explorer.

 5. Right-click the HSSB.INF file and click [install] on the pop up menu.
        Installer will copy driver files and register them to the registry.

    Next, setup HSSB driver by the Control Panel. Remove the CD from the
    CD-ROM drive.

 6. Run the Control Panel and double-click the HSSB icon.

 7. Select the node number that you want to attach the HSSB board and click
    [Setting...] button.

 8. Enter proper name which indicates the machine connected to the HSSB board
    (for example, "FS160 - No.1", "Milling Machine") to [Node name] within 19
    characters.

 9. Select CNC type from the [CNC Type] list box.

10. Select I/O port address according to the kind of the HSSB board.
     - For the ISA HSSB interface board
       Select I/O port address that correspond to the rotary switch on
       the HSSB board, from the [I/O Port Address] list box.

     - For the PCI HSSB interface board
       Select the [Use PCI] from the [I/O Port Address] list box.
       (The I/O port address is automatically set by the driver.)

11. Click [OK] button to close this window.

12. In case of multiple HSSB connection, go back to step 7. and repeat these
    steps for each HSSB board.

    If a node is not attached to HSSB board, select "Disable" from the
    [I/O Port Address] list box.

    After driver installation, I/O port address is set to 0x280 for node-0,
    "Disable" for other nodes as default value.

13. Click [Close] button.
        A dialogue window which asks you to restart Windows, will open.

14. Click [YES] button to restart Windows NT.


 (Note) To change HSSB driver setting, you must have the Administrator
        privilege.


==========================================================================
[4] Installation (Windows 2000)
==========================================================================

 (Note) The HSSB interface board for PCI Express is not supported by the HSSB
        driver for Windows 2000.

    In case of HSSB multi-connection, determine the kind of the HSSB
    interface board (ISA/PCI) used by each node and correspondence between
    nodes and connected CNCs.
    Next, do the following according to the kind of the HSSB interface board.

     - For the ISA HSSB interface board
       Decide the I/O port address to be allocated on the HSSB board and
       set the rotary switch of the HSSB board. (In case of PC integrated type
       i-series, Intelligent Terminal type 2 which do not have the ISA
       extension unit, I/O port address is fixed at 280H-28BH.)

     - For the PCI HSSB interface board
       Nothing need to be done. (The I/O port address is automatically set
       by the HSSB driver. FANUC PANEL i and CNC Display Unit with Personal
       Computer Function are also included in this case.)

    When two or more PCI HSSB interface boards are installed, the nodes are
    allotted first from the PCI HSSB interface board inserted in lower PCI
    slot number.


    To install the HSSB driver to your system, use the "Found New Hardware
    Wizard" or "Add/Remove Hardware".
    There are following two methods of installation procedures for the driver.

        A.  Method by the automatic detection by Windows (Only for PCI HSSB 
            interface board)
        B.  Method by the manual operation

-------------------------------------------------------------------------------
 A.  Method by the automatic detection by Windows (Only for PCI HSSB interface
     board)

 1. When Windows is started, the PCI HSSB interface board is detected as "PCI
    Simple Communications Controller".
        [Found New Hardware Wizard] window is displayed.

     (When the PCI HSSB interface board is not detected automatically by
      Windows, install the driver according to following procedure,
     "B. Method by the manual operation".)

 2. Click [Next] button.
        [Install Hardware Device Drivers] screen is displayed.

 3. Select "Search for a suitable driver for my device(recommended)" 
    and click [Next] button.
        [Locate Driver Files] screen is displayed.

 4. Set this CD onto the CD-ROM drive.
    Check only "Specify a location" of Optional search locations, then
    click [Next] button.

 5. Enter "d:\hssb\win2k" on [Copy manufacturer's files from] box, then 
    click [OK] button.("d:" is the drive letter for the CD-ROM.  If other
    drive letter is assigned to your drive, change it properly.)

 6. The following message is displayed on [Driver Files Search Results].
    Click [Next] button.

        Windows found a driver for this device. To install the driver 
        Windows found, click Next.
            d:\hssb\win2k\hssb.inf

 7. Click [Next] button.
        Files are copied.

 8. [Completing the Found New Hardware Wizard] is displayed.
    Click [Finish] button.

         The installation is automatically done if there are two or more PCI
        HSSB interface boards.

    Next, setup HSSB driver by the Control Panel. Remove this CD from the
    CD-ROM drive.

 9. Run the Control Panel and double-click the HSSB icon.

10. Select the node number that you want to attach the HSSB board and click
    [Setting...] button.

11. Enter proper name which indicates the machine connected to the HSSB board
    (for example, "FS160 - No.1", "Milling Machine") to [Node name] within 19
    characters.

12. Select CNC type from the [CNC Type] list box.

13. Click [OK] button to close this window.

14. In case of multiple HSSB connection, go back to step 10. and repeat these
    steps for each HSSB board.

15. Click [Close] button.
        A dialogue window which asks you to restart Windows, will open.

16. Click [Yes] button to restart Windows 2000.


-------------------------------------------------------------------------------
 B. Method by the manual operation

 1. Click [Start] button.
        [Start] menu is displayed.

 2. Point [Setting], then click [Control Panel].
        [Control Panel] window is displayed.

 3. Double-click [Add/Remove Hardware].
        [Welcome to the Add/Remove Hardware Wizard] screen of [Add/Remove
        Hardware Wizard] window is displayed.

 4. Click [Next] button.
        [Choose a Hardware Task] screen is displayed.

 5. Select "Add/Troubleshoot a device", then click [Next] button.
        [Choose a Hardware Device] screen is displayed.

 6. Click [Add a new device] from [Devices] list box, then click [Next]
    button.
        [Find New Hardware] screen is displayed, then following message is
        displayed.
            Do you want Windows to search for your new hardware?

 7. Select "No, I want to select the hardware from a list", then click
    [Next] button.
        [Hardware Type] screen is displayed.

 8. When the item of [FANUC Open CNC] is not displayed on [Hardware types]
    list box, click [Other devices], then click [Next] button.
        [Select a Device Driver] screen is displayed.

    When the item of [FANUC Open CNC] is displayed on [Hardware types] list
    box, click [FANUC Open CNC], then click [Next] button.
    Proceed to step 11.

 9. Click [Have Disk...] button, then set this CD onto the CD-ROM drive.

10. Enter "d:\hssb\win2k" on [Copy manufacturer's files from] box, then
    click [OK] button.  ("d:" is the drive letter for the CD-ROM.  If other
    drive letter is assigned to your drive, change it properly.)

11. [Select a Device Driver] screen is displayed.
        The following [Models] list box is displayed.
          FANUC HSSB Type 2 (ISA 1 Channel)
          FANUC HSSB Type 2 (ISA 2 Channel)
          FANUC HSSB Type 2 (PCI 1 Channel)
          FANUC HSSB Type 2 (PCI 2 Channel)
          FANUC HSSB3 (PCI 1 Channel)
          FANUC HSSB3 (PCI 2 Channel)

12. Click the item corresponding to the HSSB interface board which should be
    used.
    In case of PC integrated type i-series, Intelligent Terminal type 2, select
    "FANUC HSSB Type 2 (ISA 1 Channel)".
    In case of FANUC PANEL i and CNC Display Unit with Personal Computer
    Function, select "FANUC HSSB Type 2 (PCI 1 Channel)".
    In case of FS30i/31i/32i/35i, PMi-A, FS0i-D and FS0i-F, select
    "FANUC HSSB3 (PCI 1 Channel)".

13. Click [Next] button.

14. The following message is displayed by [Add/Remove Hardware Wizard].

        Windows could not detect the settings of the device. To use this
        device, you must enter its hardware settings. Consult the 
        documentation that came with this device for information.

15. Click [OK] button.
        [Add New hardware Wizard Properties] window is displayed.

16. Click [Input/output Range] of [Resource settings] list box, then click
    [Change Setting...]  button.
        [Edit Input/Output Range] window is displayed.

17. Change the Input/Oouput Range displayed  in "Value" box with one which
    you decided previously, then click [OK] button.
        Return to [Add New Hardware Wizard Properties] screen.

18. Click [OK] button.
        [Start hardwareInstallation] screen is displayed.

19. Click [Next] button.
        After copying files from the floppy disk, [Completing the Add/remove
        Hardware Wizard] screen is displayed.

20. Click [Finish] button.
        [System Setting Change] window is displayed, then the following
        message is displayed.

            You must restart your computer before the new settings will take
            effect. Do you want to restart your computer now?

21. Click [Yes] button to restart Windows 2000.


    Next, setup HSSB driver by the Control Panel. Remove this CD from the
    CD-ROM drive.

22. Run the Control Panel and double-click the HSSB icon.

23. Select the node number that you want to attach the HSSB board and click
    [Setting...] button.

24. Enter proper name which indicates the machine connected to the HSSB board
    (for example, "FS160 - No.1", "Milling Machine") to [Node name] within 19
    characters.

25. Select CNC type from the [CNC Type] list box.

26. Click [OK] button to close this window.

27. In case of multiple HSSB connection, go back to step 10. and repeat these
    steps for each HSSB board.

28. Click [Close] button.
        A dialogue window which asks you to restart Windows, will open.

29. Click [Yes] button to restart Windows 2000.


 (Note) To change HSSB driver setting, you must have the Administrator
        privilege.


==========================================================================
[5] Installation (Windows XP)
==========================================================================

    In Windows XP, the PCI HSSB interface board and
    the HSSB interface board for PCI Express are supported.

    When two or more PCI HSSB interface boards are installed, the nodes are
    allotted first from the PCI HSSB interface board inserted in lower PCI
    slot number.

    There are following two methods of installation procedures for the driver.

        A.  Method by the automatic detection by Windows
        B.  Method by the manual operation

-------------------------------------------------------------------------------
 A.  Method by the automatic detection by Windows

 1. When Windows is started, the PCI HSSB interface board is detected as "PCI
    Simple Communications Controller".
        [Found New Hardware Wizard] window is displayed.

 2. Select [Install from a list or specific location(advanced)], then click
    [Next] button.
        [Please choose your search and installation options] screen is
        displayed.

 3. Select [Search for the best driver in these locations], then check
    [include this location in the search] only.

 4. Set this CD onto the CD-ROM drive.

 5. Click [Browse] button, then select "d:\hssb\winxp" and click [Next] button.
    ("d:" is the drive letter for the CD-ROM.  If other drive letter is
    assigned to your drive, change it properly.)
        Files are copied.

 6. [Completing the Found New Hardware Wizard] is displayed.
    Click [Finish] button.

         The installation is automatically done if there are two or more PCI
        HSSB interface boards.

    Next, setup HSSB driver by the Control Panel. Remove the disk from the
    floppy drive.

 7. Click [Start] button, then click [Control Panel].

 8. Click [Other Control Panel Options] in the [See Also].
        [HSSB] icon is displayed, then click this.

 9. Select the node number that you want to attach the HSSB board and click
    [Setting...] button.

10. Enter proper name which indicates the machine connected to the HSSB board
    (for example, "FS160 - No.1", "Milling Machine") to [Node name] within 19
    characters.

11. Select CNC type from the [CNC Type] list box.

12. Click [OK] button to close this window.

13. In case of multiple HSSB connection, go back to step 9. and repeat these
    steps for each HSSB board.

14. Click [Close] button.


-------------------------------------------------------------------------------
 B. Method by the manual operation

 1. Click [Start] button.
        [Start] menu is displayed.

 2. Point [My Computer], then right-click this. And click [Properties] from menu.
        [System Properties] screen is displayed.

 3. Click [Hardware] tab, then click [Device Manager] button.
        [Device Manager] screen is displayed.

 4. Right-click the [PCI Simple Communications Controller] in [Other devices],
    then click [Properties] from popup menu.
        [PCI Simple Communications Controller Propertied] screen is displayed.

 5. Click [Driver] tab, then click [Update Driver...] button.
        [Haedware Update Wizard] screen is displayed.

 6. Select [Install from a list or specific location(advanced)], then click
    [Next] button.
        [Please choose your search and installation options] screen is
        displayed.

 7. Select [Search for the best driver in these locations], then check
    [include this location in the search] only.

 8. Set this CD onto the CD-ROM drive.

 9. Click [Browse] button, then select "d:\hssb\winxp" and click [Next] button.
    ("d:" is the drive letter for the CD-ROM.  If other drive letter is
    assigned to your drive, change it properly.)
        Files are copied.

10. [Completing the Found New Hardware Wizard] is displayed.
    Click [Finish] button.

11. Click [Close] button in [FANUC HSSB Type 2 (PCI X Channel) Propreties]
    screen.

12. Close [Device Manager] screen, then close [System Properties] screen.


    Next, setup HSSB driver by the Control Panel. Remove this CD from the
    CD-ROM drive.

13. Click [Start] button, then click [Control Panel].

14. Click [Other Control Panel Options] in the [See Also].
        [HSSB] icon is displayed, then click this.

15. Select the node number that you want to attach the HSSB board and click
    [Setting...] button.

16. Enter proper name which indicates the machine connected to the HSSB board
    (for example, "FS160 - No.1", "Milling Machine") to [Node name] within 19
    characters.

17. Select CNC type from the [CNC Type] list box.

18. Click [OK] button to close this window.

19. In case of multiple HSSB connection, go back to step 15. and repeat these
    steps for each HSSB board.

20. Click [Close] button.


==========================================================================
[6] Installation (Windows Vista/7 32-bit Edition)
==========================================================================

    In Windows Vista/7 32-bit Edition, the PCI HSSB interface board and
    the HSSB interface board for PCI Express are supported.

    When two or more PCI HSSB interface boards are installed, the nodes are
    allotted first from the PCI HSSB interface board inserted in lower PCI
    slot number.

    There are following two methods of installation procedures for the driver.

        A.  Method by the automatic detection by Windows
        B.  Method by the manual operation

-------------------------------------------------------------------------------
 A.  Method by the automatic detection by Windows

 1. When Windows is started, the PCI HSSB interface board is detected as "PCI
    Simple Communications Controller".
        [Found New Hardware] window is displayed.

 2. Click [Locate and install driver software(recommended)].

 3. The [User Account Control] window is displayed, then click [Continue]

 4. [Found New Hardware - PCI Simple Communications Controller] window is
    displayed.
        Click [Don't search online].

 5. [Insert the disc that came with your PCI Simple Communications Controller]
    message is displayed on [Found New Hardware - PCI Simple Communications
    Controller] window.
        Click [I don't have the disc. Show me other options]

 6. [Windows couldn't find driver software for your device] message is
    displayed on [Found New Hardware - PCI Simple Communications Controller]
    window.
        Click [Browse my computer for driver software(advanced)]

 7. Set this CD onto the CD-ROM drive.

 8. [Browse for driver software on your computer] message is displayed on
    [Found New Hardware - PCI Simple Communications Controller] window.
        Click [Browse] button of [Search for driver software in this location],
        then select "d:\hssb\vista" and click [Next]
        button. ("d:" is the drive letter for the CD-ROM.  If other drive
        letter is assigned to your drive, change it properly.)

 9. [Windows can't verify the publisher of this driver software] message is
    displayed on [Windows Security] window.
        Click [Install this driver software anyway]

10. Driver software is automatically installed, then [Found New Hardware - 
    FANUC HSSB xxxxx (xxxxx: This message is different by HSSB board type.)]
    window is displayed.
        After [The software for this device has been successfully installed]
        message is displayed, click [Close] button.

11. Remove this CD from the CD-ROM drive. 

12. Next, setup HSSB driver according to [C. HSSB setting].


-------------------------------------------------------------------------------
 B. Method by the manual operation

 1. Click [Start] button.
        [Start] menu is displayed.

 2. Point [Computer], then right-click this. And click [Properties] from menu.
        [System] window is displayed.

 3. Click [Device Manager] on [Tasks] list.

 4. The [User Account Control] window is displayed, then click [Continue]
    button.
        [Device Manager] window is diaplayed.

 5. Right-click the [PCI Simple Communications Controller] in [Other devices],
    then click [Properties] from popup menu.
        [PCI Simple Communications Controller Properties] window is displayed.

 6. Click [Driver] tab, then click [Update Driver...] button.
        [Update Driver Software - PCI Simple Communications Controller] window
        is displayed.

 7. Click [Browse my Computer for driver software].

 8. Set this CD onto the CD-ROM drive.

 9. Click [Browse] button of [Search for driver software in this location],
    then select "d:\hssb\vista" and click [Next] button.
    ("d:" is the drive letter for the CD-ROM.  If other drive letter is
    assigned to your drive, change it properly.)

10. [Windows can't verify the publisher of this driver software] message is
    displayed on [Windows Security] window.
        Click [Install this driver software anyway]

11. Driver software is automatically installed, then [Update Driver Software -
    FANUC HSSBxxxxx (xxxxx: the message is different by HSSB board type.)] 
    window is displayed.
        After [Windows has successfully updated your driver software] message is
        displayed, then click [Close] button.

12. Remove this CD from the CD-ROM drive. 

13. Next, setup HSSB driver according to [C. HSSB setting].


-------------------------------------------------------------------------------
 C. HSSB setting

 1. Click [Start] button, then click [Control Panel].

 2. Click [Hardware and Sound].
        [HSSB] icon is displayed, then click this.

 3. The [User Account Control] window is displayed, then click [Allow].

 4. Select the node number that you want to attach the HSSB board and click
    [Setting...] button.

 5. Enter proper name which indicates the machine connected to the HSSB board
    (for example, "FS160 - No.1", "Milling Machine") to [Node name] within 19
    characters.

 6. Select CNC type from the [CNC Type] list box.

 7. Click [OK] button to close this window.

 8. In case of multiple HSSB connection, go back to step 4. and repeat these
    steps for each HSSB board.

 9. Click [Close] button.


 (Note) To change HSSB driver setting, you must have the Administrator
        privilege.


==========================================================================
[7] Installation (Windows 7 64-bit Edition)
==========================================================================

    In Windows 7 64-bit Edition, the PCI HSSB interface board and
    the HSSB interface board for PCI Express are supported.

    When two or more PCI HSSB interface boards are installed, the nodes are
    allotted first from the PCI HSSB interface board inserted in lower PCI
    slot number.

    There are following two methods of installation procedures for the driver.

        A.  Method by the automatic detection by Windows
        B.  Method by the manual operation

-------------------------------------------------------------------------------
 A.  Method by the automatic detection by Windows

 1. When Windows is started, the PCI HSSB interface board is detected as "PCI
    Simple Communications Controller".
        [Found New Hardware] window wii be displayed.

 2. Click [Locate and install driver software(recommended)].

 3. [Found New Hardware - PCI Simple Communications Controller] window is
    displayed.
        Click [Don't search online].

 4. [Insert the disc that came with your PCI Simple Communications Controller]
    message is displayed on [Found New Hardware - PCI Simple Communications
    Controller] window.
        Click [I don't have the disc. Show me other options]

 5. [Windows couldn't find driver software for your device] message is
    displayed on [Found New Hardware - PCI Simple Communications Controller]
    window.
        Click [Browse my computer for driver software(advanced)]

 6. Set this CD to the CD-ROM drive.

 7. [Browse for driver software on your computer] message is displayed on
    [Found New Hardware - PCI Simple Communications Controller] window.
        Click [Browse] button of [Search for driver software in this location],
        then select "d(the drive letter for the CD-ROM):\hssb\WIN7_64" and 
        click [Next] button.

 8. Driver software is automatically installed, then [Found New Hardware - 
    FANUC HSSB xxxxx (xxxxx: a message that depends on the type of HSSB board)]
    window will be displayed.
        After [The software for this device has been successfully installed]
        message is displayed, click [Close] button.

 9. Remove this CD from the CD-ROM drive. 

10. Next, setup HSSB driver according to [C. HSSB setting].


-------------------------------------------------------------------------------
 B. Method by the manual operation

 1. Click [Start] button.
        [Start] menu will be displayed.

 2. Point [Computer], then right-click this. And click [Properties] on the menu.
        [System] window will be displayed.

 3. Click [Device Manager] on [Control Panel Home] list.

 4. Right-click [PCI Simple Communications Controller] in [Other devices], then
    click [Properties] on the popup menu.
        [PCI Simple Communications Controller Properties] window will be displayed.

 5. Click [Driver] tab, then click [Update Driver...] button.
        [Update Driver Software - PCI Simple Communications Controller] window
        will be displayed.

 6. Click [Browse my Computer for driver software].

 7. Set this CD to the CD-ROM drive.

 8. Click [Browse] button of [Search for driver software in this location],
    then select "d(the drive letter of the CD-ROM):\hssb\WIN7_64" and click 
    [Next] button.
 
 9. Driver software is automatically installed, then [Update Driver Software -
    FANUC HSSBxxxxx (xxxxx: a message that depends on the type of HSSB board)] 
    window will be displayed.
        After [Windows has successfully updated your driver software] message is
        displayed, then click [Close] button.

10. Remove this CD from the CD-ROM drive. 

11. Next, setup HSSB driver according to [C. HSSB setting].


-------------------------------------------------------------------------------
 C. HSSB setting

 1. Click [Start] button, then click [Control Panel].

 2. Click [Hardware and Sound].
        [HSSB] icon is displayed, then click this.

 3. The [User Account Control] window is displayed, then click [Allow].

 4. Select the node number that you want to attach the HSSB board and click
    [Setting...] button.

 5. Enter proper name which indicates the machine connected to the HSSB board
    (for example, "FS30i - No.1", "Milling Machine") to [Node name] within 19
    characters.

 6. Select CNC type from the [CNC Type] list box.

 7. Click [OK] button to close this window.

 8. In case of multiple HSSB connection, go back to step 4. and repeat these
    steps for each HSSB board.

 9. Click [Close] button.


 (Note) To change HSSB driver setting, you must have the Administrator
        privilege.


==========================================================================
[8] Installation (Upgrade to Windows 2000 from Windows 95/98/NT)
==========================================================================

    When Windows95/98 is upgraded to Windows 2000, an old HSSB driver 
    automatically becomes disabled.

    When you do the upgrade from windows NT to Windows 2000, delete the 
    old HSSB driver beforehand according to the following procedure, then
    do the upgrade to Windows 2000.

 1. Open [Device] in the Control Panel.

 2. Select "mmcncd" from list box, then click [Stop] button.

 3. Click [Startup] button and select [Disabled] in [Startup Type] group.

 4. Close the Control Panel.

 5. Set this CD onto the CD-ROM drive, then open \hssb\Nt40 folder at CD-ROM
    by explorer.

 6. Right-click the UNHSSB.INF file and click [install] on the pop up menu.
        The driver will be removed.

 7. Restart Windows 2000.


    Install the driver according to "[4] Installation (Windows 2000)" after
    doing the upgrade to Windows 2000.


==========================================================================
[9] Update method (Common)(except Windows Vista/7 32-bit Edition)
==========================================================================

 1. Set this CD onto the CD-ROM drive, then execute the "d:\hssb\update.exe".
    ("d:" is the drive letter for the CD-ROM.  If other drive letter is
    assigned to your drive, change it properly.)
    The dialog box opens, and the following question is displayed.

      Update the HSSB Driver
      Are you sure?

 2. If you update the HSSB driver, select "Yes".

 3. When the HSSB driver was normally updated, the following message is
    displayed.

      The HSSB Driver has been updated correctly.
      You will need to exit and restart Windows so that the new setting
      can take effect.
      Do you want to restart your computer now?

 Please update the HSSB driver according to the procedure of [9],[10],[11],[12]
 and [13] when failing in the update of the HSSB driver.


==========================================================================
[10] Update method (Windows 95/98)
==========================================================================

    To update the driver from 16 bit version, install this driver according
    to the section [2] of this document.

    To update the driver from previous 32 bit version, follow these steps.

 1. Terminate Ncboot32.exe.
    Right-click the icon on the system tray and click "End" on the pop up
    menu.

 2. Click [Start] button.
        [Start] menu is displayed.

 3. Point [Setting], then click [Control Panel].
        [Control Panel] window is displayed.

 4. Double-click [System] of [Control Panel].
        [System Properties] window is displayed.

 5. Click [Device Manager] tab.

 6. Click [HSSB Type 2] of [FANUC Open CNC].

 7. Click [Remove] button of [System Properties] window.
        [Confirm Device Removal] window is displayed.

 8. Click [OK] button.
        HSSB Type2 is deleted.

 9. If there are two or more HSSB Type2 devices, repeats from step 6.

10. If you add the PCI HSSB interface board, shut down the Windows and
    turn off the computer.
    Then insert the PCI HSSB interface board into the PCI slot.

11. Restart Windows and install this driver according to the section [2].


 (Note) The FS30i/31i/32i/35i, PMi-A, FS0i-D and FS0i-F are not supported by
        the HSSB driver for Windows 95/98.


==========================================================================
[11] Update method (Windows NT)
==========================================================================

 1. Terminate Ncboot32.exe.
    Right-click the icon on the system tray and click "End" on the pop up
    menu.

 2. Copy files.
    Copy \HSSB\COMMON\NCBOOT32.EXE and \HSSB\NT40\HSSB2.CPL to the Windows
    NT's system32 folder.
    Copy \HSSB\NT40\MMCNCD.SYS to the system32\drivers folder.

 3. Restart Windows NT.


 (Note) The FS30i/31i/32i/35i, PMi-A, FS0i-D and FS0i-F are not supported by
        the HSSB driver for Windows NT.


==========================================================================
[12] Update method (Windows 2000)
==========================================================================

 1. Terminate Ncboot32.exe.
    Right-click the icon on the system tray and click "End" on the pop up
    menu.

 2. Copy files.
    Copy \HSSB\COMMON\NCBOOT32.EXE and \HSSB\WIN2K\HSSB2.CPL to the Windows
    2000's system32 folder.
    Copy \HSSB\WIN2K\MMCNCD.SYS to the system32\drivers folder.

 3. Restart Windows 2000.


==========================================================================
[13] Update method (Windows XP)
==========================================================================

 1. Terminate Ncboot32.exe.
    Right-click the icon on the system tray and click "End" on the pop up
    menu.

 2. Copy files.
    Copy \HSSB\COMMON\NCBOOT32.EXE, \HSSB\WIN2K\HSSB2.CPL and 
    \HSSB|WINXP\HSSB2.CPL.MANIFEST to the Windows XP's system32 folder.
    Copy \HSSB\WIN2K\MMCNCD.SYS to the system32\drivers folder.

 3. Restart Windows XP.


==========================================================================
[14] Update method (Windows Vista/7 32-bit Edition)
==========================================================================

 1. Terminate Ncboot32.exe.
    Right-click the icon on the system tray and click "End" on the pop up
    menu.

 2. Copy files.
    Copy \HSSB\VISTA\NCBOOT32.EXE and \HSSB\VISTA\HSSBSET.EXE to the Windows
    Vista/7's system32 folder.
    Copy \HSSB\WIN2K\MMCNCD.SYS to the system32\drivers folder.

 3. Restart Windows Vista/7.


==========================================================================
[15] Update method (Windows 7 64-bit Edition)
==========================================================================

 1. Terminate Ncboot32.exe.
    Right-click the icon on the system tray and click "End" on the pop up
    menu.

 2. Click [Start] button.
        [Start] menu will be displayed.

 3. Point [Computer], then right-click this. And click [Properties] on the menu.
        [System] window will be displayed.

 4. Click [Device Manager] on [Control Panel Home] list.

 5. Right-click [FANUC HSSBxxxxx (xxxxx: a message that depends on the type
    of HSSB board)] in [FANUC Open CNC], then click [Update Driver...] on the
    popup menu.
        [Update Driver Software - FANUC HSSBxxxxx] window will be displayed.

 6. Click [Browse my Computer for driver software].

 7. Set this CD to the CD-ROM drive.

 8. Click [Browse] button of [Search for driver software in this location],
    then select "d(the drive letter of the CD-ROM):\hssb\WIN7_64" and click 
    [Next] button.

 9. [Windows can't verify the publisher of this driver software] message is
    displayed on [Windows Security] window.
        Click [Install this driver software anyway]

10. Driver software is automatically installed, then [Update Driver Software -
    FANUC HSSBxxxxx] window will be displayed.
        After [Windows has successfully updated your driver software] message 
        is displayed, then click [Close] button.

11. Remove this CD from the CD-ROM drive. 

12. [System Setting Change] window is displayed.
        Click [Yes] button, then restart Windows 7.


==========================================================================
[16] Delete HSSB Driver (Windows 95/98)
==========================================================================

 1. Open [System] -> [Device Manager] in the Control Panel.

 2. Delete all driver in [FANUC Open CNC] section.

 3. Execute REGEDIT.EXE.

 4. Remove "ncboot32.exe" in "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\
    CurrentVersion\Run" key by using REGEDIT.EXE.

 5. Restart PC.

 6. Delete mmcncd.vxd, hssbui.dll ncboot32.exe in Windows System folder.


==========================================================================
[17] Delete HSSB Driver (Windows NT)
==========================================================================

 1. Open [Device] in the Control Panel.

 2. Select "mmcncd" and click [Stop] button.

 3. Click [Startup] button and select [Disabled] in [Startup Type] group.

 4. Close the Control Panel.

 5. Terminate Ncboot32.exe.
    Right-click the icon[FANUC Open CNC] on the system tray and click "End" on
    the pop up menu.

 6. Set this CD onto the CD-ROM drive, then open the folder "d:\hssb\nt40"
    by explorer.
    ("d:" is the drive letter for the CD-ROM.  If other drive letter is
     assigned to your drive, change it properly.)

 7. Right-click the UNHSSB.INF file and click [install] on the pop up menu.
        The driver and the registry will be removed.

 8. Turn off PC, then remove the HSSB interface boards.


==========================================================================
[18] Delete HSSB Driver (Windows 2000)
==========================================================================

 1. Start Windows 2000. Never open Control Panel.

 2. Terminate Ncboot32.exe.
    Right-click the icon[FANUC Open CNC] on the system tray and click "End" on
    the pop up menu.

 3. Set this CD onto the CD-ROM drive, then open the folder "d:\hssb\win2k"
     by explorer.
    ("d:" is the drive letter for the CD-ROM.  If other drive letter is
     assigned to your drive, change it properly.)

 4. Right-click the UNHSSB.INF file and click [install] on the pop up menu.
        The driver and the registry will be removed.

 5. Open [System] in the Control Panel.

 6. Right-click [Hardware] tab, and continuously right-click
    [Device Manager...] button.

 7. Open the properties of all driver in [FANUC Open CNC] of device list. 
    And click [General] tab, then set "Do not use this device(disable)" in 
    [Device usage:].  Moreover, delete all these drivers.

 8. Turn off PC, then remove the HSSB interface boards.


 (Note) If the control panel was opened even once, the HSSB icon is not deleted. 


==========================================================================
[19] Delete HSSB Driver (Windows XP)
==========================================================================

 1. Start Windows XP. Never open Control Panel.
 
 2. Terminate Ncboot32.exe.
    Right-click the icon[FANUC Open CNC] on the system tray and click "End" on
    the pop up menu.

 3. Set this CD onto the CD-ROM drive, then open the folder "d:\hssb\winxp"
    by explorer.
    ("d:" is the drive letter for the CD-ROM.  If other drive letter is
     assigned to your drive, change it properly.)

 4. Right-click the UNHSSB.INF file and click [install] on the pop up menu.
        The driver and the registry will be removed.

 5. Click [Start] button.
        [Start] menu is displayed.

 6. Point [My Computer], then right-click. And click [Properties] from pulldown
    menu.
        [System Properties] screen is displayed.

 5. Open [System] in the Control Panel.

 7. Right-click [Hardware] tab, and continuously right-click
    [Device Manager...] button.

 8. Open the properties of all driver in [FANUC Open CNC] of device list. 
    And click [General] tab, then set "Do not use this device(disable)" in 
    [Device usage:].  Moreover, delete all these drivers.

 9. Turn off PC, then remove the HSSB interface boards.


 (Note) If the control panel was opened even once, the HSSB icon is not deleted. 


==========================================================================
[20] Delete HSSB Driver (Windows Vista/7 32-bit Edition)
==========================================================================

 1. Start Windows Vista/7. 

 2. Set this CD onto the CD-ROM drive, then open the folder "d:\hssb\vista"
    by explorer.
    ("d:" is the drive letter for the CD-ROM.  If other drive letter is
     assigned to your drive, change it properly.)

 3. Execute DelDrv.exe

 4. The [User Account Control] window is displayed, then click [Allow].

 5. Select [HSSB Driver] from [Select the kind of driver.], then click [Delete]
    button.

 6. Because there is a question of [HSSB Driver is deleted. Are you sure?],
    click [Yes] button.
        The driver and the registry will be removed.

 7. Click [Start] button.
        [Start] menu is displayed.

 8. Point [Computer], then right-click. And click [Properties] from pulldown
    menu.
        [System] screen is displayed.

 9. Click [Device Manager] on [Tasks] list.

10. The [User Account Control] window is displayed, then click [Continue]
    button.
        [Device Manager] window is diaplayed.

11. Open the properties of all driver in [FANUC Open CNC] of device list. 
    And click [driver] tab, then click [Disable] button. Moreover, uninstall
    all these drivers.

12. Turn off PC, then remove the HSSB interface boards.


==========================================================================
[21] Delete HSSB Driver (Windows 7 64-bit Edition)
==========================================================================

 1. Start Windows 7. 

 2. Click [Start] button.
        [Start] menu is displayed.

 3. Point [Computer], then right-click. And click [Properties] from pulldown
    menu.
        [System] screen is displayed.

 4. Click [Device Manager] on [Tasks] list.

 5. Open the properties of all driver in [FANUC Open CNC] of device list. 
    And click [driver] tab, then click [Disable] button. Moreover, uninstall
    all these drivers.

 6. Turn off PC, then remove the HSSB interface boards.


==========================================================================
[22] Change in the registry information (Windows 95/98)
==========================================================================

 The information of the node name for Windows95/98 registry has been
 changed as follows.

  - since Edition 1.6 or later

   HKEY_LOCAL_MACHINE
    |
     System
     |
      CurrentControlSet
      |
       Services
       |
        Class
        |
         CNC
         |
          0000                     <- The software key for the first HSSB
         |                            interface board.
         |   NodeName0 "FS160-1"   <- The node name for channel 1.
         |   NodeName1 "FS160-2"   <- The node name for channel 2.
         |   *********
          0001                     <- The software key for the second HSSB
         |                            interface board.
         |   NodeName0 "FS150-1"   <- The node name for channel 1.
         |   NodeName1 "FS150-2"   <- The node name for channel 2.
         |   *********


 - until Edition 1.5 or former

   HKEY_LOCAL_MACHINE
    |
     Enum
     |
      Root
      |
       CNC
       |
        0000                     <- The hardware key for node 0.
       |   DeviceDesc "FS160-1"  <- The node name for node 0.
       |   *********
        0001                     <- The hardware key for node 1.
       |   DeviceDesc "FS160-2"  <- The node name for node 1.
       |   *********

