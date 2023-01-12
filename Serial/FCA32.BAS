'------------------------------------------------------------------------------
'
'   FCA32.BAS
'
'       Serial/FCA Library function declarations, constant definitions
'
'------------------------------------------------------------------------------
'   Copyright (C) 1996-2011 by FANUC CORPORAION All rights reserved.
'------------------------------------------------------------------------------

'---------------------
' constant definitions
'---------------------
' for rs_open, fca_setparam (ser_t)
Public Const FCA_BAUD_1200  As Long =  1200
Public Const FCA_BAUD_2400  As Long =  2400
Public Const FCA_BAUD_4800  As Long =  4800
Public Const FCA_BAUD_9600  As Long =  9600
Public Const FCA_BAUD_19200 As Long = 19200
Public Const FCA_BAUD_38400 As Long = 38400
Public Const FCA_BAUD_57600 As Long = 57600
Public Const FCA_BAUD_76800 As Long = 76800

Public Const DATA_7 As Long = 7
Public Const DATA_8 As Long = 8

Public Const PARITY_N As Long = 0
Public Const PARITY_E As Long = 1
Public Const PARITY_O As Long = 2

Public Const STOP_1 As Long = 1
Public Const STOP_2 As Long = 2

' for rs_buffer
Public Const RS_CHK_BUF_R As Long = 0
Public Const RS_CHK_BUF_W As Long = 1
Public Const RS_CLR_BUF_R As Long = 2
Public Const RS_CLR_BUF_W As Long = 3
Public Const RS_GET_BUF_R As Long = 4
Public Const RS_GET_BUF_W As Long = 5

' for rs_status
Public Const STS_CD_ON    As Long = &H0002
Public Const STS_PARITY   As Long = &H0008
Public Const STS_OVERRUN  As Long = &H0010
Public Const STS_FRAME    As Long = &H0020
Public Const STS_DR_ON    As Long = &H0080
Public Const STS_OVERFLOW As Long = &H0100
Public Const STS_EMPTY    As Long = &H0200
Public Const STS_R_STOP   As Long = &H0800
Public Const STS_BUF_FULL As Long = &H2000
Public Const STS_S_STOP   As Long = &H8000

'-----------------------
' user-defined data type
'-----------------------
' for rs_open, fca_setparam
Type SER_T
    lBaud     As Long
    lStopBit  As Long
    lParity   As Long
    lDataBit  As Long
    lHardflow As Long
    lDcEnable As Long
    lDcPut    As Long
    lDc1Code  As Long
    lDc2Code  As Long
    lDc3Code  As Long
    lDc4Code  As Long
End Type

' for fca_readdir
Type FCA_DIR
    byFileName(0 To 17) As Byte
    byFileSize(0 To 8)  As Byte
    byWrtProtect        As Byte
    byRecordCode        As Byte
    byVolNo(0 To 2)     As Byte
    byMultiVol          As Byte
End Type

'----------------------
' function declarations
'----------------------
Declare Function rs_open   Lib "fca32" Alias "rs_open"   (ByVal lPort As Long, ByRef Param As ser_t, ByVal sMode As String) As Long
Declare Function rs_close  Lib "fca32" Alias "rs_close"  (ByVal lPort As Long) As Long
Declare Function rs_putc   Lib "fca32" Alias "rs_putc"   (ByVal lChar As Long, ByVal lPort As Long) As Long
Declare Function rs_write  Lib "fca32" Alias "rs_write"  (ByRef byBuffer As Byte, ByVal lSize As Long, ByVal lPort As Long) As Long
Declare Function rs_getc   Lib "fca32" Alias "rs_getc"   (ByVal lPort As Long) As Long
Declare Function rs_read   Lib "fca32" Alias "rs_read"   (ByRef byBuffer As Byte, ByVal lSize As Long, ByVal lPort As Long) As Long
Declare Function rs_buffer Lib "fca32" Alias "rs_buffer" (ByVal lPort As Long, ByVal lCmnd As Long) As Long
Declare Function rs_status Lib "fca32" Alias "rs_status" (ByVal lPort As Long) As Long

Declare Function fca_setparam Lib "fca32" Alias "fca_setparam" (ByVal lPort As Long, ByRef Param As SER_T) As Long
Declare Function fca_bye      Lib "fca32" Alias "fca_bye"      (ByVal lPort As Long) As Long
Declare Function fca_open     Lib "fca32" Alias "fca_open"     (ByVal sName As String, ByVal lMode As Long) As Long
Declare Function fca_close    Lib "fca32" Alias "fca_close"    () As Long
Declare Function fca_read     Lib "fca32" Alias "fca_read"     (ByRef byBuffer As Byte, ByVal lBytes As Long) As Long
Declare Function fca_write    Lib "fca32" Alias "fca_write"    (ByRef byBuffer As Byte, ByVal lBytes As Long) As Long
Declare Function fca_fopen    Lib "fca32" Alias "fca_fopen"    (ByVal sName As String, ByVal sMode As String) As Long
Declare Function fca_fclose   Lib "fca32" Alias "fca_fclose"   () As Long
Declare Function fca_getc     Lib "fca32" Alias "fca_getc"     () As Long
Declare Function fca_putc     Lib "fca32" Alias "fca_putc"     (ByVal lChar As Long) As Long
Declare Function fca_readdir  Lib "fca32" Alias "fca_readdir"  (ByRef Buffer As FCA_DIR, ByVal lNdir As Long, ByVal lNfile As Long) As Long
Declare Function fca_rename   Lib "fca32" Alias "fca_rename"   (ByVal sOldname As String, ByVal sNewname As String) As Long
Declare Function fca_delete   Lib "fca32" Alias "fca_delete"   (ByVal sName As String) As Long
Declare Function fca_remains  Lib "fca32" Alias "fca_remains"  (ByRef lRemains As Long) As Long
Declare Function fca_status   Lib "fca32" Alias "fca_status"   (ByRef byBuffer As Byte) As Long

