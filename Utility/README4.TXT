
                               README4.TXT File
                     README file for Option Setup Utility
       Copyright(C), FANUC CORPORATION, 1997-2011. All Rights Reserved.

This document provides the information about "Option Setup Utility".
Please read this document before using the SOFTWARE.

------------------------------------------------------------------------------
[1] Purpose
[2] Supported CNC
[3] Execution
[4] Operation
------------------------------------------------------------------------------


==============================================================================
[1] Purpose
==============================================================================

This software "Option Setup Utility" is provided to set NC options from a
personal computer on the personal computer function system.


==============================================================================
[2] Supported CNC
==============================================================================

You can use this utility on the following CNCs.
    FS16-Model B/C
    FS18-Model B/C
    FS21-Model B
    FS16i-Model A/B
    FS18i-Model A/B
    FS21i-Model A/B
    FS16i/18i-W
    FS0i-Model A/B
    Power Mate i-Model D/H
This utility cannot be used on FS15-B, FS15i, FS30i/31i/32i/35i, FS0i-D,
Power Mate D/H and Power Motion i-A.

This utility cannot be used with "NC which corresponds to the function for the
setting of option parameter" and "the remote option setting function".


==============================================================================
[3] Execution
==============================================================================

This utility uses the Data Window Library (FWLIB32.DLL, FWLIB160.DLL, 
FWLIB16W.DLL, FWLIB0i.DLL, FWLIB0iB.DLL, FWLIBPMi.DLL) at run-time. 
So, confirm that these DLLs have been installed in your hard disk.

You can start this utility by the following procedure.
  1) Run "Optset.EXE" from this CD.
  or
  2) Copy "Optset.EXE" from this CD to a proper folder of a hard disk
     and then run it.


==============================================================================
[4] Operation
==============================================================================

"Option Setup Utility" window shows the following items.
    Node         : Node number which is selected now.
    Path         : Path which is selected now.
    No.          : Parameter Number which is selected now.
    Current Value: Current value of the above parameter number.
                   When this value consists of bit data, this column shows all
                   8 bits.
    Input Value  : Edit box in order to input new data.

First you choose [Node], [Path], and [No.], then enter new data in
[Input Value] and click [Setting] button.
The new data which is entered in [Input Value] must be 8 figures or less, and
must be bit data when [No.] is 9920 or more, or WORD type data (between -32768
and 32767) when [No.] is smaller than 9920.

When you change CNC option setting, CNC system must be powered off and on to
make this change effective.

