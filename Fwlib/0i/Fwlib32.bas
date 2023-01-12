Attribute VB_Name = "Fwlib32"
'------------------------------------------------------------------
'
' CNC/PMC Data Window Library for FOCAS1 and FOCAS2
'
' Copyright (C) 2003-2011 by FANUC CORPORATION All rights reserved.
'
'------------------------------------------------------------------

'----------------------
'  Axis Define
'----------------------
' machine information for FS0i-A
Public Const FS0ID = 1
Public Const MAX_AXIS = 4
Public Const MAX_SPINDLE = 4
Public Const MAX_CNCPATH = 10
Public Const MAX_IFSB_LINE = 3

#Const HSSB_LIB = 1
#Const FS0ID = 1
#Const AXIS_DEF = 1
#Const SPINDLE_DEF = 1
#Const CNCPATH_DEF = 1
#Const IFSB_LINE_DEF = 1

#If AXIS_DEF = 0 Then
Public Const MAX_AXIS = 32
#End If
#If SPINDLE_DEF = 0 Then
Public Const MAX_SPINDLE = 8
#End If
#If CNCPATH_DEF = 0 Then
Public Const MAX_CNCPATH = 10
#End If
#If IFSB_LINE_DEF = 0 Then
Public Const MAX_IFSB_LINE = 3
#End If

Public Const MAX_AXISNAME = 4

Public Const ALL_AXES = -1
Public Const ALL_SPINDLES = -1

'----------------------
'  Error Codes
'----------------------
Public Const EW_PROTOCOL = -17          ' protocol error
Public Const EW_SOCKET = -16            ' Windows socket error
Public Const EW_NODLL = -15             ' DLL not exist error
Public Const EW_INIERR = -14            ' error in APi library inital valiefile
Public Const EW_ITLOW = -13             ' low temperature alarm of intelligent terminal
Public Const EW_ITHIGHT = -12           ' hight temperature alarm of intelligent terminal
Public Const EW_BUS = -11               ' bus error
Public Const EW_SYSTEM2 = -10           ' system error
Public Const EW_HSSB = -9               ' hssb communication error
Public Const EW_HANDLE = -8             ' Windows library handle error
Public Const EW_VERSION = -7            ' CNC/PMC version missmatch
Public Const EW_UNEXP = -6              ' abnormal error
Public Const EW_SYSTEM = -5             ' system error
Public Const EW_PARITY = -4             ' shared RAM parity error
Public Const EW_MMCSYS = -3             ' emm386 or mmcsys install error
Public Const EW_RESET = -2              ' reset or stop occured error
Public Const EW_BUSY = -1               ' busy error
Public Const EW_OK = 0                  ' no problem
Public Const EW_FUNC = 1                ' command prepare error
Public Const EW_NOPMC = 1               ' pmc not exist
Public Const EW_LENGTH = 2              ' data block length error
Public Const EW_NUMBER = 3              ' data number error
Public Const EW_RANGE = 3               ' address range error
Public Const EW_ATTRIB = 4              ' data attribute error
Public Const EW_TYPE = 4                ' data type error
Public Const EW_DATA = 5                ' data error
Public Const EW_NOOPT = 6               ' no option error
Public Const EW_PROT = 7                ' write protect error
Public Const EW_OVRFLOW = 8             ' memory overflow error
Public Const EW_PARAM = 9               ' cnc parameter not correct error
Public Const EW_BUFFER = 10             ' buffer error
Public Const EW_PATH = 11               ' path error
Public Const EW_MODE = 12               ' cnc mode error
Public Const EW_REJECT = 13             ' execution rejected error
Public Const EW_DTSRVR = 14             ' data server error
Public Const EW_ALARM = 15              ' alarm has been occurred
Public Const EW_STOP = 16               ' CNC is not running
Public Const EW_PASSWD = 17             ' protection data error

'---------------------------------
'  Result codes of DNC operation
'---------------------------------
Public Const DNC_NORMAL = -1            ' normal completed
Public Const DNC_CANCEL = -32768        ' DNC operation was canceled by CNC
Public Const DNC_OPENERR = -514         ' file open error
Public Const DNC_NOFILE = -516          ' file not found
Public Const DNC_READERR = -517         ' read error


'------------------------------------------------
'  Option name for cnc_getlibopt / cnc_setlibopt
'------------------------------------------------
Public Const LIB_MODE = 0
Public Const MOVE_RDPRGPTR = 1
Public Const PRM_ALLPATH = 2
Public Const MSG_NOCTRL = 4
Public Const UP_DNLOAD_EDT = 8
Public Const TLIFE_OPTION = 16

'----------------------
'
'  Structure Template
'
'----------------------
'-------------------------------------
' CNC: Control axis / spindle related
'-------------------------------------

' cnc_actf:read actual axis feedrate(F)
' cnc_acts:read actual spindle speed(S)
Type ODBACT
  nDummy(0 To 1) As Integer     ' dummy
  lData As Long                 ' actual feed / actual spindle
End Type

' cnc_acts2:read actual spindle speed(S)
' (All or specified)
Type ODBACT2
  nDatano As Integer            ' spindle number
  nType As Integer              ' dummy
  lData(0 To MAX_SPINDLE - 1) As Long ' spindle data
End Type

' cnc_absolute:read absolute axis position
' cnc_machine:read machine axis position
' cnc_machine2:read machine axis position(2)
' cnc_relative:read relative axis position
' cnc_distance:read distance to go
' cnc_skip:read skip position
' cnc_srvdelay:read servo delay value
' cnc_accdecdly:read acceleration/deceleration delay value
' cnc_absolute2:read absolute axis position 2
' cnc_relative2:read relative axis position 2
Type ODBAXIS
  nDummy As Integer             ' dummy
  nType As Integer              ' axis number
  lData(0 To MAX_AXIS - 1) As Long ' data value
End Type

' cnc_rddynamic:read all dynamic data
#If ONO8D Then

Type ODBDY1
  nDummy As Integer
  nAxis As Integer              ' axis number
  nAlarm As Integer             ' alarm status
  lPrgnum As Long               ' current program number
  lPrgmnum As Long              ' main program number
  lSeqnum As Long               ' current sequence number
  lActf As Long                 ' actual feedrate
  lActs As Long                 ' actual spindle speed
  lAbsolute As Long             ' absolute position
  lMachine As Long              ' machine position
  lRelative As Long             ' relative position
  lDistance As Long             ' distance to go
End Type                        ' In case of no axis

Type ODBDY2
  nDummy As Integer
  nAxis As Integer              ' axis number
  nAlarm As Integer             ' alarm status
  lPrgnum As Long               ' current program number
  lPrgmnum As Long              ' main program number
  lSeqnum As Long               ' current sequence number
  lActf As Long                 ' actual feedrate
  lActs As Long                 ' actual spindle speed
  lAbsolute(0 To MAX_AXIS - 1) As Long ' absolute position
  lMachine(0 To MAX_AXIS - 1) As Long  ' machine position
  lRelative(0 To MAX_AXIS - 1) As Long ' relative position
  lDistance(0 To MAX_AXIS - 1) As Long ' distance to go
End Type

#Else

Type ODBDY1
  nDummy As Integer
  nAxis As Integer              ' axis number
  nAlarm As Integer             ' alarm status
  nPrgnum As Integer            ' current program number
  nPrgmnum As Integer           ' main program number
  lSeqnum As Long               ' current sequence number
  lActf As Long                 ' actual feedrate
  lActs As Long                 ' actual spindle speed
  lAbsolute As Long             ' absolute position
  lMachine As Long              ' machine position
  lRelative As Long             ' relative position
  lDistance As Long             ' distance to go
End Type                        ' In case of no axis

Type ODBDY2
  nDummy As Integer
  nAxis As Integer              ' axis number
  nAlarm As Integer             ' alarm status
  nPrgnum As Integer            ' current program number
  nPrgmnum As Integer           ' main program number
  lSeqnum As Long               ' current sequence number
  lActf As Long                 ' actual feedrate
  lActs As Long                 ' actual spindle speed
  lAbsolute(0 To MAX_AXIS - 1) As Long ' absolute position
  lMachine(0 To MAX_AXIS - 1) As Long  ' machine position
  lRelative(0 To MAX_AXIS - 1) As Long ' relative position
  lDistance(0 To MAX_AXIS - 1) As Long ' distance to go
End Type

#End If

' cnc_rddynamic2:read all dynamic data
Type ODBDY10
  nDummy As Integer
  nAxis As Integer              ' axis number
  lAlarm As Long                ' alarm status
  lPrgnum As Long               ' current program number
  lPrgmnum As Long              ' dummy
  lSeqnum As Long               ' current sequence number
  lActf As Long                 ' actual feedrate
  lActs As Long                 ' actual spindle speed
  lAbsolute As Long             ' absolute position
  lMachine As Long              ' machine position
  lRelative As Long             ' relative position
  lDistance As Long             ' distance to go
End Type                        ' In case of no axis

Type ODBDY20
  nDummy As Integer
  nAxis As Integer              ' axis number
  lAlarm As Long                ' alarm status
  lPrgnum As Long               ' current program number
  lPrgmnum As Long              ' dummy
  lSeqnum As Long               ' current sequence number
  lActf As Long                 ' actual feedrate
  lActs As Long                 ' actual spindle speed
  lAbsolute(0 To MAX_AXIS - 1) As Long ' absolute position
  lMachine(0 To MAX_AXIS - 1) As Long  ' machine position
  lRelative(0 To MAX_AXIS - 1) As Long ' relative position
  lDistance(0 To MAX_AXIS - 1) As Long ' distance to go
End Type

' cnc_wrrelpos:set origin / preset relative axis position
Type IDBWRR
  nDatano As Integer            ' dummy
  nType As Integer              ' axis number
  lData(0 To MAX_AXIS - 1) As Long ' preset data
End Type

' cnc_prstwkcd:preset work coordinate
Type IDBWRA
  nDatano As Integer            ' dummy
  nType As Integer              ' axis number
  lData(0 To MAX_AXIS - 1) As Long ' preset data
End Type

' cnc_rdmovrlap:read manual overlapped motion value
Type IODBOVL
  nDatano As Integer            ' dummy
  nType As Integer              ' axis number
  lData(0 To 1, 0 To MAX_AXIS - 1) As Long ' data value
End Type

' cnc_rdspload:read load information of serial spindle
' cnc_rdspmaxrpm:read maximum r.p.m. ratio of serial spindle
' cnc_rdspgear:read gear ratio of serial spindle
Type ODBSPN
  nDatano As Integer            ' spindle number
  nType As Integer              ' dummy
  nData(0 To MAX_SPINDLE - 1) As Integer ' spindle data
End Type

' cnc_rdposition:read tool position
Type POSELM
  nData As Long                 ' position data
  nDec As Integer               ' decimal position
  nUnit As Integer              ' data unit
  nDisp As Integer              ' display flag
  sName As String * 1           ' axis name
  sSuff As String * 1           ' suffix
End Type

#If PMD = 0 Then                ' except Power Mate D/H
Type ODBPOS
  abs As POSELM                 ' absolute position
  mach As POSELM                ' machine position
  rel As POSELM                 ' relative position
  dist As POSELM                ' distance position
End Type
#End If

' cnc_rdhndintrpt:read handle interruption
Type ODBHND
  input As POSELM               ' input unit
  output As POSELM              ' output unit
End Type

' cnc_rdspeed:read current speed
Type SPEEDELM
  nData As Long                 ' speed data
  nDec As Integer               ' decimal position
  nUnit As Integer              ' data unit
  nDisp As Integer              ' display flag
  sName As String * 1           ' name
  sSuff As String * 1           ' suffix
End Type

Type ODBSPEED
  actf As SPEEDELM            ' actual feed rate
  acts As SPEEDELM            ' actual spindle speed
End Type

' cnc_rdsvmeter:read servo load meter
' cnc_rdspmeter:read spindle load meter
Type LOADELM
  nData As Long                 ' speed data
  nDec As Integer               ' decimal position
  nUnit As Integer              ' data unit
  sName As String * 1           ' axis name
  sSuff1 As String * 1          ' suffix
  sSuff2 As String * 1          ' suffix
  sReserve As String * 1        ' reserve
End Type

Type ODBSVLOAD
  svload As LOADELM             ' servo load meter
End Type

Type ODBSPLOAD
  spload As LOADELM             ' spindle load meter
  spspeed As LOADELM            ' spindle speed
End Type

' cnc_rdaxisdata:read various axis data
Type ODBAXDT
  sName As String * 4   ' axis name
  lData As Long         ' position data
  nDec  As Integer      ' decimal position
  nUnit As Integer      ' data unit
  nFlag As Integer      ' flags
  nReserve As Integer   ' reserve
End Type

' cnc_rdspcss:constantly spindle speed
Type ODBCSS
  lSrpm As Long                 ' order spindle speed
  lSspm As Long                 ' order constant spindle speed
  lSmax As Long                 ' order maximum spindle speed
End Type

' cnc_rdexecpt:read execution program pointer
Type PRGPNT
  lProg_no As Long              ' program number
  lBlk_no As Long               ' block number
End Type

' cnc_rdhsprminfo:read the information for function cnc_rdhsparam()
Type HSPINFO
  sPrminfo(0 To 127) As Byte
End Type

' cnc_rdhsparam:read parameters at the high speed
Type HSPDATA1
  sCdata(0 To MAX_AXIS - 1) As Byte
End Type

Type HSPDATA2
  nIdata(0 To MAX_AXIS - 1) As Integer
End Type

Type HSPDATA3
  lLdata(0 To MAX_AXIS - 1) As Long
End Type

' cnc_rd5axmandt:read manual feed for 5-axis machining
Type ODB5AXMAN
  type1 As Integer
  type2 As Integer
  type3 As Integer
  data1 As Long
  data2 As Long
  data3 As Long
  c1 As Long
  c2 As Long
  dummy As Long
  td As Long
  r1 As Long
  r2 As Long
  vr As Long
  h1 As Long
  h2 As Long
End Type

'----------------------
' CNC: Program related
'----------------------

' cnc_rddncdgndt:read the diagnosis data of DNC operation
Type ODBDNCDGN
  nCtrl_word As Integer
  nCan_word As Integer
  sNc_file As String * 16
  nRead_ptr As Integer
  nWrite_ptr As Integer
  nEmpty_cnt As Integer
  nTotal_size As Long
End Type

' cnc_upload:upload NC program
' cnc_cupload:upload NC program(conditional)
Type ODBUP
  nDummy(0 To 1) As Integer     ' dummy
  sData As String * 256         ' data
End Type                        ' In case that the number of data is 256

' cnc_buff:read buffer status for downloading/verification NC program
Type ODBBUF
  nDummy(0 To 1) As Integer     ' dummy
  nData As Integer              ' buffer status
End Type

' cnc_rdprogdir:read program directory
Type PRGDIR
  sPrg_data As String * 256     ' directory data
End Type                        ' In case that the number of data is 256

' cnc_rdproginfo:read program information
Type ODBNC1
  nReg_prg As Integer           ' registered program number
  nUnreg_prg As Integer         ' unregistered program number
  lUsed_mem As Long             ' used memory area
  lUnused_mem As Long           ' unused memory area
End Type

Type ODBNC2
  sAsc As String * 31           ' ASCII string type
End Type

' cnc_rdprgnum:read program number under execution
#If ONO8D Then

Type ODBPRO
  nDummy(0 To 1) As Integer     ' dummy
  lData As Long                 ' running program number
  lMdata As Long                ' main program number
End Type

#Else

Type ODBPRO
  nDummy(0 To 1) As Integer     ' dummy
  nData As Integer              ' running program number
  nMdata As Integer             ' main program number
End Type

#End If

' cnc_exeprgname:read program name under execution
Type ODBEXEPRG
  sName As String * 36       ' running program name
  lOnum As Long              ' running program number
End Type

' cnc_rdseqnum:read sequence number under execution
Type ODBSEQ
  nDummy(0 To 1) As Integer     ' dummy
  lData As Long                 ' sequence number
End Type

' cnc_rdmdipntr:read execution pointer for MDI operation
#If ONO8D Then

Type ODBMDIP
  lMdiprog As Long              ' exec. program number
  lMdipntr As Long              ' exec. pointer
  lCrntprog As Long             ' prepare program number
  lCrntpntr As Long             ' prepare pointer
End Type

#Else

Type ODBMDIP
  nMdiprog As Integer           ' exec. program number
  lMdipntr As Long              ' exec. pointer
  nCrntprog As Integer          ' prepare program number
  lCrntpntr As Long             ' prepare pointer
End Type

#End If

' cnc_rdpdf_drive:read program drive directory
Type ODBPDFDRV
  nMax_num As Integer                   ' maximum drive number
  dummy As Integer
  sDrive(0 To 15) As String * 12        ' drive name
End Type

' cnc_rdpdf_inf:read program drive information
Type ODBPDFINF
  lUsed_page As Long            ' used capacity
  lAll_page As Long             ' all capacity
  lUsed_dir As Long             ' used directory number
  lAll_dir As Long              ' all directory number
End Type

' cnc_rdpdf_subdir:read directory (sub directories)
Type IDBPDFSDIR
  sPath As String * 212           ' path name
  nReq_num As Integer             ' entry number
  dummy As Integer
End Type

' cnc_rdpdf_subdir:read directory (sub directories)
Type ODBPDFSDIR
  nSub_exist As Integer           ' existence of sub directory
  dummy As Integer
  sD_f As String * 36             ' directory name
End Type

' cnc_rdpdf_alldir:read directory (all files)
Type IDBPDFADIR
  sPath As String * 212           ' path name
  nReq_num As Integer             ' entry number
  nSize_kind As Integer           ' kind of size
  nType As Integer                ' kind of format
  dummy As Integer
End Type

' cnc_rdpdf_alldir:read directory (all files)
Type ODBPDFADIR
  nData_kind As Integer           ' kinf of data
  nYear As Integer                ' last date and time
  nMon As Integer                 ' last date and time
  nDay As Integer                 ' last date and time
  nHour As Integer                ' last date and time
  nMin As Integer                 ' last date and time
  nSec As Integer                 ' last date and time
  dummy As Integer
  dummy2 As Long
  lSize As Long                   ' size
  lAttr As Long                   ' attribute
  sD_f As String * 36             ' path name
  sComment As String * 52         ' comment
  sO_time As String * 12          ' machining time stamp
End Type

' cnc_rdpdf_subdirn:read file count the directory has
Type ODBPDFNFIL
  nDir_num As Integer           ' directory
  nFile_num As Integer          ' file
End Type

' cnc_wrpdf_attr:change attribute of program file and directory
Type IDBPDFTDIR
  lSlct As Long ' selection
  lAttr As Long ' data
End Type


'---------------------------
' CNC: NC file data related
'---------------------------

' cnc_rdtofs:read tool offset value
Type ODBTOFS
  nDatano As Integer            ' data number
  nType As Integer              ' data type
  lData As Long                 ' data
End Type

' cnc_rdtofsr:read tool offset value(area specified)
' cnc_wrtofsr:write tool offset value(area specified)
Type IODBTO1
  nDatano_s As Integer          ' start offset number
  nType As Integer              ' offset type
  nDatano_e As Integer          ' end offset number
  dummy As Integer
  lM_ofs(0 To 4) As Long        ' M Each
End Type                        ' In case that the number of data is 5

Type IODBTO2
  nDatano_s As Integer          ' start offset number
  nType As Integer              ' offset type
  nDatano_e As Integer          ' end offset number
  dummy As Integer
  lM_ofs_a(0 To 4) As Long      ' M-A All
End Type                        ' In case that the number of data is 5

Type IODBTO3
  nDatano_s As Integer          ' start offset number
  nType As Integer              ' offset type
  nDatano_e As Integer          ' end offset number
  dummy As Integer
  lM_ofs_b(0 To 9) As Long      ' M-B All
End Type                        ' In case that the number of data is 5

Type IODBTO4
  nDatano_s As Integer          ' start offset number
  nType As Integer              ' offset type
  nDatano_e As Integer          ' end offset number
  dummy As Integer
  lM_ofs_c(0 To 19) As Long     ' M-C All
End Type                        ' In case that the number of data is 5

Type IODBTO5
  nDatano_s As Integer          ' start offset number
  nType As Integer              ' offset type
  nDatano_e As Integer          ' end offset number
  dummy As Integer
  nT_tip(0 To 4) As Integer     ' T Each, 2-byte
End Type                        ' In case that the number of data is 5

Type IODBTO6
  nDatano_s As Integer          ' start offset number
  nType As Integer              ' offset type
  nDatano_e As Integer          ' end offset number
  dummy As Integer
  lT_ofs(0 To 4) As Long        ' T Each, 4-byte
End Type                        ' In case that the number of data is 5

Type IODBTOdata1
  nTip As Integer
  lData(0 To 3) As Long
End Type

Type IODBTO7
  nDatano_s As Integer                  ' start offset number
  nType As Integer                      ' offset type
  nDatano_e As Integer                  ' end offset number
  dummy As Integer
  nT_ofs_a(0 To 4) As IODBTOdata1       ' T-A All
End Type                                ' In case that the number of data is 5

Type IODBTOdata2
  nTip As Integer
  lData(0 To 7) As Long
End Type

Type IODBTO8
  nDatano_s As Integer                  ' start offset number
  nType As Integer                      ' offset type
  nDatano_e As Integer                  ' end offset number
  dummy As Integer
  nT_ofs_b(0 To 4) As IODBTOdata2       ' T-B All
End Type                                ' In case that the number of data is 5

' cnc_rdzofs:read work zero offset value
' cnc_wrzofs:write work zero offset value
Type IODBZOFS
  nDatano As Integer            ' offset NO.
  nType As Integer              ' axis number
  lData(0 To MAX_AXIS - 1) As Long ' data value
End Type

' cnc_rdzofsr:read work zero offset value(area specified)
' cnc_wrzofsr:write work zero offset value(area specified)
Type IODBZOR
  nDatano_s As Integer          ' start offset number
  nType As Integer              ' axis number
  nDatano_e As Integer          ' end offset number
  lData(0 To 7 * MAX_AXIS - 1) As Long ' offset value
End Type                        ' The number of data is 7

' cnc_rdparam:read parameter
' cnc_wrparam:write parameter
' cnc_rdset:read setting data
' cnc_wrset:write setting data
' cnc_rdparar:read parameter(area specified)
' cnc_wrparas:write parameter(area specified)
' cnc_rdsetr:read setting data(area specified)
' cnc_wrsets:write setting data(area specified)
Type IODBPSD1
  nDatano As Integer            ' data number
  nType As Integer              ' axis number
  sCdata As Byte
End Type

Type IODBPSD2
  nDatano As Integer            ' data number
  nType As Integer              ' axis number
  nIdata As Integer
End Type

Type IODBPSD3
  nDatano As Integer            ' data number
  nType As Integer              ' axis number
  lLdata As Long
End Type

Type IODBPSD4
  nDatano As Integer            ' data number
  nType As Integer              ' axis number
  sCdatas(0 To MAX_AXIS - 1) As Byte
End Type

Type IODBPSD5
  nDatano As Integer            ' data number
  nType As Integer              ' axis number
  nIdatas(0 To MAX_AXIS - 1) As Integer
End Type

Type IODBPSD6
  nDatano As Integer            ' data number
  nType As Integer              ' axis number
  lLdatas(0 To MAX_AXIS - 1) As Long
End Type

Type REALPRM
  lPrm_val As Long
  lDec_val As Long
End Type

Type IODBPSD7
  nDatano As Integer            ' data number
  nType As Integer              ' axis number
  nRdata As REALPRM
End Type

Type IODBPSD8
  nDatano As Integer            ' data number
  nType As Integer              ' axis number
  nRdatas(0 To MAX_AXIS - 1) As REALPRM
End Type

' cnc_rdmsptype:read mesured point value
' cnc_wrmsptype:write mesured point value
Type IODBMSTP
  nDatano_s As Integer    ' start offset number
  dummy As Integer        ' dummy
  nDatano_e As Integer    ' end offset number
  cData(0 To 6) As Byte   ' mesured point value
End Type

' cnc_rdparam_ext:read parameters
' cnc_rddiag_ext:read diagnosis data
Type IODBPRMdata
  lPrm_val As Long              ' parameter / setting data
  lDec_val As Long
End Type

Type IODBPRM
  lDatano As Long               ' data number
  nType As Integer              ' data type
  nAxis As Integer              ' axis information
  nInfo As Integer              ' misc information
  nUnit As Integer              ' unit information
  pData(0 To 31) As IODBPRMdata
End Type

' cnc_rdpitchr:read pitch error compensation data(area specified)
' cnc_wrpitchr:write pitch error compensation data(area specified)
Type IODBPI
  nDatano_s As Integer          ' start pitch number
  nDummy As Integer             ' dummy
  nDatano_e As Integer          ' end pitch number
  sData(0 To 4) As Byte         ' offset value
End Type                        ' In case that the number of data is 5


' cnc_rdvolc : read volumetric compensation data
' cnc_wrvolc : write volumetric compensation data
Type ODBVOLC
  lStart_no As Long
  lStart_ax As Long
  lEnd_no As Long
  lEnd_ax As Long
  lNum As Long
  sType As Byte
  lData(0 To 71) As Long
End Type

' cnc_rdvolccomp : get volumetric compensation amount of axes
Type ODBVOLCOMP
 lComp(0 To 2) As Long
End Type

' cnc_rdmacro:read custom macro variable
Type ODBM
  nDatano As Integer            ' variable number
  nDummy As Integer             ' dummy
  lMcr_val As Long              ' macro variable
  nDec_val As Integer           ' decimal point
End Type

' cnc_rdmacro3:read custom macro variable (3)
Type ODBM3
  lDatano As Long               ' variable number
  lMcr_val As Long              ' macro variable
  nDec_val As Integer           ' decimal point
End Type

' cnc_rdmacror:read custom macro variables(area specified)
' cnc_wrmacror:write custom macro variables(area specified)
Type IODBMRdata
  lMcr_val As Long              ' macro variable
  nDec_val As Integer           ' decimal point
End Type

Type IODBMR
  nDatano_s As Integer          ' start macro number
  nDummy As Integer             ' dummy
  nDatano_e As Integer          ' end macro number
  nData(0 To 4) As IODBMRdata
End Type                        ' In case that the number of data is 5

' cnc_rdmacror3:read custom macro names(area specified)
Type IODBMRN3
   mcr_val As Double    ' macro variable
   sName As String * 32 ' macro name
End Type

' cnc_rdpmacro:read P code macro variable
Type ODBPM
  lDatano As Long               ' variable number
  nDummy As Integer             ' dummy
  lMcr_val As Long              ' macro variable
  nDec_val As Integer           ' decimal point
End Type

' cnc_rdpmacror:read P code macro variables(area specified)
' cnc_wrpmacror:write P code macro variables(area specified)
Type IODBPRdata
  lMcr_val As Long              ' macro variable
  nDec_val As Integer           ' decimal point
End Type

Type IODBPR
  lDatano_s As Long             ' start macro number
  nDummy As Integer             ' dummy
  lDatano_e As Long             ' end macro number
  nData(0 To 4) As IODBPRdata
End Type                        ' In case that the number of data is 5

' cnc_rdtofsinfo:read tool offset information
Type ODBTLINF
  nOfs_type As Integer
  nUse_no As Integer
End Type

' cnc_rdtofsinfo2:read tool offset information(2)
Type ODBTLINF2
  nOfs_type As Integer
  nUse_no As Integer
  nOfs_enable As Integer
End Type

' cnc_rdmacroinfo:read custom macro variable information
Type ODBMVINF
  nUse_no1 As Integer
  nUse_no2 As Integer
End Type

' cnc_rdpmacroinfo:read P code macro variable information
#If FS15BD Then

Type ODBPMINF
  nUse_no1 As Integer
  nUse_no2 As Integer
  nV2_type1 As Integer
  nV2_type2 As Integer
End Type

#Else

Type ODBPMINF
  nUse_no1 As Integer
  nUse_no2 As Integer
  nV2_type As Integer
End Type

#End If

' cnc_rdpmacroinfo2:read P code macro variable information (2)
Type ODBPMINF2
  lUse_no1 As Long
  lUse_no2 As Long
  lUse_no20 As Long
  nV1_type As Integer
  nV2_type As Integer
  nV20_type As Integer
End Type

' cnc_tofs_rnge:read validity of tool offset
' cnc_zofs_rnge:read validity of work zero offset
' cnc_wksft_rnge:read validity of work shift value
Type ODBDATRNG
  lData_min As Long     ' lower limit
  lData_max As Long     ' upper limit
  lStatus As Long       ' status of setting
End Type

' cnc_rdfixoffs:read rotary table dynamic fixture offset
Type ODBFOFS
  lMcrval As Long
  nDecval As Integer
End Type


'----------------------------------------
' CNC: Tool life management data related
'----------------------------------------

' cnc_rdgrpid:read tool life management data(tool group number)
Type ODBTLIFE1
  nDummy As Integer             ' dummy
  nType As Integer              ' data type
  lData As Long                 ' data
End Type

' cnc_rdngrp:read tool life management data(number of tool groups)
Type ODBTLIFE2
  nDummy(0 To 1) As Integer     ' dummy
  lData As Long                 ' data
End Type

' cnc_rdntool:read tool life management data(number of tools)
' cnc_rdlife:read tool life management data(tool life)
' cnc_rdcount:read tool life management data(tool lift counter)
Type ODBTLIFE3
  nDatano As Integer            ' data number
  nDummy As Integer             ' dummy
  lData As Long                 ' data
End Type

' cnc_rd1length:read tool life management data(tool length number-1)
' cnc_rd2length:read tool life management data(tool length number-2)
' cnc_rd1radius:read tool life management data(cutter compensation no.-1)
' cnc_rd2radius:read tool life management data(cutter compensation no.-2)
' cnc_t1info:read tool life management data(tool information-1)
' cnc_t2info:read tool life management data(tool information-2)
' cnc_toolnum:read tool life management data(tool number)
Type ODBTLIFE4
  nDatano As Integer            ' data number
  nType As Integer              ' data type
  lData As Long                 ' data
End Type

' cnc_rdgrpid2:read tool life management data(tool group number) 2
Type ODBTLIFE5
  lDummy As Long                ' dummy
  lType As Long                 ' data type
  lData As Long                 ' data
End Type

' cnc_rdtoolrng:read tool life management data(tool number, tool life,
'                                      tool life counter)(area specified)
Type IODBTRdata
  lNtool As Long                ' tool number
  lLife As Long                 ' tool life
  lCount As Long                ' tool life counter
End Type

Type IODBTR
  nDatano_s As Integer          ' start group number
  nDummy As Integer             ' dummy
  nDatano_e As Integer          ' end group number
  nData(0 To 4) As IODBTRdata
End Type                        ' In case that the number of data is 5

' cnc_rdtoolgrp:read tool life management data(all data within group)
Type ODBTGdata
  lTuse_num As Long             ' tool number
  lTool_num As Long             ' tool life
  lLength_num As Long           ' tool life counter
  lRadius_num As Long           ' tool life counter
  lTinfo As Long                ' tool life counter
End Type

Type ODBTG
  nGrp_num As Integer           ' start group number
  nDummy(0 To 1) As Integer     ' dummy
  lNtool As Long                ' tool number
  lLife As Long                 ' tool life
  lCount As Long                ' tool life counter
  nData(0 To 4) As ODBTGdata
End Type                        ' In case that the number of data is 5

' cnc_wrcountr:write tool life management data(tool life counter)
'                                                         (area specified)
Type IDBWRCdata
  lDummy(0 To 1) As Long        ' dummy
  lCount As Long                ' tool life counter
End Type

Type IDBWRC
  nDatano_s As Integer          ' start group number
  nDummy As Integer             ' dummy
  nDatano_e As Integer          ' end group number
  nData(0 To 4) As IDBWRCdata
End Type                        ' In case that the number of data is 5

' cnc_rdusegrpid:read tool life management data(used tool group number)
Type ODBUSEGR
  nDatano As Integer            ' dummy
  nType As Integer              ' dummy
  lNext As Long                 ' next use group number
  lUse As Long                  ' using group number
  lSlct As Long                 ' selecting group number
End Type

' cnc_rdmaxgrp:read tool life management data(max. number of tool groups)
' cnc_rdmaxtool:read tool life management data
'                                       (maximum number of tool within group)
Type ODBLFNO
  nDatano As Integer            ' dummy
  nType As Integer              ' dummy
  nData As Integer              ' number of data
End Type

' cnc_rdusetlno:read tool life management data(used tool no within group)
Type ODBTLUSE
  nS_grp As Integer             ' start group number
  nDummy As Integer             ' dummy
  nE_grp As Integer             ' end group number
  lData(0 To 4) As Long         ' tool using number
End Type                        ' In case that the number of group is 5

' cnc_rd1tlifedata:read tool life management data(tool data1)
' cnc_rd2tlifedata:read tool life management data(tool data2)
' cnc_wr1tlifedata:write tool life management data(tool data1)
' cnc_wr2tlifedata:write tool life management data(tool data2)
Type IODBTD
  nDatano As Integer            ' tool group number
  nType As Integer              ' tool using number
  lTool_num As Long             ' tool number
  lH_code As Long               ' H code
  lD_code As Long               ' D code
  lTool_inf As Long             ' tool information
End Type

' cnc_rd1tlifedat2:read tool life management data(tool data1) 2
' cnc_wr1tlifedat2:write tool life management data(tool data1) 2
Type IODBTD2
  nDatano As Integer            ' tool group number
  nDummy As Integer             ' dummy
  lType As Long                 ' tool using number
  lTool_num As Long             ' tool number
  lH_code As Long               ' H code
  lD_code As Long               ' D code
  lTool_inf As Long             ' tool information
End Type

' cnc_rdgrpinfo:read tool life management data(tool group information)
' cnc_wrgrpinfo:write tool life management data(tool group information)
Type IODBTGIdata
  lN_tool As Long               ' number of tool
  lCount_value As Long          ' tool life
  lCounter As Long              ' tool life counter
  lCount_type As Long           ' tool life counter type
End Type

Type IODBTGI
  nS_grp As Integer             ' start group number
  nDummy As Integer             ' dummy
  nE_grp As Integer             ' end group number
  nData(0 To 4) As IODBTGIdata
End Type                        ' In case that the number of group is 5

Type IODBTGI2
  nS_grp As Integer             ' start group number
  nDummy As Integer             ' dummy
  nE_grp As Integer             ' end group number
  nOpt_grpno(0 To 4) As Long    ' optional group number of tool
End Type                        ' In case that the number of group is 5

Type IODBTGI3
  nS_grp As Integer             ' start group number
  nDummy As Integer             ' dummy
  nE_grp As Integer             ' end group number
  nLife_rest(0 To 4) As Long    ' tool life rest count
End Type                        ' In case that the number of group is 5

' cnc_instlifedt:insert tool life management data(tool data)
Type IDBITD
  nDatano As Integer            ' tool group number
  nType As Integer              ' tool using number
  lData As Long                 ' tool number
End Type

' cnc_rdtlinfo:read tool life management data
Type ODBTLINFO
  lMax_group As Long            ' maximum number of tool groups
  lMax_tool As Long             ' maximum number of tool within group
  lMax_minute As Long           ' maximum number of life count (minutes)
  lMax_cycle As Long            ' maximum number of life count (cycles)
End Type

' cnc_rdgrpinfo4:read tool life management data
Type IODBTGI4
  nGrp_no As Integer            ' tool group number
  lN_tool As Long               ' number of tool
  lCount_value As Long          ' tool life
  lCounter As Long              ' tool life counter
  lCount_type As Long           ' tool life counter type
  lOpt_grpno As Long            ' optional group number of tool
  lLife_rest As Long            ' tool life rest count
End Type

' cnc_rdtlusegrp:read tool life management data(used tool group number)
Type ODBUSEGRP
  lNext As Long                 ' next use group number
  lUse As Long                  ' using group number
  lSlct As Long                 ' selecting group number
  lOpt_next As Long             ' next use optional group number
  lOpt_use As Long              ' using optional group number
  lOpt_slct As Long             ' selecting optional group number
End Type

' cnc_rdtlgrp:read tool life management data(tool group information 2)
Type IODBTLGRP
  lNtool As Long                ' number of all tool
  lNfree As Long                ' number of free tool
  lLife As Long                 ' tool life
  lCount As Long                ' tool life counter
  lUse_tool As Long             ' using tool number
  lOpt_grpno As Long            ' optional group number
  lLife_rest As Long            ' tool life rest count
  nRest_sig As Integer          ' tool life rest signal
  nCount_type As Integer        ' tool life counter type
End Type

' cnc_rdexchgtgrp:read tool life management data(exchange tool group number)
Type ODBEXGP
  lGrp_no  As Long              ' group number
  lOpt_grpno As Long            ' optional group number
End Type

' cnc_rdtltool:read tool life management data (tool data1)
Type IODBTLTOOL
  lTool_num As Long             ' tool number
  lH_code As Long               ' H code
  lD_code As Long               ' D code
  lTool_inf As Long             ' tool information
End Type


'-----------------------------------
' CNC: Tool management data related
'-----------------------------------
' cnc_regtool:new registration of tool management data
' cnc_rdtool:lead of tool management data
' cnc_wrtool:write of tool management data
Type IODBTLMNG
  lT_code As Long
  lLife_count As Long
  lMax_life As Long
  lRest_life As Long
  sLife_stat As Byte
  sCust_bits As Byte
  nTool_info As Integer
  nH_code As Integer
  nD_code As Integer
  lSpindle_speed As Long
  lFeedrate As Long
  nMagazine As Integer
  nPot As Integer
  nG_code As Integer
  nW_code As Integer
  nGno As Integer
  nGrp As Integer
  nEdge As Integer
  nOrg_magazine As Integer
  nOrg_pot As Integer
  nReserve_s As Integer
  lReserved(0 To 1) As Long
  lCustom1 As Long
  lCustom2 As Long
  lCustom3 As Long
  lCustom4 As Long
  lCustom5 As Long
  lCustom6 As Long
  lCustom7 As Long
  lCustom8 As Long
  lCustom9 As Long
  lCustom10 As Long
  lCustom11 As Long
  lCustom12 As Long
  lCustom13 As Long
  lCustom14 As Long
  lCustom15 As Long
  lCustom16 As Long
  lCustom17 As Long
  lCustom18 As Long
  lCustom19 As Long
  lCustom20 As Long
End Type

Type IODBTLMNG_F2
  lT_code As Long
  lLife_count As Long
  lMax_life As Long
  lRest_life As Long
  cLife_stat As Byte
  cCust_bits As Byte
  nTool_info As Integer
  nH_code As Integer
  nD_code As Integer
  lSpindle_speed As Long
  lFeedrate As Long
  nMagazine As Integer
  nPot As Integer
  nG_code As Integer
  nW_code As Integer
  nGno As Integer
  nGrp As Integer
  nEdge As Integer
  nOrg_magazine As Integer
  nOrg_pot As Integer
  nReserve_s As Integer
  lReserved(0 To 1) As Long
  lCustom1 As Long
  lCustom2 As Long
  lCustom3 As Long
  lCustom4 As Long
  lCustom5 As Long
  lCustom6 As Long
  lCustom7 As Long
  lCustom8 As Long
  lCustom9 As Long
  lCustom10 As Long
  lCustom11 As Long
  lCustom12 As Long
  lCustom13 As Long
  lCustom14 As Long
  lCustom15 As Long
  lCustom16 As Long
  lCustom17 As Long
  lCustom18 As Long
  lCustom19 As Long
  lCustom20 As Long
  lCustom21 As Long
  lCustom22 As Long
  lCustom23 As Long
  lCustom24 As Long
  lCustom25 As Long
  lCustom26 As Long
  lCustom27 As Long
  lCustom28 As Long
  lCustom29 As Long
  lCustom30 As Long
  lCustom31 As Long
  lCustom32 As Long
  lCustom33 As Long
  lCustom34 As Long
  lCustom35 As Long
  lCustom36 As Long
  lCustom37 As Long
  lCustom38 As Long
  lCustom39 As Long
  lCustom40 As Long
End Type

' cnc_wrtool2:write of individual data of tool management data
Type IDBTLM1
  nData_id As Long
  sData1 As Byte
End Type

Type IDBTLM2
  nData_id As Long
  nData2 As Integer
End Type

Type IDBTLM3
  nData_id As Long
  lData3 As Long
End Type

' cnc_regmagazine:new registration of magazine management data
' cnc_rdmagazine:lead of magazine management data
Type IODBTLMAG
  nMagazine As Integer
  nPot As Integer
  nTool_index As Integer
End Type

' cnc_delmagazine:deletion of magazine management data
Type IODBTLMAG2
  nMagazine As Integer
  nPot As Integer
End Type

Type IODBTLGEOM
  sL_pot_num As Byte
  sR_pot_num As Byte
  sU_pot_num As Byte
  sD_pot_num As Byte
  sTl_geom_num As Byte
End Type

' cnc_rdmag_property:read of magazineproperty data
' cnc_wrmag_property:write of magazineproperty data
Type IODBMAGPRTY
  nMmag As Integer
  nSeserve_s As Integer
  sMag_info As Byte
  sReserve(0 to 2) As Byte
  nMt_line As Integer
  nMt_row As Long
  lCstm(0 to 3) As Long
End Type

' cnc_delmag_property:delete of magazineproperty data
Type IODBMAGPRTY2
  nMag As Integer
  nReserve As Integer
End Type

' cnc_rdpot_property:read of potproperty data
' cnc_wrpot_property:write of potproperty data
Type IODBPOTPRTY
  nTool_no As Integer
  nPot_type As Integer
  sPot_info1 As Byte
  sPot_info2 As Byte
  sReserve(0 to 1) As Byte
  lCstm(0 to 9) As Long
End Type

' cnc_rdtlgeomsize_ext:read extended tool geom size data
' cnc_wrtlgeomsize_ext:write extended tool geom size data
Type IODBTLGSEXT
  lData1 As Long
  lData2 As Long
  lData3 As Long
  lData4 As Long
  sTooltype As Byte
  sInstall As Byte
  sHolder As Byte
  sToolname As String * 9
End Type

'-------------------------------------
' CNC: Operation history data related
'-------------------------------------

' cnc_rdophistry:read operation history data
Type RECALM
  nRec_type As Integer          ' record type
  nAlm_grp As Integer           ' alarm group
  nAlm_no As Integer            ' alarm number
  sAxis_no As Byte              ' axis number
  sDummy As Byte
End Type

Type ODBHIS1
  nS_no As Integer              ' start number
  nType As Integer              ' dummy
  nE_no As Integer              ' end number
  nRec_alm(0 To 9) As RECALM
End Type                        ' In case that the number of data is 10

Type RECMDI
  nRec_type As Integer          ' record type
  sKey_code As Byte             ' key code
  sPw_flag As Byte              ' power on flag
  sDummy(0 To 3) As Byte
End Type

Type ODBHIS2
  nS_no As Integer              ' start number
  nType As Integer              ' dummy
  nE_no As Integer              ' end number
  nRec_mdi(0 To 9) As RECMDI
End Type                        ' In case that the number of data is 10

Type RECSGN
  nRec_type As Integer          ' record type
  sSig_name As Byte             ' signal name
  sSig_old As Byte              ' old signal bit pattern
  sSig_new As Byte              ' new signal bit pattern
  sDummy As Byte
  nSig_no As Integer            ' signal number
End Type

Type ODBHIS3
  nS_no As Integer              ' start number
  nType As Integer              ' dummy
  nE_no As Integer              ' end number
  nRec_sgn(0 To 9) As RECSGN
End Type                        ' In case that the number of data is 10

Type RECDATE
  nRec_type As Integer          ' record type
  sYear As Byte                 ' year
  sMonth As Byte                ' month
  sDay As Byte                  ' day
  sPw_flag As Byte              ' power on flag
  sDummy(0 To 1) As Byte
End Type

Type ODBHIS4
  nS_no As Integer              ' start number
  nType As Integer              ' dummy
  nE_no As Integer              ' end number
  nRec_date(0 To 9) As RECDATE
End Type                        ' In case that the number of data is 10

Type RECTIME
  nRec_type As Integer          ' record flag
  sHour As Byte                 ' hour
  sMinute As Byte               ' minute
  sSecond As Byte               ' second
  sPw_flag As Byte              ' power on flag
  sDummy(0 To 1) As Byte
End Type

Type ODBHIS5
  nS_no As Integer              ' start number
  nType As Integer              ' dummy
  nE_no As Integer              ' end number
  nRec_time(0 To 9) As RECTIME
End Type                        ' In case that the number of data is 10

' cnc_rdophistry2:read operation history data
Type ODBOPHIS1
  nRec_len As Integer           ' length
  nRec_type As Integer          ' record type
  sKey_code As Byte             ' key code
  sPw_flag As Byte              ' power on flag
  nDummy As Integer
End Type

Type ODBOPHIS2
  nRec_len As Integer           ' length
  nRec_type As Integer          ' record type
  nSig_name As Integer          ' signal name
  nSig_no As Integer            ' signal number
  sSig_old As Byte              ' old signal bit pattern
  sSig_new As Byte              ' new signal bit pattern
  nDummy As Integer
End Type

Type ODBOPHIS3
  nRec_len As Integer           ' length
  nRec_type As Integer          ' record type
  nAlm_grp As Integer           ' alarm group
  nAlm_no As Integer            ' alarm number
  nAxis_no As Integer           ' axis number
  nYear As Integer              ' year
  nMonth As Integer             ' month
  nDay As Integer               ' day
  nHour As Integer              ' hour
  nMinute As Integer            ' minute
  nSecond As Integer            ' second
  nDummy As Integer
End Type

Type ODBOPHIS4
  nRec_len As Integer           ' length
  nRec_type As Integer          ' record type
  nEvnt_type As Integer         ' event type
  nYear As Integer              ' year
  nMonth As Integer             ' month
  nDay As Integer               ' day
  nHour As Integer              ' hour
  nMinute As Integer            ' minute
  nSecond As Integer            ' second
  nDummy As Integer
End Type

' cnc_rdophistry4:read operation history data
Type ODBOPHIS4_MDI
  nRec_len As Integer       ' length
  nRec_type As Integer      ' record type
  cKey_code As Byte         ' key code
  cPw_flag As Byte          ' power on flag
  nPth_no As Integer        ' path index
  nEx_flag As Integer       ' kxternal key flag
  nHour As Integer          ' hour
  nMinute As Integer        ' minute
  nSecond As Integer        ' second
End Type

Type ODBOPHIS4_SGN
  nRec_len As Integer       ' length
  nRec_type As Integer      ' record type
  nSig_name As Integer      ' signal name
  nSig_no As Integer        ' signal number
  cSig_old As Byte          ' old signal bit pattern
  cSig_new As Byte         ' new signal bit pattern
  nPmc_no As Integer        ' pmc index
  nHour As Integer          ' hour
  nMinute As Integer        ' minute
  nSecond As Integer        ' second
  nDummy As Integer
End Type

Type ODBOPHIS4_ALM
  nRec_len As Integer       ' length
  nRec_type As Integer      ' record type
  nAlm_grp As Integer       ' alarm group
  nAlm_no As Integer        ' alarm number
  nAxis_no As Integer       ' axis number
  nYear As Integer          ' year
  nMonth As Integer         ' month
  nDay As Integer           ' day
  nHour As Integer          ' hour
  nMinute As Integer        ' minute
  nSecond As Integer        ' second
  nPth_no As Integer        ' path index
End Type

Type ODBOPHIS4_DATE
  nRec_len As Integer       ' length
  nRec_type As Integer      ' record type
  nEvnt_type As Integer     ' event type
  nYear As Integer          ' year
  nMonth As Integer         ' month
  nDay As Integer           ' day
  nHour As Integer          ' hour
  nMinute As Integer        ' minute
  nSecond As Integer        ' second
  nDummy As Integer
End Type

Type ODBOPHIS4_IAL
  nRec_len As Integer       ' length
  nRec_type As Integer      ' record type
  nAlm_grp As Integer       ' alarm group
  nAlm_no As Integer        ' alarm number
  nAxis_no As Integer       ' axis number
  nYear As Integer          ' year
  nMonth As Integer         ' month
  nDay As Integer           ' day
  nHour As Integer          ' hour
  nMinute As Integer        ' minute
  nSecond As Integer        ' second
  nPth_no As Integer        ' path index
  nSys_alm As Integer       ' sys alarm
  nDsp_flg As Integer       ' message dsp flag
  nAxis_num As Integer      ' axis num
  lG_modal(0 To 9) As Long  ' G code Modal
  cG_dp(0 To 9) As Byte     ' #7:1 Block
                            ' #6Å`#0 dp
  lA_modal(0 To 9) As Long  ' B,D,E,F,H,M,N,O,S,T code Modal
  cA_dp(0 To 9) As Byte     ' #7:1 Block
                            ' #6Å`#0 dp
  lAbs_pos(0 To 31) As Long ' Abs pos
  cAbs_dp(0 To 31) As Byte  ' Abs dp
  lMcn_pos(0 To 31) As Long ' Mcn pos
  cMcn_dp(0 To 31) As Byte  ' Mcn dp
End Type

Type ODBOPHIS4_MAL
  nRec_len As Integer       ' length
  nRec_type As Integer      ' record type
  nAlm_grp As Integer       ' alarm group
  nAlm_no As Integer        ' alarm number
  nAxis_no As Integer       ' axis number
  nYear As Integer          ' year
  nMonth As Integer         ' month
  nDay As Integer           ' day
  nHour As Integer          ' hour
  nMinute As Integer        ' minute
  nSecond As Integer        ' second
  nPth_no As Integer        ' path index
  nSys_alm As Integer       ' sys alarm
  nDsp_flg As Integer       ' message dsp flag
  nAxis_num As Integer      ' axis num
  sAlm_msg As String * 64   ' alarm message
  lG_modal(0 To 9) As Long  ' G code Modal
  cG_dp(0 To 9) As Byte     ' #7:1 Block
                            ' #6Å`#0 dp
  lA_modal(0 To 9) As Long  ' B,D,E,F,H,M,N,O,S,T code Modal
  cA_dp(0 To 9) As Byte     ' #7:1 Block
                            ' #6Å`#0 dp
  lAbs_pos(0 To 31) As Long ' Abs pos
  cAbs_dp(0 To 31) As Byte  ' Abs dp
  lMcn_pos(0 To 31) As Long ' Mcn pos
  cMcn_dp(0 To 31) As Byte  ' Mcn dp
End Type

Type ODBOPHIS4_OPM
  nRec_len As Integer       ' length
  nRec_type As Integer      ' record type
  nDsp_flg As Integer      ' Dysplay flag(ON/OFF)
  nOm_no As Integer        ' message number
  nYear As Integer          ' year
  nMonth As Integer         ' month
  nDay As Integer           ' day
  nHour As Integer          ' Hour
  nMinute As Integer        ' Minute
  nSecond As Integer        ' Second
  sOpe_msg As String * 256  ' Messege//msg_buf
End Type

Type ODBOPHIS4_OFS
  nRec_len As Integer       ' length
  nRec_type As Integer      ' record type
  nOfs_grp As Integer       ' Tool offset group
  nOfs_no As Integer        ' Tool offset number
  nHour As Integer          ' hour
  nMinute As Integer        ' minute
  nSecond As Integer        ' second
  nPth_no As Integer        ' path index
  lOfs_old As Long          ' old data
  lOfs_new As Long          ' new data
  nOld_dp As Integer        ' old data decimal point
  nNew_dp As Integer        ' new data decimal point
End Type

Type ODBOPHIS4_PRM
  nRec_len As Integer       ' length
  nRec_type As Integer      ' record type
  nPrm_grp As Integer       ' paramater group
  nPrm_num As Integer       ' paramater number
  nHour As Integer          ' hour
  nMinute As Integer        ' minute
  nSecond As Integer        ' second
  nPrm_len As Integer       ' paramater data length
  lPrm_no As Long           ' paramater no
  lPrm_old As Long          ' old data
  lPrm_new As Long          ' new data
  nOld_dp As Integer        ' old data decimal point
  nNew_dp As Integer        ' new data decimal point
End Type

Type ODBOPHIS4_WOF
  nRec_len As Integer       ' length
  nRec_type As Integer      ' record type
  nOfs_grp As Integer       ' Work offset group
  nOfs_no As Integer        ' Work offset number
  nHour As Integer          ' hour
  nMinute As Integer        ' minute
  nSecond As Integer        ' second
  nPth_no As Integer        ' path index
  nAxis_no As Integer       ' path axis num $*/
  nDummy As Integer
  lOfs_old As Long          ' old data
  lOfs_new As Long          ' new data
  nOld_dp As Integer        ' old data decimal point
  nNew_dp As Integer        ' new data decimal point
End Type

Type ODBOPHIS4_MAC
  nRec_len As Integer       ' length
  nRec_type As Integer      ' record type
  nMac_no As Integer        ' macro val number
  nHour As Integer          ' hour
  nMinute As Integer        ' minute
  nSecond As Integer        ' second
  nPth_no As Integer        ' path index
  lMac_old As Long          ' old data
  lMac_new As Long          ' new data
  nOld_dp As Integer        ' old data decimal point
  nNew_dp As Integer        ' old data decimal point
End Type

' cnc_rdalmhistry:read alarm history data
Type ODBAHISdata
  nDummy As Integer
  nAlm_grp As Integer           ' alarm group
  nAlm_no As Integer            ' alarm number
  sAxis_no As Byte              ' axis number
  sYear As Byte                 ' year
  sMonth As Byte                ' month
  sDay As Byte                  ' day
  sHour As Byte                 ' hour
  sMinute As Byte               ' minute
  sSecond As Byte               ' second
  sDummy2 As Byte
  nLen_msg As Integer           ' alarm message length
  sAlm_msg As String * 32       ' alarm message
End Type

Type ODBAHIS
  nS_no As Integer              ' start number
  nType As Integer              ' dummy
  nE_no As Integer              ' end number
  nAlm_his(0 To 9) As ODBAHISdata
End Type                        ' In case that the number of data is 10

' cnc_rdalmhistry2:read alarm history data
Type ODBAHIS2data
  nAlm_grp As Integer           ' alarm group
  nAlm_no As Integer            ' alarm number
  nAxis_no As Integer           ' axis number
  nYear As Integer              ' year
  nMonth As Integer             ' month
  nDay As Integer               ' day
  nHour As Integer              ' hour
  nMinute As Integer            ' minute
  nSecond As Integer            ' second
  nLen_msg As Integer           ' alarm message length
  sAlm_msg As String * 32       ' alarm message
End Type

Type ODBAHIS2
  nS_no As Integer              ' start number
  nE_no As Integer              ' end number
  nAlm_his(0 To 9) As ODBAHIS2data
End Type                        ' In case that the number of data is 10

' cnc_rdalmhistry3:read alarm history data 3
Type ODBAHIS3data
  nDummy As Integer
  nAlm_grp As Integer           ' alarm group
  nAlm_no As Integer            ' alarm number
  sAxis_no As Byte              ' axis number
  sYear As Byte                 ' year
  sMonth As Byte                ' month
  sDay As Byte                  ' day
  sHour As Byte                 ' hour
  sMinute As Byte               ' minute
  sSecond As Byte               ' second
  sDummy2 As Byte
  nLen_msg As Integer           ' alarm message length
  sAlm_msg As String * 36       ' alarm message
End Type

Type ODBAHIS3
  nS_no As Integer              ' start number
  nType As Integer              ' dummy
  nE_no As Integer              ' end number
  nAlm_his(0 To 9) As ODBAHIS3data
End Type                        ' In case that the number of data is 10

' cnc_rdalmhistry4:read alarm history data
Type ODBAHIS4data
  nAlm_grp As Integer           ' alarm group
  nAlm_no As Integer            ' alarm number
  nAxis_no As Integer           ' axis number
  nYear As Integer              ' year
  nMonth As Integer             ' month
  nDay As Integer               ' day
  nHour As Integer              ' hour
  nMinute As Integer            ' minute
  nSecond As Integer            ' second
  nLen_msg As Integer           ' alarm message length
  nPth_no As Integer            ' path index
  nDummy As Integer             ' dummy
  sAlm_msg As String * 64       ' alarm message
End Type

Type ODBAHIS4
  nS_no As Integer              ' start number
  nE_no As Integer              ' end number
  nAlm_his(0 To 9) As ODBAHIS4data
End Type                        ' In case that the number of data is 10

' cnc_rdalmhistry5:read alarm history data
Type ODBAHIS5data
  nAlm_grp As Integer              ' alarm group
  nAlm_no As Integer               ' alarm number
  nAxis_no As Integer              ' axis number
  nYear As Integer                 ' year
  nMonth As Integer                ' month
  nDay As Integer                  ' day
  nHour As Integer                 ' hour
  nMinute As Integer               ' minute
  nSecond As Integer               ' second
  nLen_msg As Integer              ' alarm message length
  nPth_no As Integer               ' path index
  nSys_alm As Integer              ' sys alarm
  nDsp_flg As Integer              ' message dsp flag
  nAxis_num As Integer             ' sum axis num
  sAlm_msg As String * 64          ' alarm message
  lG_modal(0 To 9) As Long         ' G code Modal
  cG_dp(0 To 9) As Byte            ' #7:1 Block
                                   ' #6Å`#0 dp
  lA_modal(0 To 9) As Long         ' B,D,E,F,H,M,N,O,S,T code Modal
  cA_dp(0 To 9) As Byte            ' #7:1 Block
                                   ' #6Å`#0 dp
  lAbs_pos(0 To 31) As Long        ' Abs pos
  cAbs_dp(0 To 31) As Byte         ' Abs dp
  lMcn_pos(0 To 31) As Long        ' Mcn pos
  cMcn_dp(0 To 31) As Byte         ' Mcn dp
End Type

Type ODBAHIS5
  nS_no As Integer                 ' start number
  nE_no As Integer                 ' end number
  alm_his(0 To 9) As ODBAHIS5data  ' In case that the number of data is 10
End Type

' cnc_rdomhistry2:read operater message history data'opm*/
Type ODBOMHIS2data
  nDsp_flg As Integer       ' Dysplay flag(ON/OFF)
  nOm_no As Integer         ' operater message number
  nYear As Integer          ' year
  nMonth As Integer         ' month
  nDay As Integer           ' day
  nHour As Integer          ' Hour
  nMinute As Integer        ' Minute
  nSecond As Integer        ' Second
  sOpe_msg As String * 256  ' Messege
End Type

Type ODBOMHIS2
  nS_no As Integer                 ' start number
  nE_no As Integer                 ' end number
  opm_his(0 To 9) As ODBOMHIS2data ' In case that the number of data is 10
End Type

' cnc_rdhissgnl:read signals related operation history
' cnc_wrhissgnl:write signals related operation history
Type IODBSIGdata
  nEnt_no As Integer            ' entry number
  nSig_no As Integer            ' signal number
  sSig_name As Byte             ' signal name
  sMask_pat As Byte             ' signal mask pattern
End Type

Type IODBSIG
  nDatano As Integer            ' dummy
  nType As Integer              ' dummy
  nData(0 To 19) As IODBSIGdata
End Type                        ' In case that the number of data is 20

' cnc_rdhissgnl2:read signals related operation history 2
' cnc_wrhissgnl2:write signals related operation history 2
Type IODBSIG2
  nDatano As Integer            ' dummy
  nType As Integer              ' dummy
  nData(0 To 44) As IODBSIGdata
End Type                        ' In case that the number of data is 45

' cnc_rdhissgnl3:read signals related operation history 3
' cnc_wrhissgnl3:write signals related operation history 3
Type IODBSIG3data
  nEnt_no As Integer            ' entry number
  nPmc_no As Integer            ' PMC number
  nSig_no As Integer            ' signal number
  sSig_name As Byte             ' signal name
  sMask_pat As Byte             ' signal mask pattern
End Type

Type IODBSIG3
  nDatano As Integer            ' dummy
  nType As Integer              ' dummy
  nData(0 To 59) As IODBSIG3data
End Type                        ' In case that the number of data is 45


'-------------
' CNC: Others
'-------------

' cnc_sysinfo:read CNC system information

#If FS15BD Then

Type ODBSYS
  nDummy As Integer             ' dummy
  nMax_axis As Integer          ' maximum axis number
  sCnc_type As String * 2       ' cnc type <ascii char>
  sMt_type As String * 2        ' M/T/TT <ascii char>
  sSeries As String * 4         ' series NO. <ascii char>
  sVersion As String * 4        ' version NO.<ascii char>
  sAxes As String * 2           ' axis number<ascii char>
End Type

#Else

Type ODBSYS
  nAddinfo As Integer           ' additional information
  nMax_axis As Integer          ' maximum axis number
  sCnc_type As String * 2       ' cnc type <ascii char>
  sMt_type As String * 2        ' M/T/TT <ascii char>
  sSeries As String * 4         ' series NO. <ascii char>
  sVersion As String * 4        ' version NO.<ascii char>
  sAxes As String * 2           ' axis number<ascii char>
End Type

#End If

' cnc_statinfo:read CNC status information
#If FS15D Or FS15BD Then
Type ODBST
  nDummy(0 To 1) As Integer     ' dummy
  nAut As Integer               ' selected automatic mode
  nManual As Integer            ' selected manual mode
  nRun As Integer               ' running status
  nEdit As Integer              ' editting status
  nMotion As Integer            ' axis, dwell status
  nMstb As Integer              ' m, s, t, b status
  nEmergency As Integer         ' emergency stop status
  nWrite As Integer             ' writting status
  nLabelskip As Integer         ' label skip status
  nAlarm As Integer             ' alarm status
  nWarning As Integer           ' warning status
  nBattery As Integer           ' battery status
End Type

#ElseIf FS16WD Then
Type ODBST
  nDummy(0 To 1) As Integer     ' dummy
  nAut As Integer               ' selected automatic mode
  nRun As Integer               ' running status
  nMotion As Integer            ' axis, dwell status
  nMstb As Integer              ' m, s, t, b status
  nEmergency As Integer         ' emergency stop status
  nAlarm As Integer             ' alarm status
  nEdit As Integer              ' editting status
End Type

#Else
' cnc_statinfo:read CNC status information
Type ODBST
  nDummy As Integer             ' dummy
  nTmmode As Integer            ' T/M mode
  nAut As Integer               ' selected automatic mode
  nRun As Integer               ' running status
  nMotion As Integer            ' axis, dwell status
  nMstb As Integer              ' m, s, t, b status
  nEmergency As Integer         ' emergency stop status
  nAlarm As Integer             ' alarm status
  nEdit As Integer              ' editting status
End Type

#End If

' cnc_statinfo2:read CNC status information 2
Type ODBST2
  nHdck As Integer       ' handl retrace status
  nTmmode As Integer     ' T/M mode
  nAut As Integer        ' selected automatic mode
  nRun As Integer        ' running status
  nMotion As Integer     ' axis, dwell status
  nMstb As Integer       ' m, s, t, b status
  nEmergency As Integer  ' emergency stop status
  nAlarm As Integer      ' alarm status
  nEdit As Integer       ' editting status
  nWarning As Integer    ' warning status
  nO3dchk As Integer     ' o3dchk status
  nExt_opt As Integer    ' option
  nRestart As Integer    ' State of edit when SBK
End Type

' cnc_alarm:read alarm status
Type ODBALM
  nDummy(0 To 1) As Integer     ' dummy
  nData As Integer              ' alarm status
End Type

' cnc_rdalminfo:read alarm information
Type ALMINFO1data
  nAxis As Integer
  nAlm_no As Integer
End Type

Type ALMINFO1
  nAlm(0 To 4) As ALMINFO1data
  nData_end As Integer
End Type                        ' In case that the number of alarm is 5

Type ALMINFO2data
  nAxis As Integer
  nAlm_no As Integer
  nMsg_len As Integer
  sAlm_msg As String * 32
End Type

Type ALMINFO2
  nAlm(0 To 4) As ALMINFO2data
  nData_end As Integer
End Type                        ' In case that the number of alarm is 5

Type ALMINFO3data
  nAxis As Long
  nAlm_no As Integer
End Type

Type ALMINFO3
  nAlm(0 To 4) As ALMINFO3data
  nData_end As Integer
End Type                        ' In case that the number of alarm is 5

Type ALMINFO4data
  nAxis As Long
  nAlm_no As Integer
  nMsg_len As Integer
  sAlm_msg As String * 32
End Type

Type ALMINFO4
  nAlm(0 To 4) As ALMINFO4data
  nData_end As Integer
End Type                        ' In case that the number of alarm is 5

' cnc_rdalmmsg:read alarm messages
Type ODBALMMSG
  lAlm_no As Long
  nType As Integer
  nAxis As Integer
  nDummy As Integer
  nMsg_len As Integer
  sAlm_msg As String * 32
End Type

Type ODBALMMSG2
  alm_no As Long
  type As Integer
  axis As Integer
  dummy As Integer
  msg_len As Integer
  alm_msg As String * 64
End Type

' cnc_modal:read modal data
Type ODBMDLdata
  lAux_data As Long
  sFlag1 As Byte
  sFlag2 As Byte
End Type

Type ODBMDL1
  nDatano As Integer
  nType As Integer
  sG_data As Byte
End Type

Type ODBMDL2
  nDatano As Integer
  nType As Integer
  sG_rdata(0 To 34) As Byte
End Type

Type ODBMDL3
  nDatano As Integer
  nType As Integer
  nAux As ODBMDLdata
End Type

Type ODBMDL4
  nDatano As Integer
  nType As Integer
  nRaux1(0 To 26) As ODBMDLdata
End Type

Type ODBMDL5
  nDatano As Integer
  nType As Integer
  nRaux2(0 To MAX_AXIS - 1) As ODBMDLdata
End Type

Type ODBMDL6
  nDatano As Integer
  nType As Integer
  sG_1shot(0 To 3) As Byte
End Type

' cnc_rdgcode:read modal G code
Type ODBGCD
  nGroup As Integer             ' G code group
  nFlag As Integer              ' Flag
  sCode As String * 8           ' G code
End Type

' cnc_rdcommand:read command value
Type ODBCMD
  nAdrs As Byte
  nNum As Byte
  nFlag As Integer
  nCmd_val As Long
  nDec_val As Long
End Type

' cnc_diagnoss:read diagnosis data
' cnc_diagnosr:read diagnosis data(area specified)
Type ODBDGN1
  nDatano As Integer            ' data number
  nType As Integer              ' axis number
  sCdata As Byte
End Type

Type ODBDGN2
  nDatano As Integer            ' data number
  nType As Integer              ' axis number
  nIdata As Integer
End Type

Type ODBDGN3
  nDatano As Integer            ' data number
  nType As Integer              ' axis number
  lLdata As Long
End Type

Type ODBDGN4
  nDatano As Integer            ' data number
  nType As Integer              ' axis number
  sCdatas(0 To MAX_AXIS - 1) As Byte
End Type

Type ODBDGN5
  nDatano As Integer            ' data number
  nType As Integer              ' axis number
  nIdatas(0 To MAX_AXIS - 1) As Integer
End Type

Type ODBDGN6
  nDatano As Integer            ' data number
  nType As Integer              ' axis number
  lLdatas(0 To MAX_AXIS - 1) As Long
End Type

Type REALDGN
  lDgn_val As Long
  lDec_val As Long
End Type

Type ODBDGN7
  nDatano As Integer            ' data number
  nType As Integer              ' axis number
  nRdata As REALDGN
End Type

Type ODBDGN8
  nDatano As Integer            ' data number
  nType As Integer              ' axis number
  nRdatas(0 To MAX_AXIS - 1) As REALDGN
End Type

#If FS15BD Then
' cnc_adcnv:read A/D conversion data
Type ODBAD
  nDatano As Integer            ' input analog voltage type
  nType As Integer              ' analog voltage type
  nData As Byte                 ' digital voltage data
End Type

#Else

' cnc_adcnv:read A/D conversion data
Type ODBAD
  nDatano As Integer            ' input analog voltage type
  nType As Integer              ' analog voltage type
  nData As Integer              ' digital voltage data
End Type

#End If

#If FS15D Or FS15BD Then

' cnc_rdopmsg:read operator's message
Type MSG
  nDatano As Integer            ' operator's message number
  nType As Integer              ' operator's message type
  nChar_num As Integer          ' message string length
  sData As String * 129         ' operator's message string
End Type                        ' In case that the data length is 129

#Else

' cnc_rdopmsg:read operator's message
Type MSG
  nDatano As Integer            ' operator's message number
  nType As Integer              ' operator's message type
  nChar_num As Integer          ' message string length
  sData As String * 256         ' operator's message string
End Type                        ' In case that the data length is 256

#End If

' cnc_rdopmsg2:read operator's message
Type MSG2
  nDatano As Integer            ' operator's message number
  nType As Integer              ' operator's message type
  nChar_num As Integer          ' message string length
  sData As String * 64          ' operator's message string
End Type                        ' In case that the data length is 64

' cnc_rdopmsg3:read operator's message
Type MSG3
  nDatano As Integer            ' operator's message number
  nType As Integer              ' operator's message type
  nChar_num As Integer          ' message string length
  sData As String * 256         ' operator's message string
End Type                        ' In case that the data length is 256

' cnc_rdopmsgmps:read operator message for MAPPS
Type OPMSGMPS
  nDatano As Integer            ' operator's message number
  nType As Integer              ' operator's message type
  nChar_num As Integer          ' message string length
  sData As String * 256         ' operator's message string
End Type                        ' In case that the data length is 256

' cnc_sysconfig:read CNC configuration information
#If FS15BD Then
Type ODBSYSC
  sSlot_no_p(0 To 15) As Byte
  sSlot_no_l(0 To 15) As Byte
  nMod_id(0 To 15) As Integer
  nSoft_id(0 To 15) As Integer
  sS_series(0 To 15) As String * 5
  sS_version(0 To 15) As String * 5
  sSys_id(0 To 15) As Byte
  sSys_ser As String * 5
  sSys_ver As String * 5
  sBasic_ver As String * 5
  sOpta1_ver As String * 5
  sOpta2_ver As String * 5
  sOpta3_ver As String * 5
  sOpta4_ver As String * 5
  sSub_ver As String * 5
  sOpts1_ver As String * 5
  sTcopt_ver As String * 5
  sAxis_ser As String * 5
  sAxis_ver As String * 5
  sHelp_ser As String * 5
  sHelp_ver As String * 5
  sBoot_ser As String * 5
  sBoot_ver As String * 5
  sServo_ser As String * 5
  sServo_ver As String * 5
  sCmpl_ser As String * 5
  sCmpl_ver As String * 5
  sSral1_ser As String * 5
  sSral1_ver As String * 5
  sSral2_ser As String * 5
  sSral2_ver As String * 5
  sSral3_ser As String * 5
  sSral3_ver As String * 5
  sSral4_ser As String * 5
  sSral4_ver As String * 5
  nPcb_info(0 To 19) As Integer
  nPcb_note(0 To 19, 0 To 15) As Integer
End Type

#ElseIf PMD Then
Type ODBSYSC
  sSlot_no_p(0 To 15) As Byte
  sSlot_no_l(0 To 15) As Byte
  nMod_id(0 To 15) As Integer
  nSoft_id(0 To 15) As Integer
  sS_series(0 To 15) As String * 5
  sS_version(0 To 15) As String * 5
  nDummy(0 To 15) As Byte
  nM_rom As Integer
  nS_rom As Integer
  sSvo_soft As String * 8
  sPmc_soft As String * 6
  sLad_soft As String * 6
  sMcr_soft As String * 8
  sSpl1_soft As String * 6
  sSpl2_soft As String * 6
  nFrmmin As Integer
  nDrmmin As Integer
  nSrmmin As Integer
  nPmcmin As Integer
  nSv1min As Integer
  nSv3min As Integer
  nSv5min As Integer
  nSicmin As Integer
  nPosmin As Integer
  nSubmin As Integer
  nHdiio As Integer
  nDummy2(0 To 31) As Integer
End Type

#Else
Type ODBSYSC
  sSlot_no_p(0 To 15) As Byte
  sSlot_no_l(0 To 15) As Byte
  nMod_id(0 To 15) As Integer
  nSoft_id(0 To 15) As Integer
  sS_series(0 To 15) As String * 5
  sS_version(0 To 15) As String * 5
  nDummy(0 To 15) As Byte
  nM_rom As Integer
  nS_rom As Integer
  sSvo_soft As String * 8
  sPmc_soft As String * 6
  sLad_soft As String * 6
  sMcr_soft As String * 8
  sSpl1_soft As String * 6
  sSpl2_soft As String * 6
  nFrmmin As Integer
  nDrmmin As Integer
  nSrmmin As Integer
  nPmcmin As Integer
  nCrtmin As Integer
  nSv1min As Integer
  nSv3min As Integer
  nSicmin As Integer
  nPosmin As Integer
  nDrmmrc As Integer
  nDrmarc As Integer
  nPmcmrc As Integer
  nDmaarc As Integer
  nIopt As Integer
  nHdiio As Integer
  nFrmsub As Integer
  nDrmsub As Integer
  nSrmsub As Integer
  nSv5sub As Integer
  nSv7sub As Integer
  nSicsub As Integer
  nPossub As Integer
  nHamsub As Integer
  nGm2gr1 As Integer
  nCrtgr2 As Integer
  nGm1gr2 As Integer
  nGm2gr2 As Integer
  nCmmrb As Integer
  nSv5axs As Integer
  nSv7axs As Integer
  nSicaxs As Integer
  nPosaxs As Integer
  nHanaxs As Integer
  nRomr64 As Integer
  nSrmr64 As Integer
  nDr1r64 As Integer
  nDr2r64 As Integer
  nIopio2 As Integer
  nHdiio2 As Integer
  nCmmrb2 As Integer
  nRomfap As Integer
  nSrmfap As Integer
  nDrmfap As Integer
End Type

#End If

' cnc_rdprstrinfo:read program restart information
Type ODBPRS
  nDatano As Integer            ' dummy
  nType As Integer              ' dummy
  nData_info(0 To 4) As Integer ' data setting information
  lRstr_bc As Long              ' block counter
  lRstr_m(0 To 34) As Long      ' M code value
  lRstr_t(0 To 1) As Long       ' T code value
  lRstr_s As Long               ' S code value
  lRstr_b As Long               ' B code value
  lDest(0 To MAX_AXIS - 1) As Long ' program re-start position
  lDist(0 To MAX_AXIS - 1) As Long ' program re-start distance
End Type

#If FS15D Or FS15BD Then

' cnc_rdopnlsgnl:read output signal image of software operator's panel
' cnc_wropnlsgnl:write output signal of software operator's panel
Type IODBSGNL
  nDatano As Integer            ' dummy
  nType As Integer              ' data select flag
  nMode As Integer              ' mode signal
  nHndl_ax As Integer           ' Manual handle feed axis selection signal
  nHndl_mv As Integer           ' Manual handle feed travel distance selection signal
  nRpd_ovrd As Integer          ' rapid traverse override signal
  nJog_ovrd As Integer          ' manual feedrate override signal
  nFeed_ovrd As Integer         ' feedrate override signal
  nSpdl_ovrd As Integer         ' (not used)
  nBlck_del As Integer          ' optional block skip signal
  nSngl_blck As Integer         ' single block signal
  nMachn_lock As Integer        ' machine lock signal
  nDry_run As Integer           ' dry run signal
  nMem_prtct As Integer         ' memory protection signal
  nFeed_hold As Integer         ' automatic operation halt signal
  nManual_rpd As Integer        ' manual rapid traverse selection signal
  nDummy(0 To 1) As Integer     ' (reserve)
End Type

#Else

' cnc_rdopnlsgnl:read output signal image of software operator's panel
' cnc_wropnlsgnl:write output signal of software operator's panel
Type IODBSGNL
  nDatano As Integer            ' dummy
  nType As Integer              ' data select flag
  nMode As Integer              ' mode signal
  nHndl_ax As Integer           ' Manual handle feed axis selection signal
  nHndl_mv As Integer           ' Manual handle feed travel distance selection signal
  nRpd_ovrd As Integer          ' rapid traverse override signal
  nJog_ovrd As Integer          ' manual feedrate override signal
  nFeed_ovrd As Integer         ' feedrate override signal
  nSpdl_ovrd As Integer         ' (not used)
  nBlck_del As Integer          ' optional block skip signal
  nSngl_blck As Integer         ' single block signal
  nMachn_lock As Integer        ' machine lock signal
  nDry_run As Integer           ' dry run signal
  nMem_prtct As Integer         ' memory protection signal
  nFeed_hold As Integer         ' automatic operation halt signal
End Type

#End If

' cnc_rdopnlgnrl:read general signal image of software operator's panel
' cnc_wropnlgnrl:write general signal image of software operator's panel
Type IODBGNRL
  nDatano As Integer            ' dummy
  nType As Integer              ' data select flag
  sSgnal As Byte                ' general signal
End Type

' cnc_rdopnlgsname:read general signal name of software operator's panel
' cnc_wropnlgsname:write general signal name of software operator's panel
Type IODBRDNA
  nDatano As Integer            ' dummy
  nType As Integer              ' data select flag
  sSgnl1_name As String * 9     ' general signal 1 name
  sSgnl2_name As String * 9     ' general signal 2 name
  sSgnl3_name As String * 9     ' general signal 3 name
  sSgnl4_name As String * 9     ' general signal 4 name
  sSgnl5_name As String * 9     ' general signal 5 name
  sSgnl6_name As String * 9     ' general signal 6 name
  sSgnl7_name As String * 9     ' general signal 7 name
  sSgnl8_name As String * 9     ' general signal 8 name
End Type

' cnc_getdtailerr:get detail error
Type ODBERR
  nErr_no As Integer
  nErr_dtno As Integer
End Type


' cnc_rdparainfo:read informations of CNC parameter
Type ODBPARAIFdata
  nPrm_no As Integer
  nPrm_type As Integer
End Type

Type ODBPARAIF
  nInfo_no As Integer
  nPrev_no As Integer
  nNext_no As Integer
  nInfo(0 To 9) As ODBPARAIFdata
End Type                        ' In case that the number of data is 10

' cnc_rdparainfo3:read informations of CNC parameter(3)
Type ODBPARAIF2
  nPrm_no As Integer
  nSize As Integer
  nArray As Integer
  nUnit As Integer
  nDim As Integer
  nInput As Integer
  nDisplay As Integer
  nOthers As Integer
End Type

' cnc_rdsetinfo:read informations of CNC setting data
Type ODBSETIFdata
  nSet_no As Integer
  nSet_type As Integer
End Type

Type ODBSETIF
  nInfo_no As Integer
  nPrev_no As Integer
  nNext_no As Integer
  nInfo(0 To 9) As ODBSETIFdata
End Type                        ' In case that the number of data is 10

' cnc_rddiaginfo:read informations of CNC diagnose data
Type ODBDIAGIFdata
  nDiag_no As Integer
  nDiag_type As Integer
End Type

Type ODBDIAGIF
  nInfo_no As Integer
  nPrev_no As Integer
  nNext_no As Integer
  nInfo(0 To 9) As ODBDIAGIFdata
End Type                        ' In case that the number of data is 10

' cnc_rdparanum:read maximum, minimum and total number of CNC parameter
Type ODBPARANUM
  nPara_min As Integer
  nPara_max As Integer
  nTotal_no As Integer
End Type

' cnc_rdsetnum:read maximum, minimum and total number of CNC setting data
Type ODBSETNUM
  nSet_min As Integer
  nSet_max As Integer
  nTotal_no As Integer
End Type

' cnc_rddiagnum:read maximum, minimum and total number of CNC diagnose data
Type ODBDIAGNUM
  nDiag_min As Integer
  nDiag_max As Integer
  nTotal_no As Integer
End Type

' cnc_rdfrominfo:read F-ROM information on CNC
Type ODBFINFOdata
  sSys_name As String * 12   ' F-ROM SYSTEM data Name
  lFrom_size As Long         ' F-ROM Size
End Type

Type ODBFINFO
  sSlot_name As String * 12  ' Slot Name
  lFrom_num  As Long         ' Number of F-ROM SYSTEM data
  nInfo(0 To 31) As ODBFINFOdata
End Type

' cnc_getfrominfo:read F-ROM information on CNC
Type ODBFINFORMdata
  sSys_name As String * 12   ' F-ROM SYSTEM data Name
  lFrom_size As Long         ' F-ROM Size
  lFrom_attrib As Long       ' F-ROM data attribute
End Type

Type ODBFINFORM
  lSlot_no As Long           ' Slot Number
  sSlot_name As String * 12  ' Slot Name
  lFrom_num  As Long         ' Number of F-ROM SYSTEM data
  nInfo(0 To 127) As ODBFINFORMdata
End Type

' cnc_rdsraminfo:read S-RAM information on CNC
' cnc_getsraminfo:read S-RAM information on CNC
Type ODBSINFOdata
  sSram_name As String * 12      ' S-RAM data Name
  lSram_size As Long             ' S-RAM data Size
  nDiv_number As Integer         ' Division number of S-RAM file
  sFname(0 To 6) As String * 16  ' S-RAM file names
End Type

Type ODBSINFO
  lSram_num As Long              ' Number of S-RAM data
  nInfo(0 To 7) As ODBSINFOdata
End Type

' cnc_rdsramaddr:read S-RAM address on CNC
Type SRAMADDR
  nType As Integer           ' SRAM data type
  lSize As Long              ' SRAM data size
  lOffset As Long            ' offset from top address of SRAM
End Type

' cnc_dtsvrdpgdir:read file directory in Data Server
Type ODBDSDIRdata
  sFile_name As String * 16
  sComment As String * 64
  nSize As Long
  sDate As String * 16
End Type

Type ODBDSDIR
  nFile_num As Long
  nRemainder As Long
  nData_num As Integer
  nData(0 To 31) As ODBDSDIRdata
End Type

' cnc_dtsvrdset:read setting data for Data Server
' cnc_dtsvwrset:write setting data for Data Server
Type IODBDSSET
  sHost_ip As String * 16
  sHost_uname As String * 32
  sHost_passwd As String * 32
  sHost_dir As String * 128
  sDtsv_mac As String * 13
  sDtsv_ip As String * 16
  sDtsv_mask As String * 16
End Type

' cnc_dtsvmntinfo:read maintenance information for Data Server
Type ODBDSMNT
  lEmpty_cnt As Long
  lTotal_size As Long
  lRead_ptr As Long
  lWrite_ptr As Long
End Type

' cnc_rdposerrs2:read the position deviation S1 and S2
Type ODBPSER
  nPoserr1 As Long
  nPoserr2 As Long
End Type

' cnc_rdctrldi:read the control input signal
Type ODBSPDIdata
  nSgnl1 As Byte
  nSgnl2 As Byte
  nSgnl3 As Byte
  nSgnl4 As Byte
End Type

Type ODBSPDI
  nData(0 To 2) As ODBSPDIdata
End Type ' In case that the number of data is 3

' cnc_rdctrldo:read the control output signal
Type ODBSPDOdata
  nSgnl1 As Byte
  nSgnl2 As Byte
  nSgnl3 As Byte
  nSgnl4 As Byte
End Type

Type ODBSPDO
  nData(0 To 2) As ODBSPDOdata
End Type ' In case that the number of data is 3

' cnc_rdsvfeedback:Read Servo feedback multiplication data
Type ODBSVFBACK
  nDummy As Integer
  nDtype As Integer
  lFback(0 To MAX_AXIS - 1) As Long
  lAfback(0 To MAX_AXIS - 1) As Long
End Type

' cnc_rdwaveprm:read the parameter of wave diagnosis
' cnc_wrwaveprm:write the parameter of wave diagnosis
Type CHDATA
  nKind As Integer
  nAxis As Integer              ' nKind == 13 --> Low byte  : signal address
'                 High byte : bit number
  nIo_no As Integer             ' nKind == 13 --> signal number
End Type

Type IODBWAVE
  nCondition As Integer
  sTrg_adr As Byte
  sTrg_bit As Byte
  nTrg_no As Integer
  nDelay As Integer
  nT_range As Integer
  nCh(0 To 11) As CHDATA
End Type

' cnc_rdwaveprm2:read the parameter of wave diagnosis 2
' cnc_wrwaveprm2:write the parameter of wave diagnosis 2
Type WVPRMdata1                 ' nKind is except 13
  nKind As Integer
  nAxis As Long
  nReserve2 As Long
End Type

Type WVPRMdata2                 ' nKind is 13
  nKind As Integer
  sAdr As Byte
  sBit As Byte
  nNo As Integer
  nReserve2 As Long
End Type

Type IODBWVPRM
  nCondition As Integer
  sTrg_adr As Byte
  sTrg_bit As Byte
  nTrg_no As Integer
  nReserve1 As Integer
  nDelay As Long
  nT_range As Long
  nCh1(0 To 7) As WVPRMdata1
  nCh2(0 To 3) As WVPRMdata2
End Type

' cnc_rdwavedata:read the data of wave diagnosis
Type ODBWVDT
  nChannel As Integer
  nKind As Integer
  nAxis As Integer              ' nKind == 13 --> Low byte  : signal address
'                 High byte : bit number
  nIo_no As Integer             ' nKind == 13 --> signal number
  sYear As Byte
  sMonth As Byte
  sDay As Byte
  sHour As Byte
  sMinute As Byte
  sSecond As Byte
  nT_cycle As Integer
  nData(0 To 8191) As Integer
End Type

' cnc_rdmgrpdata:read M-code group data
Type ODBMGRP
  lM_code As Long
  nGrp_no As Integer
  sM_name As String * 21
  sDummy As Byte
End Type

' cnc_wrmgrpdata:write M-code group data
Type IDBMGRP
  nS_no As Integer
  nDummy As Integer
  nNum As Integer
  nGroup(0 To 499) As Integer
End Type

' cnc_rdexecmcode:read executing M-code group data
Type ODBEXEMdata
  lNo As Long
  nFlag As Integer
End Type

Type ODBEXEM
  nGrp_no As Integer
  nMem_no As Integer
  nM_code(0 To 4) As ODBEXEMdata
  sM_name As String * 21
  sDummy As Byte
End Type

' cnc_rdrstrmcode:read program restart M-code group data
Type ODBRSTRMdata
  lNo As Long
  nFlag As Integer
End Type

Type ODBRSTRM
  nGrp_no As Integer
  nMem_no As Integer
  nM_code(0 To 4) As ODBRSTRMdata
End Type

' cnc_rdproctime:read processing time stamp data
Type ODBPTIMEdata
  lPrg_no As Long
  nHour As Integer
  sMinute As Byte
  sSecond As Byte
End Type

Type ODBPTIME
  nNum As Integer
  nData(0 To 9) As ODBPTIMEdata
End Type

' cnc_rdprgdirtime:read program directory for processing time data
Type PRGDIRTM
  lPrg_no As Long
  sComment As String * 51
  sCuttime As String * 13
End Type

' cnc_rdprogdir2:read program directory 2
#If ONO8D Then

Type PRGDIR2
  lNumber As Long
  lLength As Long
  sComment As String * 51
  sDummy As Byte
End Type

#Else

Type PRGDIR2
  nNumber As Integer
  lLength As Long
  sComment As String * 51
  sDummy As Byte
End Type

#End If

' cnc_rdprogdir3:read program directory 3
Type PRGDIR3mdata
  nYear As Integer
  nMonth As Integer
  nDay As Integer
  nHour As Integer
  nMinute As Integer
  nDummy As Integer
End Type

Type PRGDIR3cdata
  nYear As Integer
  nMonth As Integer
  nDay As Integer
  nHour As Integer
  nMinute As Integer
  nDummy As Integer
End Type

Type PRGDIR3
  lNumber As Long
  lLength As Long
  lPage As Long
  sComment As String * 52
  nMdata As PRGDIR3mdata
  nCdata As PRGDIR3cdata
End Type

' cnc_rdprogdir4:read program directory 4
Type PRGDIR4mdata
  nYear As Integer
  nMonth As Integer
  nDay As Integer
  nHour As Integer
  nMinute As Integer
  nDummy As Integer
End Type

Type PRGDIR4cdata
  nYear As Integer
  nMonth As Integer
  nDay As Integer
  nHour As Integer
  nMinute As Integer
  nDummy As Integer
End Type

Type PRGDIR4
  lNumber As Long
  lLength As Long
  lPage As Long
  sComment As String * 52
  nMdata As PRGDIR4mdata
  nCdata As PRGDIR4cdata
End Type

' cnc_rdcomparam:read communication parameter for DNC1, DNC2, OSI-Ethernet
' cnc_wrcomparam:write communication parameter for DNC1, DNC2, OSI-Ethernet
Type IODBCPRM
  sNcApli As String * 65
  sDummy1 As Byte
  sHostApli As String * 65
  sDummy2 As Byte
  lStatPstv As Long
  lStatNgtv As Long
  lStatmask As Long
  lAlarmStat As Long
  lPsclHaddr As Long
  lPsclLaddr As Long
  nSvcMode1 As Integer
  nSvcMode2 As Integer
  lFileTout As Long
  lRemTout As Long
End Type

' cnc_rdintchk:read interference check
' cnc_wrintchk:write interference check
Type IODBINT
  nDatano_s As Integer          ' start offset number
  nType As Integer              ' kind of position
  nDatano_e As Integer          ' end offset number
  lData(0 To 23) As Long        ' position value of area for not attach
End Type

' cnc_rdwkcdshft:read work coordinate shift
' cnc_wrwkcdshft:write work coordinate shift
' cnc_rdwkcdsfms:read work coordinate shift measure
' cnc_wrwkcdsfms:write work coordinate shift measure
Type IODBWCSF
  nDatano As Integer            ' data number
  nType As Integer              ' axis number
  lData(0 To MAX_AXIS - 1) As Long ' data value
End Type

' cnc_rdomhisinfo:read operator message history information
Type ODBOMIF
  nOm_max As Integer
  nOm_sum As Integer
  nOm_char As Integer
End Type

' cnc_rdomhistry:read operator message history
Type ODBOMHIS
  nOm_no As Integer
  nYear As Integer
  nMonth As Integer
  nDay As Integer
  nHour As Integer
  nMinute As Integer
  nSecond As Integer
  sOm_msg As String * 256
End Type

' cnc_rdbtofsr:read b-axis tool offset value(area specified)
' cnc_wrbtofsr:write b-axis tool offset value(area specified)
Type IODBBTO
  nDatano_s As Integer          ' start offset number
  nType As Integer              ' offset type
  nDatano_e As Integer          ' end offset number
  lOfs(0 To 17) As Long         ' offset
End Type                        ' In case that the number of data is 9 (B type)

' cnc_rdbtofsinfo:read b-axis tool offset information
Type ODBBTLINF
  nOfs_type As Integer
  nUse_no As Integer
  nSub_no As Integer
End Type

' cnc_rdbaxis:read b-axis command
Type ODBBAXIS
  nFlag As Integer
  nCommand As Integer
  nSpeed As Integer
  lSub_data As Long
End Type

' cnc_rdsyssoft:read CNC system soft series and version
Type ODBSYSSseries
  sSoft_series(0 To 15) As String * 5
End Type

Type ODBSYSSversion
  sSoft_version(0 To 15) As String * 5
End Type

Type ODBSYSS
  sSlot_no_p(0 To 15) As Byte
  sSlot_no_l(0 To 15) As Byte
  nModule_id(0 To 15) As Integer
  nSoft_id(0 To 15) As Integer
  Series As ODBSYSSseries
  Version As ODBSYSSversion
  nSoft_inst As Integer
  sBoot_ser As String * 5
  sBoot_ver As String * 5
  sServo_ser As String * 5
  sServo_ver As String * 5
  sPmc_ser As String * 5
  sPmc_ver As String * 5
  sLadder_ser As String * 5
  sLadder_ver As String * 5
  sMcrlib_ser As String * 5
  sMcrlib_ver As String * 5
  sMcrapl_ser As String * 5
  sMcrapl_ver As String * 5
  sSpl1_ser As String * 5
  sSpl1_ver As String * 5
  sSpl2_ser As String * 5
  sSpl2_ver As String * 5
  sSpl3_ser As String * 5
  sSpl3_ver As String * 5
  sC_exelib_ser As String * 5
  sC_exelib_ver As String * 5
  sC_exeapl_ser As String * 5
  sC_exeapl_ver As String * 5
  sInt_vga_ser As String * 5
  sInt_vga_ver As String * 5
  sOut_vga_ser As String * 5
  sOut_vga_ver As String * 5
  sPmm_ser As String * 5
  sPmm_ver As String * 5
  sPmc_mng_ser As String * 5
  sPmc_mng_ver As String * 5
  sPmc_shin_ser As String * 5
  sPmc_shin_ver As String * 5
  sPmc_shout_ser As String * 5
  sPmc_shout_ver As String * 5
  sPmc_c_ser As String * 5
  sPmc_c_ver As String * 5
  sPmc_edit_ser As String * 5
  sPmc_edit_ver As String * 5
  sLddr_mng_ser As String * 5
  sLddr_mng_ver As String * 5
  sLddr_apl_ser As String * 5
  sLddr_apl_ver As String * 5
  sSpl4_ser As String * 5
  sSpl4_ver As String * 5
  sMcr2_ser As String * 5
  sMcr2_ver As String * 5
  sMcr3_ser As String * 5
  sMcr3_ver As String * 5
  sEth_boot_ser As String * 5
  sEth_boot_ver As String * 5
  sReserve(0 To 7) As String * 5
End Type

' cnc_rdsyssoft2:read CNC system soft series and version(2)
Type ODBSYSS2
  sSlot_no_p(0 To 15) As Byte
  sSlot_no_l(0 To 15) As Byte
  nModule_id(0 To 15) As Integer
  nSoft_id(0 To 15) As Integer
  Series As ODBSYSSseries
  Version As ODBSYSSversion
  nSoft_inst As Integer
  sBoot_ser As String * 5
  sBoot_ver As String * 5
  sServo_ser As String * 5
  sServo_ver As String * 5
  sPmc_ser As String * 5
  sPmc_ver As String * 5
  sLadder_ser As String * 5
  sLadder_ver As String * 5
  sMcrlib_ser As String * 5
  sMcrlib_ver As String * 5
  sMcrapl_ser As String * 5
  sMcrapl_ver As String * 5
  sSpl1_ser As String * 5
  sSpl1_ver As String * 5
  sSpl2_ser As String * 5
  sSpl2_ver As String * 5
  sSpl3_ser As String * 5
  sSpl3_ver As String * 5
  sC_exelib_ser As String * 5
  sC_exelib_ver As String * 5
  sC_exeapl_ser As String * 5
  sC_exeapl_ver As String * 5
  sInt_vga_ser As String * 5
  sInt_vga_ver As String * 5
  sOut_vga_ser As String * 5
  sOut_vga_ver As String * 5
  sPmm_ser As String * 5
  sPmm_ver As String * 5
  sPmc_mng_ser As String * 5
  sPmc_mng_ver As String * 5
  sPmc_shin_ser As String * 5
  sPmc_shin_ver As String * 5
  sPmc_shout_ser As String * 5
  sPmc_shout_ver As String * 5
  sPmc_c_ser As String * 5
  sPmc_c_ver As String * 5
  sPmc_edit_ser As String * 5
  sPmc_edit_ver As String * 5
  sLddr_mng_ser As String * 5
  sLddr_mng_ver As String * 5
  sLddr_apl_ser As String * 5
  sLddr_apl_ver As String * 5
  sSpl4_ser As String * 5
  sSpl4_ver As String * 5
  sMcr2_ser As String * 5
  sMcr2_ver As String * 5
  sMcr3_ser As String * 5
  sMcr3_ver As String * 5
  sEth_boot_ser As String * 5
  sEth_boot_ver As String * 5
  sReserve(0 To 7) As String * 5
  embEthe_ser As String * 5
  embEthe_ver As String * 5
  sReserve2(0 To 37) As String * 5
End Type

' cnc_rdsyssoft3:read CNC system soft series and version (3)
Type ODBSYSS3
  nSoft_id As Integer
  sSoft_series As String * 5
  sSoft_edition As String * 5
End Type

Type ODBSYSH
  lId1 As Long
  lId2 As Long
  nGroup_id As Integer
  nHard_id As Integer
  nHard_num As Integer
  nSlot_no As Integer
  nId1_format As Integer
  nId2_format As Integer
End Type

Type ODBSYSS3_STR
  nSoft_id As Integer
  sSoft_name As String * 13
  sSoft_series As String * 5
  sSoft_edition As String * 5
  sDummy As String * 3
End Type

Type ODBSYSH_STR
  nGroup_id As Integer
  sGroup_name As String * 14
  nHard_id As Integer
  sHard_name As String * 13
  sId1 As String * 11
  sId9 As String * 9
  sSlot_no As String * 3
  sDummy As String * 2
End Type

' cnc_rdmdlconfig:read CNC module configuration information
Type ODBMDLC
  nFrom As Integer
  nDram As Integer
  nSram As Integer
  nPmc As Integer
  nCrtc As Integer
  nServo12 As Integer
  nServo34 As Integer
  nServo56 As Integer
  nServo78 As Integer
  nSic As Integer
  nPos_lsi As Integer
  nHi_aio As Integer
  nReserve(0 To 11) As Integer
  nDrmmrc As Integer
  nDrmarc As Integer
  nPmcmrc As Integer
  nDmaarc As Integer
  nIopt As Integer
  nHdiio As Integer
  nGm2gr1 As Integer
  nCrtgr2 As Integer
  nGm1gr2 As Integer
  nGm2gr2 As Integer
  nCmmrb As Integer
  nSv5axs As Integer
  nSv7axs As Integer
  nSicaxs As Integer
  nPosaxs As Integer
  nHamaxs As Integer
  nRomr64 As Integer
  nSrmr64 As Integer
  nDr1r64 As Integer
  nDr2r64 As Integer
  nIopio2 As Integer
  nHdiio2 As Integer
  nCmmrb2 As Integer
  nRomfap As Integer
  nSrmfap As Integer
  nDrmfap As Integer
  nDrmare As Integer
  nPmcmre As Integer
  nDmaare As Integer
  nFrmbgg As Integer
  nDrmbgg As Integer
  nAsrbgg As Integer
  nEdtpsc As Integer
  nSlcpsc As Integer
  nReserve2(0 To 33) As Integer
End Type

' read CNC module configuration information 2
Type RDMDLCONFIG2
  sData(0 To 127) As Byte
End Type

' cnc_rdpscdproc:read processing condition file (processing data)
' cnc_wrpscdproc:write processing condition file (processing data)
Type IODBPSCD
  nSlct As Integer
  lFeed As Long
  nPower As Integer
  nFreq As Integer
  nDuty As Integer
  nG_press As Integer
  nG_kind As Integer
  nG_ready_t As Integer
  nDisplace As Integer
  lSupple As Long
  nEdge_slt As Integer
  nAppr_slt As Integer
  nPwr_ctrl As Integer
  nReserve(0 To 3) As Integer
End Type

' cnc_rdpscdpirc:read processing condition file (piercing data)
' cnc_wrpscdpirc:write processing condition file (piercing data)
Type IODBPIRC
  nSlct As Integer
  nPower As Integer
  nFreq As Integer
  nDuty As Integer
  nI_freq As Integer
  nI_duty As Integer
  nStep_t As Integer
  nStep_sum As Integer
  lPier_t As Long
  nG_press As Integer
  nG_kind As Integer
  nG_time As Integer
  nDef_pos As Integer
  nReserve(0 To 3) As Integer
End Type

' cnc_rdpscdedge:read processing condition file (edging data)
' cnc_wrpscdedge:write processing condition file (edging data)
Type IODBEDGE
  nSlct As Integer
  nAngle As Integer
  nPower As Integer
  nFreq As Integer
  nDuty As Integer
  lPier_t As Long
  nG_press As Integer
  nG_kind As Integer
  lR_len As Long
  nR_feed As Integer
  nR_freq As Integer
  nR_duty As Integer
  nGap As Integer
  nReserve(0 To 3) As Integer
End Type

' cnc_rdpscdslop:read processing condition file (slope data)
' cnc_wrpscdslop:write processing condition file (slope data)
Type IODBSLOP
  lSlct As Long
  lUpleng As Long
  nUpsp(0 To 9) As Integer
  lDwleng As Long
  nDwsp(0 To 9) As Integer
  nReserve(0 To 9) As Integer
End Type

' cnc_rdlpwrdty:read power controll duty data
' cnc_wrlpwrdty:write power controll duty data
Type IODBLPWDT
  nSlct As Integer
  nDty_const As Integer
  nDty_min As Integer
  nReserve(0 To 5) As Integer
End Type

' cnc_rdlpwrdat:read laser power data
Type ODBLOPDT
  nSlct As Integer
  nPwr_mon As Integer
  nPwr_ofs As Integer
  nPwr_act As Integer
  lFeed_act As Long
  nReserve(0 To 3) As Integer
End Type

' cnc_rdlagslt:read laser assist gas selection
' cnc_wrlagslt:write laser assist gas selection
Type IODBLAGSL
  nSlct As Integer
  nAg_slt As Integer
  nAgflow_slt As Integer
  nReserve(0 To 5) As Integer
End Type

' cnc_rdlagst:read laser assist gas flow
' cnc_wrlagst:write laser assist gas flow
Type IODBLAGSTdata
  nSlct As Integer
  nPre_time As Integer
  nPre_press As Integer
  nProc_press As Integer
  nEnd_time As Integer
  nEnd_press As Integer
  nReserve(0 To 2) As Integer
End Type

Type IODBLAGST
  nData(0 To 2) As IODBLAGSTdata
End Type

' cnc_rdledgprc:read laser power for edge processing
' cnc_wrledgprc:write laser power for edge processing
Type IODBLEGPR
  nSlct As Integer
  nPower As Integer
  nFreq As Integer
  nDuty As Integer
  nReserve(0 To 4) As Integer
End Type

' cnc_rdlprcprc:read laser power for piercing
' cnc_wrlprcprc:write laser power for piercing
Type IODBLPCPR
  nSlct As Integer
  nPower As Integer
  nFreq As Integer
  nDuty As Integer
  lTime As Long
  nReserve(0 To 3) As Integer
End Type

' cnc_rdlcmddat:read laser command data
Type ODBLCMDT
  nSlct As Integer
  lFeed As Long
  nPower As Integer
  nFreq As Integer
  nDuty As Integer
  nG_kind As Integer
  nG_ready_t As Integer
  nG_press As Integer
  nError As Integer
  lDsplc As Long
  nReserve(0 To 6) As Integer
End Type

' cnc_rdlactnum:read active number
Type ODBLACTN
  nSlct As Integer
  nAct_proc As Integer
  nAct_pirce As Integer
  nAct_slop As Integer
  nReserve(0 To 4) As Integer
End Type

' cnc_rdlcmmt:read laser comment
Type ODBLCMMT
  sComment As String * 25
End Type

' cnc_rdpwofsthis:read power correction factor history data
Type ODBPWOFST
  lPwratio As Long
  lRfvolt As Long
  nYear As Integer
  nMonth As Integer
  nDay As Integer
  nHour As Integer
  nMinute As Integer
  nSecond As Integer
End Type

' cnc_rdmngtime:read management time
' cnc_wrmngtime:write management time
Type IODBMNGTIME
  lLife As Long
  lTotal As Long
End Type

' cnc_rddischarge:read data related to electrical discharge at power correction ends
Type ODBDISCHRG
  nAps As Integer
  nYear As Integer
  nMonth As Integer
  nDay As Integer
  nHour As Integer
  nMinute As Integer
  nSecond As Integer
  nHpc As Integer
  nHfq As Integer
  nHdt As Integer
  nHpa As Integer
  lHce As Long
  lRfi(0 To 7) As Long
  lRfv(0 To 7) As Long
  lDci(0 To 7) As Long
  lDcv(0 To 7) As Long
  lDcw(0 To 7) As Long
End Type

' cnc_rddischrgalm:read alarm history data related to electrical discharg
Type ODBDISCHRGALM
  nYear As Integer
  nMonth As Integer
  nDay As Integer
  nHour As Integer
  nMinute As Integer
  nSecond As Integer
  lAlmnum As Long
  lPsec As Long
  nHpc As Integer
  nHfq As Integer
  nHdt As Integer
  nHpa As Integer
  lHce As Long
  nAsq As Integer
  nPsu As Integer
  nAps As Integer
  nDummy As Integer
  lRfi(0 To 7) As Long
  lRfv(0 To 7) As Long
  lDci(0 To 7) As Long
  lDcv(0 To 7) As Long
  lDcw(0 To 7) As Long
  nAlmcd(0 To 7) As Integer
End Type

' cnc_gettimer:get date and time from cnc
' cnc_settimer:set date and time for cnc
Type IODBTIMER1
  nType As Integer
  nDummy As Integer
  nYear As Integer
  nMonth As Integer
  nDate As Integer
End Type

Type IODBTIMER2
  nType As Integer
  nDummy As Integer
  nHour As Integer
  nMinute As Integer
  nSecond As Integer
End Type

' cnc_rdtimer:read timer data from cnc
' cnc_wrtimer:write timer data for cnc
Type IODBTIME
  nMinute As Long
  nMsec As Long
End Type

' cnc_rdtlctldata: read tool controll data
' cnc_wrtlctldata: write tool controll data
Type IODBTLCTL
  nSlct As Integer
  nUsed_tool As Integer
  nTurret_indx As Integer
  lZero_tl_no As Long
  lT_axis_move As Long
  lTotal_punch(0 To 1) As Long
  nReserve(0 To 10) As Integer
End Type

' cnc_rdtooldata: read tool data
' cnc_wrtooldata: read tool data
Type IODBTLDT
  nSlct As Integer
  lTool_no As Long
  lX_axis_ofs As Long
  lY_axis_ofs As Long
  lTurret_pos As Long
  lChg_tl_no As Long
  lPunch_count As Long
  lTool_life As Long
  lM_tl_radius As Long
  lM_tl_angle As Long
  sTl_shape As Byte
  lTl_size_i As Long
  lTl_size_j As Long
  lTl_angle As Long
  lReserve(0 To 2) As Long
End Type

' cnc_rdmultitldt: read multi tool data
' cnc_wrmultitldt: write multi tool data
Type IODBMLTTL
  nSlct As Integer
  nM_tl_no As Integer
  lM_tl_radius As Long
  lM_tl_angle As Long
  lX_axis_ofs As Long
  lY_axis_ofs As Long
  sTl_shape As Byte
  lTl_size_i As Long
  lTl_size_j As Long
  lTl_angle As Long
  lReserve(0 To 6) As Long
End Type

' cnc_rdmtapdata: read multi tap data
' cnc_wrmtapdata: write multi tap data
Type IODBMTAP
  nSlct As Integer
  lTool_no As Long
  lX_axis_ofs As Long
  lY_axis_ofs As Long
  lPunch_count As Long
  lTool_life As Long
  lReserve(0 To 10) As Long
End Type

' cnc_rdtoolinfo: read tool information
Type ODBPTLINF
  nTld_max As Integer
  nMlt_max As Integer
  nReserve As Integer
  nTld_size(0 To 15) As Integer
  nMlt_size(0 To 15) As Integer
  nReserves(0 To 15) As Integer
End Type

' cnc_rdsafetyzone: read safetyzone data
' cnc_wrsafetyzone: write safetyzone data
Type IODBSAFE
  nSlct As Integer
  lData(0 To 2) As Long
End Type

' cnc_rdtoolzone: read toolzone data
' cnc_wrtoolzone: write toolzone data
Type IODBTLZN
  nSlct As Integer
  lData(0 To 1) As Long
End Type

' cnc_rdacttlzone: read active toolzone data
Type ODBACTTLZN
  nAct_no As Integer
  lData(0 To 1) As Long
End Type

' cnc_rdbrstrinfo:read block restart information
Type ODBBRS
  lDest(0 To MAX_AXIS - 1) As Long
  lDist(0 To MAX_AXIS - 1) As Long
End Type

' cnc_rdradofs:read tool radius offset for position data
Type ODBROFS
  nMode As Integer
  nPln_axes(0 To 1) As Integer
  lOfsvct(0 To 1) As Long
End Type

' cnc_rdlenofs:read tool length offset for position data
Type ODBLOFS
  nMode As Integer
  lOfsvct(0 To MAX_AXIS - 1) As Long
End Type

' cnc_rdfixcycle:read fixed cycle for position data
Type ODBFIX
  nMode As Integer
  nPln_axes(0 To 1) As Integer
  nDrl_axes As Integer
  lI_pos As Long
  lR_pos As Long
  lZ_pos As Long
  lCmd_cnt As Long
  lAct_cnt As Long
  lCut As Long
  lShift(0 To 1) As Long
End Type

' cnc_rdcdrotate:read coordinate rotate for position data
Type ODBROT
  nMode As Integer
  nPln_axes(0 To 1) As Integer
  lCenter(0 To 1) As Long
  lAngle As Long
End Type

' cnc_rd3dcdcnv:read 3D coordinate convert for position data
Type ODB3DCD
  nMode As Integer
  nDno As Integer
  nCd_axes(0 To 2) As Integer
  lCenter(0 To 1, 0 To 2) As Long
  lDirect(0 To 1, 0 To 2) As Long
  lAngle(0 To 1) As Long
End Type

' cnc_rdmirimage:read programable mirror image for position data
Type ODBMIR
  nMode As Integer
  lMir_flag As Long
  lMir_pos(0 To MAX_AXIS - 1) As Long
End Type

' cnc_rdscaling:read scaling data for position data
Type ODBSCL
  nMode As Integer
  lCenter(0 To MAX_AXIS - 1) As Long
  lMagnif(0 To MAX_AXIS - 1) As Long
End Type

' cnc_rd3dtofs:read 3D tool offset for position data
Type ODB3DTO
  nMode As Integer
  nOfs_axes(0 To 2) As Integer
  lOfsvct(0 To 2) As Long
End Type

' cnc_rdposofs:read tool position offset for position data
Type ODBPOFS
  nMode As Integer
  lOfsvct(0 To MAX_AXIS - 1) As Long
End Type

' cnc_rdhpccset:read hpcc setting data
' cnc_wrhpccset:write hpcc setting data
Type IODBHPST
  nSlct As Integer
  nHpcc As Integer
  nMulti As Integer
  nOvr1 As Integer
  nIgn_f As Integer
  nFoward As Integer
  lMax_f As Long
  nOvr2 As Integer
  nOvr3 As Integer
  nOvr4 As Integer
  lReserve(0 To 6) As Long
End Type

' cnc_rdhpcctupr:read hpcc tuning data ( parameter input )
' cnc_wrhpcctupr:write hpcc tuning data ( parameter input )
Type IODBHPPRdata
  nSlct As Integer
  nDiff As Integer
  nFine As Integer
  nAcc_lv As Integer
  lMax_f As Long
  nBipl As Integer
  nAipl As Integer
  lCorner As Long
  nClamp As Integer
  lRadius As Long
  lMax_cf As Long
  lMin_cf As Long
  lFoward As Long
  lReserve(0 To 4) As Long
End Type

Type IODBHPPR
  nTune(0 To 2) As IODBHPPRdata
End Type

' cnc_rdhpcctuac:read hpcc tuning data ( acc input )
' cnc_wrhpcctuac:write hpcc tuning data ( acc input )
Type IODBHPACdata
  nSlct As Integer
  nDiff As Integer
  nFine As Integer
  nAcc_lv As Integer
  lBipl As Long
  nAipl As Integer
  lCorner As Long
  lClamp As Long
  lC_acc As Long
  lFoward As Long
  lReserve(0 To 7) As Long
End Type

Type IODBHPAC
  nTune(0 To 2) As IODBHPACdata
End Type

'  cnc_rd3dtooltip:read tip of tool for 3D handle
'  cnc_rd3dmovrlap:read move overrlap of tool for 3D handle
Type ODB3DHDL
  nAxes(0 To 4) As Integer
  lData(0 To 4) As Long
End Type

'  cnc_rd3dpulse:read pulse for 3D handle
Type ODB3DPLS
  lRight_angle_x As Long
  lRight_angle_y As Long
  lTool_axis As Long
  lTool_tip_a_b As Long
  lTool_tip_c As Long
End Type

' cnc_rdaxisname: read axis name
Type ODBAXISNAME
  sName As String * 1           ' axis name
  sSuff As String * 1           ' suffix
End Type

' cnc_rdspdlname: read spindle name
Type ODBSPDLNAME
  sName As String * 1           ' axis name
  sSuff1 As String * 1          ' suffix
  sSuff2 As String * 1          ' suffix
  sSuff3 As String * 1          ' suffix
End Type

' cnc_exaxisname:read extended axis name
Type ODBEXAXISNAME
  sAxname(0 To MAX_AXIS - 1) As String * MAX_AXISNAME
End Type

' cnc_rdpm_mcnitem:read machine specific maintenance item
' cnc_wrpm_mcnitem:write machine specific maintenance item
Type ODBMCNITEM
  sName(9) As String * 40
End Type

' cnc_rdpm_item:read maintenance item status
' cnc_wrpm_item:write maintenance item status
Type IODBPMAINTE
 sName As String * 62
 lType As Long
 lTotal As Long
 lRemain As Long
 lStat As Long
End Type

' cnc_sysinfo_ex:read CNC system path information
Type ODBSYSEXdata
  nSystem As Integer      ' M/T/TT <ascii char>
  nGroup As Integer       '
  nAttrib As Integer      '
  nCtrl_axis As Integer   '
  nCtrl_srvo As Integer   '
  nCtrl_spdl As Integer   '
  nMchn_no As Integer     '
  reserved As Integer
End Type

Type ODBSYSEX
  nMax_axis As Integer        ' maximum axis number
  nMax_spdl As Integer        '
  nMax_path As Integer        '
  nMax_mchn As Integer        '
  nCtrl_axis As Integer       '
  nCtrl_srvo As Integer       '
  nCtrl_spdl As Integer       '
  nCtrl_path As Integer       '
  nCtrl_mchn As Integer       '
  reserved(0 To 2) As Integer
  Path(0 To MAX_CNCPATH - 1) As ODBSYSEXdata
End Type

Type UNSOLICMSG_TYPE_PRM_PMC
    lPath       As Integer
    lAddr       As Integer
    lNo         As Long
    lSize       As Long
End Type

Type UNSOLICMSG_TYPE_PRM_MACRO
    lPath               As Integer
    sDummy2(0 To 1)     As Byte
    lNo                 As Long
    lNum                As Long
End Type

Type UNSOLICMSG_TYPE_PRM_PRM_PMC
    Pmc As UNSOLICMSG_TYPE_PRM_PMC
End Type

Type UNSOLICMSG_TYPE_PRM_PRM_MACRO
    Macro As UNSOLICMSG_TYPE_PRM_MACRO
End Type

Type UNSOLICMSG_TYPE_PRM1
    nType           As Integer
    sDummy1(0 To 1) As Byte
    prm             As UNSOLICMSG_TYPE_PRM_PRM_PMC
End Type

Type UNSOLICMSG_TYPE_PRM2
    nType           As Integer
    sDummy1(0 To 1) As Byte
    prm             As UNSOLICMSG_TYPE_PRM_PRM_MACRO
End Type


'-----
' CNC
'-----

' cnc_srcsrdidinfo
' cnc_srcswridinfo
Type IODBIDINF
  lId_no As Long
  nDrv_no As Integer
  nAcc_element As Integer
  nErr_general As Integer
  nErr_id_no As Integer
  nErr_id_name As Integer
  nErr_attr As Integer
  nErr_unit As Integer
  nErr_min_val As Integer
  nErr_max_val As Integer
  nId_name_len As Integer
  nId_name_max As Integer
  sId_name As String * 60
  lAttr As Long
  nUnit_len As Integer
  nUnit_max As Integer
  sUnit As String * 12
  lMin_val As Long
  lMax_val As Long
End Type

' cnc_srcsrdexstat
Type ODBSRCSST
  nAcc_element As Integer
  nErr_general As Integer
  nErr_id_no As Integer
  nErr_attr As Integer
  nErr_op_data As Integer
End Type

' cnc_srcsrdlayout
Type ODBSRCSLYT
  nSpndl(0 To 3) As Integer
  nServo(0 To MAX_AXIS - 1) As Integer
  sAxis_name(0 To MAX_AXIS - 1) As String * 1
End Type


'-----
' CNC
'-----
' cnc_sfbreadsmpl
' cnc_sdtreadsmpl
Type ODBSD
  nChadata As Integer
  lCount As Long
End Type

' cnc_sfbsetchnl
Type IDBSFBCHAN
  sChno As Byte
  sAxis As Byte
  nShift As Integer
End Type

' cnc_sdtsetchnl
Type IDBSDTCHAN
  nType As Integer
  sChno As Byte
  sAxis As Byte
  nShift As Integer
End Type


'------------------------
' CNC : FS18-LN function
'------------------------
Type ODBCAXIS
  nDummy As Integer             ' dummy
  nType As Integer              ' axis number
  sData(0 To MAX_AXIS - 1) As Byte ' data value
End Type


#If PMD Then        ' only Power Mate D/H
'------------------------------
' MAXIS: Axis Movement Control
'------------------------------

' cnc_opdi:signal operation command
Type ODBOPDI1
  nAxis As Integer      ' axis number
  cData As Byte         ' signal data
End Type

Type ODBOPDI2
  nAxis As Integer      ' axis number
  nData As Integer      ' signal data
End Type

Type ODBOPDI3
  nAxis As Integer      ' axis number
  lData As Long         ' signal data
End Type

' cnc_abspoint:absolute movement
' cnc_incpoint:incremental movement
' cnc_dwell:dwell
' cnc_coordre:coordinate establihment
Type ODBPOS
  nData As Integer      ' axis number
  lData As Long         ' data value
End Type

' cnc_refpoint:reference point return
' cnc_abspoint:absolute movement
' cnc_incpoint:incremental movement
' cnc_dwell:dwell
' cnc_coordre:coordinate establihment
' cnc_exebufstat:reading of the executive buffer condition
Type ODBEXEC
  nDummy As Integer
  sData(0 To 1) As String * 8   ' the infomation of the executive condition of
End Type

' cnc_finstate:Reading of the execution completion condition
' cnc_setfin:Release of the reading mode of the execution completion condition
Type ODBFIN
  nDummy As Integer
  sData As String * 8   ' the infomation of the complete notice flag
End Type                ' condition of each PATH
#End If


'-----
' PMC
'-----

' pmc_rdpmcrng:read PMC data(area specified)
' pmc_wrpmcrng:write PMC data(area specified)
Type IODBPMC1
  nType_a As Integer            ' PMC address type
  nType_d As Integer            ' PMC data type
  nDatano_s As Integer          ' start PMC address
  nDatano_e As Integer          ' end PMC address
  sCdata(0 To 4) As Byte
End Type                        ' In case that the number of data is 5

Type IODBPMC2
  nType_a As Integer            ' PMC address type
  nType_d As Integer            ' PMC data type
  nDatano_s As Integer          ' start PMC address
  nDatano_e As Integer          ' end PMC address
  nIdata(0 To 4) As Integer
End Type                        ' In case that the number of data is 5

Type IODBPMC3
  nType_a As Integer            ' PMC address type
  nType_d As Integer            ' PMC data type
  nDatano_s As Integer          ' start PMC address
  nDatano_e As Integer          ' end PMC address
  lLdata(0 To 4) As Long
End Type                        ' In case that the number of data is 5

' pmc_rdpmcinfo:read informations of PMC data
Type ODBPMCINFdata
  sPmc_adr As Byte
  sAdr_attr As Byte
  nTop_num As Integer
  nLast_num As Integer
End Type

Type ODBPMCINF
  nDatano As Integer
  nInfo(0 To 63) As ODBPMCINFdata
End Type

' pmc_rdcntldata:read PMC parameter data table control data
' pmc_wrcntldata:write PMC parameter data table control data
Type IODBPMCCNTLdata
  sTbl_prm As Byte
  sData_type As Byte
  nData_size As Integer
  nData_dsp As Integer
  nDummy As Integer
End Type

Type IODBPMCCNTL
  nDatano_s As Integer
  nDummy As Integer
  nDatano_e As Integer
  nInfo(0 To 99) As IODBPMCCNTLdata
End Type

' pmc_rdalmmsg:read PMC alarm message
Type ODBPMCALM
  sAlmmsg As String * 128
End Type

' pmc_getdtailerr:get detail error for pmc
Type ODBPMCERR
  nErr_no As Integer
  nErr_dtno As Integer
End Type

' pmc_rdpmctitle:read pmc title data
Type ODBPMCTITLE
  sMtb As String * 48
  sMachine As String * 48
  sType As String * 48
  sPrgno As String * 8
  sPrgvers As String * 4
  sPrgdraw As String * 48
  sDate As String * 32
  sDesign As String * 48
  sWritten As String * 48
  sRemarks As String * 48
End Type

' pmc_read_seq_program_and_memory_type:Reads sequence program and momory type
Type ODBPMCLADMEMTYPE
  lSystemType As Long
  lSystemAttribute As Long
  lTargetType As Long
  lTargetAttribute As Long
  sSystemTypeStr As String * 32
  sTargetTypeStr As String * 32
End Type


'--------------------------
' PMC : PROFIBUS function
'--------------------------

' read PROFIBUS configration data
Type ODBPRFCNF
  sMaster_ser As String * 5
  sMaster_ver As String * 3
  sSlave_ser As String * 5
  sSlave_ver As String * 3
  sCntl_ser As String * 5
  sCntl_ver As String * 3
End Type

' read bus parameter for master function
' write bus parameter for master function
Type IODBBUSPRM
  sFdl_add As Byte
  sBaudrate As Byte
  nTsl As Integer
  nMin_tsdr As Integer
  nMax_tsdr As Integer
  sTqui As Byte
  sTset As Byte
  lTtr As Long
  sGap As Byte
  sHsa As Byte
  sMax_retry As Byte
  sBp_flag As Byte
  nMin_slv_int As Integer
  nPoll_tout As Integer
  nData_cntl As Integer
  sReserve1(0 To 5) As Byte
  sCls2_name As String * 32
  nUser_dlen As Integer
  sUser_data(0 To 61) As Byte
  sReserve2(0 To 95) As Byte
End Type

' read slave parameter for master function
' write slave parameter for master function
Type IODBSLVPRM
  nDis_enb As Integer
  nIdent_no As Integer
  sSlv_flag As Byte
  sSlv_type As Byte
  sReserve1(0 To 11) As Byte
  sSlv_stat As Byte
  sWd_fact1 As Byte
  sWd_fact2 As Byte
  sMin_tsdr As Byte
  sReserve2 As Byte
  sGrp_ident As Byte
  nUser_plen As Integer
  sUser_pdata(0 To 31) As Byte
  nCnfg_dlen As Integer
  sCnfg_data(0 To 125) As Byte
  nSlv_ulen As Integer
  sSlv_udata(0 To 29) As Byte
  sReserve3(0 To 7) As Byte
End Type

Type IODBSLVPRM2
  nDis_enb As Integer
  nIdent_no As Integer
  sSlv_flag As Byte
  sSlv_type As Byte
  sReserve1(0 To 11) As Byte
  sSlv_stat As Byte
  sWd_fact1 As Byte
  sWd_fact2 As Byte
  sMin_tsdr As Byte
  sReserve2 As Byte
  sGrp_ident As Byte
  nUser_plen As Integer
  sUser_pdata(0 To 205) As Byte
  nCnfg_dlen As Integer
  sCnfg_data(0 To 125) As Byte
  nSlv_ulen As Integer
  sSlv_udata(0 To 29) As Byte
  sReserve3(0 To 7) As Byte
End Type

' read allocation address for master function
' set allocation address for master function
Type IODBPRFADR
  sDi_size As Byte
  sDi_type As Byte
  nDi_addr As Integer
  nReserve1 As Integer
  sDo_size As Byte
  sDo_type As Byte
  nDo_addr As Integer
  nReserve2 As Integer
  sDgn_size As Byte
  sDgn_type As Byte
  nDgn_addr As Integer
End Type

' read allocation address for slave function
' set allocation address for slave function
Type IODBSLVADR
  sSlave_no As Byte
  sDi_size As Byte
  sDi_type As Byte
  nDi_addr As Integer
  sDo_size As Byte
  sDo_type As Byte
  nDo_addr As Integer
  sReserve(0 To 6) As Byte
End Type

' read status for slave function
Type ODBSLVST
  sCnfg_stat As Byte
  sPrm_stat As Byte
  sWdg_stat As Byte
  sLive_stat As Byte
  nIdent_no As Integer
End Type

' pmc_prfrdslvid:Reads slave index data of master function
' pmc_prfwrslvid:Writes slave index data of master function
Type IODBSLVID
  nDis_enb As Integer
  nSlave_no As Integer
  nNsl As Integer
  cDgn_size As Byte
  cDgn_type As Byte
  cDgn_addr As Integer
End Type

' pmc_prfrdslvprm2:Reads slave parameter of master function(2)
' pmc_prfwrslvprm2:Writes slave parameter of master function(2)
Type IODBSLVPRM3
  nIdent_no As Integer
  cSlv_flag As Byte
  cSlv_type As Byte
  reserve1(0 To 11) As Byte
  cSlv_stat As Byte
  cWd_fact1 As Byte
  cWd_fact2 As Byte
  cMin_tsdr As Byte
  reserve2 As Byte
  cGrp_ident As Byte
  nUser_plen As Integer
  cUser_pdata(0 To 205) As Byte
  nSlv_ulen As Integer
  cSlv_udata(0 To 29) As Byte
End Type

' pmc_prfrddido:Reads DI/DO parameter of master function
' pmc_prfwrdido:Writes DI/DO parameter of master function
Type IODBDIDO
  nSlave_no As Integer
  nSlot_no As Integer
  cDi_size As Byte
  cDi_type As Byte
  nDi_addr As Integer
  cDo_size As Byte
  cDo_type As Byte
  nDo_addr As Integer
  nShift As Integer
  cModule_dlen As Byte
  cModule_data(0 To 127) As Byte
End Type

' pmc_prfrdindiadr:Reads indication address of master function
' pmc_prfwrindiadr:Writes indication address of master function
Type IODBINDEADR
  dummy As Byte
  cIndi_type As Byte
  nIndi_addr As Integer
End Type


'-----------------------------------
' CB : CUSTOMER'S BOARD function
'-----------------------------------

Type ODBTRANSINFO
  lMas_buff_size As Long
  nTrans_start_reqflag As Integer
  nTrans_end_reqflag As Integer
  nTrans_start_respflag As Integer
  nTrans_end_respflag As Integer
  lAll_transfer_size As Long
  nData_id As Integer
  nReserve As Integer
  lData_write_pt As Long
  lData_read_pt As Long
  lAccumulation_counter As Long
  nForwarding_status As Integer
End Type


'-----------------------------------------------
' DS : Data server & Ethernet board function
'-----------------------------------------------

' etb_rdparam : read the parameter of the Ethernet board
' etb_wrparam : write the parameter of the Ethernet board
Type TCPPRM
  sOwnIPAddress As String * 16
  sSubNetMask As String * 16
  sRouterIPAddress As String * 16
End Type

Type HOSTPRM
  nDataServerPort As Integer
  sDataServerIPAddress As String * 16
  sDataServerUserName As String * 32
  sDataServerPassword As String * 32
  sDataServerLoginDirectory As String * 128
End Type

Type FTPPRM
  sFTPServerUserName As String * 32
  sFTPServerPassword As String * 32
  sFTPServerLoginDirectory As String * 128
End Type

Type ETBPRM
  sOwnMACAddress As String * 13
  nMaximumChannel As Integer
  nHDDExistence As Integer
  nNumberOfScreens As Integer
End Type

Type IODBETP1
  nParameterType As Integer
  uTcp As TCPPRM
End Type

Type IODBETP2
  nParameterType As Integer
  uHost As HOSTPRM
End Type

Type IODBETP3
  nParameterType As Integer
  uFtp As FTPPRM
End Type

Type IODBETP4
  nParameterType As Integer
  uEtb As ETBPRM
End Type

' etb_rderrmsg : read the error message of the Ethernet board
Type ODBETMSG
  sTitle As String * 33
  sMessage(0 To 9) As String * 39
End Type

' ds_rdhddinfo : read information of the Data Server's HDD
Type ODBHDDINF
  lFile_num As Long
  lRemainder_l As Long
  lRemainder_h As Long
  sCurrent_dir As String * 32
End Type

' ds_rdhdddir : read the file list of the Data Server's HDD
Type ODBHDDDIR
  sFile_name As String * 64
  sComment As String * 80
  nAttribute As Integer
  nReserved As Integer
  lSize As Long
  sDate As String * 16
End Type

' ds_rdhostdir : read the file list of the host
Type ODBHOSTDIR
  sHost_file As String * 128
End Type

' ds_rdmntinfo : read maintenance information
Type DSMNTINFO
  nEmpty_cnt As Integer
  lTotal_size As Long
  nReadPtr As Integer
  nWritePtr As Integer
End Type

'----------------------------
' NET : Ethernet function
'----------------------------

Type COMMON_PRM
    sOwnMACAddress       As String * 13
    sOwnIPAddress        As String * 40
    sSubNetMask          As String * 16
    sRouterIPAddress     As String * 40
    sDnsServer1IpAddress As String * 40
    sDnsServer2IpAddress As String * 40
    sOwnHostName         As String * 32
    sOwnDomain           As String * 63
End Type

Type FOCAS2_PRM
    lTcpPort        As Long
    lUdpPort        As Long
    lTimeInterval   As Long
End Type

Type FTP_CLIENT_PRM
    sHostName           As String * 64
    lControlPort        As Long
    lDummy              As Long
    sUserName           As String * 32
    sPassword           As String * 32
    sLoginDirectory     As String * 128
End Type

Type FTP_SERVER_PRM
    sUserName       As String * 32
    sPassword       As String * 32
    sLoginDirectory As String * 128
End Type

Type FTPTRANS_PRM
    opposite(0 To 2) As FTP_CLIENT_PRM
End Type

Type DTSVR_PRM
    opposite(0 To 2) As FTP_CLIENT_PRM
    own              As FTP_SERVER_PRM
End Type

Type RMTDIAG_CLIENT_PRM
    sHostName       As String * 64
    lPort           As Long
    sInquiryName    As String * 64
End Type

Type RMTDIAG_PRM
    sMtbInformation     As String * 17
    sMachineInformation As String * 17
    opposite(0 To 2)    As RMTDIAG_CLIENT_PRM
End Type

Type FACTOLINK_CLIENT_PRM
    sHostName   As String * 64
    lPort       As Long
End Type

Type FACTO_PRM
    opposite(0 To 2) As FACTOLINK_CLIENT_PRM
End Type

Type PING_PRM
    sHostName As String * 64
    nCount    As Integer
End Type

Type MAINTAIN_PRM
    opposite  As PING_PRM
End Type

Type NETSRV_PRM
    sHostName        As String * 64
    lPort            As Long
    lTimeInterval    As Long
    lUdpPeriod       As Long
    sMachineNumber   As String * 25
    sDummy1(0 To 6)  As Byte
    sAcceptanceReply As String * 25
    sDummy2(0 To 6)  As Byte
    sErrorReply      As String * 25
    sDummy3(0 To 6)  As Byte
End Type

Type UNSOLICMSG_PRM11
    sHostName            As String * 64
    lPort                As Long
    nRetryCount          As Integer
    nTimeout             As Integer
    nAliveTime           As Integer
    sDummy1(0 To 7)      As Byte
    Control              As UNSOLICMSG_TYPE_PRM1
    nTransmissionNumber  As Integer
    sDummy2(0 To 13)     As Byte
    Transmission(0 To 2) As UNSOLICMSG_TYPE_PRM1
End Type

Type UNSOLICMSG_PRM12
    sHostName            As String * 64
    lPort                As Long
    nRetryCount          As Integer
    nTimeout             As Integer
    nAliveTime           As Integer
    sDummy1(0 To 7)      As Byte
    Control              As UNSOLICMSG_TYPE_PRM1
    nTransmissionNumber  As Integer
    sDummy2(0 To 13)     As Byte
    Transmission(0 To 2) As UNSOLICMSG_TYPE_PRM2
End Type

Type UNSOLICMSG_PRM21
    sHostName            As String * 64
    lPort                As Long
    nRetryCount          As Integer
    nTimeout             As Integer
    nAliveTime           As Integer
    sDummy1(0 To 7)      As Byte
    Control              As UNSOLICMSG_TYPE_PRM2
    nTransmissionNumber  As Integer
    sDummy2(0 To 13)     As Byte
    Transmission(0 To 2) As UNSOLICMSG_TYPE_PRM1
End Type

Type UNSOLICMSG_PRM22
    sHostName            As String * 64
    lPort                As Long
    nRetryCount          As Integer
    nTimeout             As Integer
    nAliveTime           As Integer
    sDummy1(0 To 7)      As Byte
    Control              As UNSOLICMSG_TYPE_PRM2
    nTransmissionNumber  As Integer
    sDummy2(0 To 13)     As Byte
    Transmission(0 To 2) As UNSOLICMSG_TYPE_PRM2
End Type

Type COMMON_PRM_FLG
    sOwnIPAddress        As Byte
    sSubNetMask          As Byte
    sRouterIPAddress     As Byte
    sDnsServer1IpAddress As Byte
    sDnsServer2IpAddress As Byte
    sOwnHostName         As Byte
    sOwnDomain           As Byte
End Type

Type FOCAS2_PRM_FLG
    sTcpPort      As Byte
    sUdpPort      As Byte
    sTimeInterval As Byte
End Type

Type FTP_CLIENT_PRM_FLG
    sHostName       As Byte
    sControlPort    As Byte
    sDummy          As Byte
    sUserName       As Byte
    sPassword       As Byte
    sLoginDirectory As Byte
End Type

Type FTP_SERVER_PRM_FLG
    sUserName       As Byte
    sPassword       As Byte
    sLoginDirectory As Byte
End Type

Type FTPTRANS_PRM_FLG
    opposite(0 To 2) As FTP_CLIENT_PRM_FLG
End Type

Type DTSVR_PRM_FLG
    opposite(0 To 2) As FTP_CLIENT_PRM_FLG
    own              As FTP_SERVER_PRM_FLG
End Type

Type RMTDIAG_CLIENT_PRM_FLG
    sHostName     As Byte
    sPort         As Byte
    sInquiryName  As Byte
End Type

Type RMTDIAG_PRM_FLG
    sMtbInformation     As Byte
    sMachineInformation As Byte
    opposite(0 To 2)    As RMTDIAG_CLIENT_PRM_FLG
End Type

Type FACTO_CLIENT_PRM_FLG
    sHostName As Byte
    sPort     As Byte
End Type

Type FACTO_PRM_FLG
    opposite(0 To 2) As FACTO_CLIENT_PRM_FLG
End Type

Type PING_PRM_FLG
    sHostName As Byte
    sCount    As Byte
End Type

Type MAINTAIN_PRM_FLG
    opposite As PING_PRM_FLG
End Type

Type NETSRV_PRM_FLG
    sHostName        As Byte
    sPort            As Byte
    sTimeInterval    As Byte
    sUdpPeriod       As Byte
    sMachineNumber   As Byte
    sAcceptanceReply As Byte
    sErrorReply      As Byte
End Type

Type UNSOLICMSG_TYPE_PRM_PMC_FLG
    sPathKindAddress As Byte
    sSize            As Byte
End Type

Type UNSOLICMSG_TYPE_PRM_MACRO_FLG
    sPathNo As Byte
    sNumber As Byte
End Type


Type UNSOLICMSG_TYPE_PRM_PRM_PMC_FLG
    Pmc As UNSOLICMSG_TYPE_PRM_PMC_FLG
End Type

Type UNSOLICMSG_TYPE_PRM_PRM_MACRO_FLG
    Macro As UNSOLICMSG_TYPE_PRM_MACRO_FLG
End Type

Type UNSOLICMSG_TYPE_PRM_FLG1
    sType           As Byte
    sDummy1(0 To 2) As Byte
    prm             As UNSOLICMSG_TYPE_PRM_PRM_PMC_FLG
    sDummy2(0 To 1) As Byte
End Type

Type UNSOLICMSG_TYPE_PRM_FLG2
    sType           As Byte
    sDummy1(0 To 2) As Byte
    prm             As UNSOLICMSG_TYPE_PRM_PRM_MACRO_FLG
    sDummy2(0 To 1) As Byte
End Type

Type UNSOLICMSG_PRM_FLG11
    sHostName            As Byte
    sPort                As Byte
    sRetryCount          As Byte
    sTimeout             As Byte
    sAliveTime           As Byte
    sDummy1(0 To 2)      As Byte
    Control              As UNSOLICMSG_TYPE_PRM_FLG1
    sTransmissionNumber  As Byte
    sDummy2(0 To 2)      As Byte
    Transmission(0 To 2) As UNSOLICMSG_TYPE_PRM_FLG1
End Type
Type UNSOLICMSG_PRM_FLG12
    sHostName            As Byte
    sPort                As Byte
    sRetryCount          As Byte
    sTimeout             As Byte
    sAliveTime           As Byte
    sDummy1(0 To 2)      As Byte
    Control              As UNSOLICMSG_TYPE_PRM_FLG1
    sTransmissionNumber  As Byte
    sDummy2(0 To 2)      As Byte
    Transmission(0 To 2) As UNSOLICMSG_TYPE_PRM_FLG2
End Type

Type UNSOLICMSG_PRM_FLG21
    sHostName            As Byte
    sPort                As Byte
    sRetryCount          As Byte
    sTimeout             As Byte
    sAliveTime           As Byte
    sDummy1(0 To 2)      As Byte
    Control              As UNSOLICMSG_TYPE_PRM_FLG2
    sTransmissionNumber  As Byte
    sDummy2(0 To 2)      As Byte
    Transmission(0 To 2) As UNSOLICMSG_TYPE_PRM_FLG1
End Type
Type UNSOLICMSG_PRM_FLG22
    sHostName            As Byte
    sPort                As Byte
    sRetryCount          As Byte
    sTimeout             As Byte
    sAliveTime           As Byte
    sDummy1(0 To 2)      As Byte
    Control              As UNSOLICMSG_TYPE_PRM_FLG2
    sTransmissionNumber  As Byte
    sDummy2(0 To 2)      As Byte
    Transmission(0 To 2) As UNSOLICMSG_TYPE_PRM_FLG2
End Type

' eth_wrparam
Type IN_ETHPRMFLAG_COMMON
    common As COMMON_PRM_FLG
End Type
Type IN_ETHPRMFLAG_FOCAS2
    focas2 As FOCAS2_PRM_FLG
End Type
Type IN_ETHPRMFLAG_FTPTRANS
    ftpTrans As FTPTRANS_PRM_FLG
End Type
Type IN_ETHPRMFLAG_DTSVR
    dataServer As DTSVR_PRM_FLG
End Type
Type IN_ETHPRMFLAG_RMTDIAG
    remoteDiag As RMTDIAG_PRM_FLG
End Type
Type IN_ETHPRMFLAG_FACTO
    factolink As FACTO_PRM_FLG
End Type
Type IN_ETHPRMFLAG_MAINTAIN
    maintain As MAINTAIN_PRM_FLG
End Type
Type IN_ETHPRMFLAG_NETSRV
    netservice As NETSRV_PRM_FLG
End Type
Type IN_ETHPRMFLAG_UNSOLICMSG11
    unsolicmsg As UNSOLICMSG_PRM_FLG11
End Type
Type IN_ETHPRMFLAG_UNSOLICMSG12
    unsolicmsg As UNSOLICMSG_PRM_FLG12
End Type
Type IN_ETHPRMFLAG_UNSOLICMSG21
    unsolicmsg As UNSOLICMSG_PRM_FLG21
End Type
Type IN_ETHPRMFLAG_UNSOLICMSG22
    unsolicmsg As UNSOLICMSG_PRM_FLG22
End Type

Type IN_ETHPRMFLAG1
    flg As IN_ETHPRMFLAG_COMMON
End Type
Type IN_ETHPRMFLAG2
    flg As IN_ETHPRMFLAG_FOCAS2
End Type
Type IN_ETHPRMFLAG3
    flg As IN_ETHPRMFLAG_FTPTRANS
End Type
Type IN_ETHPRMFLAG4
    flg As IN_ETHPRMFLAG_DTSVR
End Type
Type IN_ETHPRMFLAG5
    flg As IN_ETHPRMFLAG_RMTDIAG
End Type
Type IN_ETHPRMFLAG6
    flg As IN_ETHPRMFLAG_FACTO
End Type
Type IN_ETHPRMFLAG7
    flg As IN_ETHPRMFLAG_MAINTAIN
End Type
Type IN_ETHPRMFLAG8
    flg As IN_ETHPRMFLAG_NETSRV
End Type
Type IN_ETHPRMFLAG911
    flg As IN_ETHPRMFLAG_UNSOLICMSG11
End Type
Type IN_ETHPRMFLAG912
    flg As IN_ETHPRMFLAG_UNSOLICMSG12
End Type
Type IN_ETHPRMFLAG921
    flg As IN_ETHPRMFLAG_UNSOLICMSG21
End Type
Type IN_ETHPRMFLAG922
    flg As IN_ETHPRMFLAG_UNSOLICMSG22
End Type


Type IN_OUT_ETHPRM_COMMON
    common              As COMMON_PRM
    sDummy(0 To 699)    As Byte    'It make the same size as size of largest structure. (MaxSize)984 - (COMMON_PRM Size)284 = 700byte
End Type
Type IN_OUT_ETHPRM_FOCAS2
    focas2              As FOCAS2_PRM
    sDummy(0 To 971)    As Byte    'It make the same size as size of largest structure. (MaxSize)984 - (FOCAS2_PRM Size)12 = 972byte
End Type
Type IN_OUT_ETHPRM_FTPTRANS
    ftpTrans            As FTPTRANS_PRM
    sDummy(0 To 191)    As Byte    'It make the same size as size of largest structure. (MaxSize)984 - (FTPTRANS_PRM Size)792 = 192byte
End Type
Type IN_OUT_ETHPRM_DTSVR
    dataServer          As DTSVR_PRM  'The size of this structure is 984byte.(This is MaxSize)
End Type
Type IN_OUT_ETHPRM_RMTDIAG
    remoteDiag          As RMTDIAG_PRM
    sDummy(0 To 553)    As Byte    'It make the same size as size of largest structure. (MaxSize)984 - (RMTDIAG_PRM Size)430 = 554byte
End Type
Type IN_OUT_ETHPRM_FACTO
    factolink           As FACTO_PRM
    sDummy(0 To 779)    As Byte    'It make the same size as size of largest structure. (MaxSize)984 - (FACTO_PRM Size)204 = 780byte
End Type
Type IN_OUT_ETHPRM_MAINTAIN
    maintain            As MAINTAIN_PRM
    sDummy(0 To 917)    As Byte    'It make the same size as size of largest structure. (Maxsize)984 - (MAINTAIN_PRM Size)66 = 918byte
End Type
Type IN_OUT_ETHPRM_NETSRV
    netservice          As NETSRV_PRM
    sDummy(0 To 811)    As Byte    'It make the same size as size of largest structure. (Maxsize)984 - (NETSRV_PRM Size)172 = 812byte
End Type
Type IN_OUT_ETHPRM_UNSOLICMSG11
    unsolicmsg          As UNSOLICMSG_PRM11
    sDummy(0 To 831)    As Byte    'It make the same size as size of largest structure. (Maxsize)984 - (UNSOLICMSG_PRM11 Size)152 = 832byte
End Type
Type IN_OUT_ETHPRM_UNSOLICMSG12
    unsolicmsg          As UNSOLICMSG_PRM12
    sDummy(0 To 831)    As Byte    'It make the same size as size of largest structure. (Maxsize)984 - (UNSOLICMSG_PRM12 Size)152 = 832byte
End Type
Type IN_OUT_ETHPRM_UNSOLICMSG21
    unsolicmsg          As UNSOLICMSG_PRM21
    sDummy(0 To 831)    As Byte    'It make the same size as size of largest structure. (Maxsize)984 - (UNSOLICMSG_PRM21 Size)152 = 832byte
End Type
Type IN_OUT_ETHPRM_UNSOLICMSG22
    unsolicmsg          As UNSOLICMSG_PRM22
    sDummy(0 To 831)    As Byte    'It make the same size as size of largest structure. (Maxsize)984 - (UNSOLICMSG_PRM22 Size)152 = 832byte
End Type

Type IN_ETHPRM1
    nReserve01 As Integer
    nReserve02 As Integer
    nReserve03 As Integer
    nReserve04 As Integer
    nReserve05 As Integer
    nReserve06 As Integer
    prm        As IN_OUT_ETHPRM_COMMON
End Type

Type IN_ETHPRM2
    nReserve01 As Integer
    nReserve02 As Integer
    nReserve03 As Integer
    nReserve04 As Integer
    nReserve05 As Integer
    nReserve06 As Integer
    prm        As IN_OUT_ETHPRM_FOCAS2
End Type

Type IN_ETHPRM3
    nReserve01 As Integer
    nReserve02 As Integer
    nReserve03 As Integer
    nReserve04 As Integer
    nReserve05 As Integer
    nReserve06 As Integer
    prm        As IN_OUT_ETHPRM_FTPTRANS
End Type

Type IN_ETHPRM4
    nReserve01 As Integer
    nReserve02 As Integer
    nReserve03 As Integer
    nReserve04 As Integer
    nReserve05 As Integer
    nReserve06 As Integer
    prm        As IN_OUT_ETHPRM_DTSVR
End Type

Type IN_ETHPRM5
    nReserve01 As Integer
    nReserve02 As Integer
    nReserve03 As Integer
    nReserve04 As Integer
    nReserve05 As Integer
    nReserve06 As Integer
    prm        As IN_OUT_ETHPRM_RMTDIAG
End Type

Type IN_ETHPRM6
    nReserve01 As Integer
    nReserve02 As Integer
    nReserve03 As Integer
    nReserve04 As Integer
    nReserve05 As Integer
    nReserve06 As Integer
    prm        As IN_OUT_ETHPRM_FACTO
End Type

Type IN_ETHPRM7
    nReserve01 As Integer
    nReserve02 As Integer
    nReserve03 As Integer
    nReserve04 As Integer
    nReserve05 As Integer
    nReserve06 As Integer
    prm        As IN_OUT_ETHPRM_MAINTAIN
End Type

Type IN_ETHPRM8
    nReserve01 As Integer
    nReserve02 As Integer
    nReserve03 As Integer
    nReserve04 As Integer
    nReserve05 As Integer
    nReserve06 As Integer
    prm        As IN_OUT_ETHPRM_NETSRV
End Type

Type IN_ETHPRM911
    nReserve01 As Integer
    nReserve02 As Integer
    nReserve03 As Integer
    nReserve04 As Integer
    nReserve05 As Integer
    nReserve06 As Integer
    prm        As IN_OUT_ETHPRM_UNSOLICMSG11
End Type
Type IN_ETHPRM912
    nReserve01 As Integer
    nReserve02 As Integer
    nReserve03 As Integer
    nReserve04 As Integer
    nReserve05 As Integer
    nReserve06 As Integer
    prm        As IN_OUT_ETHPRM_UNSOLICMSG12
End Type
Type IN_ETHPRM921
    nReserve01 As Integer
    nReserve02 As Integer
    nReserve03 As Integer
    nReserve04 As Integer
    nReserve05 As Integer
    nReserve06 As Integer
    prm        As IN_OUT_ETHPRM_UNSOLICMSG21
End Type
Type IN_ETHPRM922
    nReserve01 As Integer
    nReserve02 As Integer
    nReserve03 As Integer
    nReserve04 As Integer
    nReserve05 As Integer
    nReserve06 As Integer
    prm        As IN_OUT_ETHPRM_UNSOLICMSG22
End Type




' eth_rdparam
Type OUT_ETHPRM1
    nOption         As Integer
    nType           As Integer
    nDhcp           As Integer
    nValidDevice    As Integer
    nDtsvrChannel   As Integer
    nStorage        As Integer
    prm             As IN_OUT_ETHPRM_COMMON
End Type
Type OUT_ETHPRM2
    nOption         As Integer
    nType           As Integer
    nDhcp           As Integer
    nValidDevice    As Integer
    nDtsvrChannel   As Integer
    nStorage        As Integer
    prm             As IN_OUT_ETHPRM_FOCAS2
End Type
Type OUT_ETHPRM3
    nOption         As Integer
    nType           As Integer
    nDhcp           As Integer
    nValidDevice    As Integer
    nDtsvrChannel   As Integer
    nStorage        As Integer
    prm             As IN_OUT_ETHPRM_FTPTRANS
End Type
Type OUT_ETHPRM4
    nOption         As Integer
    nType           As Integer
    nDhcp           As Integer
    nValidDevice    As Integer
    nDtsvrChannel   As Integer
    nStorage        As Integer
    prm             As IN_OUT_ETHPRM_DTSVR
End Type
Type OUT_ETHPRM5
    nOption         As Integer
    nType           As Integer
    nDhcp           As Integer
    nValidDevice    As Integer
    nDtsvrChannel   As Integer
    nStorage        As Integer
    prm             As IN_OUT_ETHPRM_RMTDIAG
End Type
Type OUT_ETHPRM6
    nOption         As Integer
    nType           As Integer
    nDhcp           As Integer
    nValidDevice    As Integer
    nDtsvrChannel   As Integer
    nStorage        As Integer
    prm             As IN_OUT_ETHPRM_FACTO
End Type
Type OUT_ETHPRM7
    nOption         As Integer
    nType           As Integer
    nDhcp           As Integer
    nValidDevice    As Integer
    nDtsvrChannel   As Integer
    nStorage        As Integer
    prm             As IN_OUT_ETHPRM_MAINTAIN
End Type
Type OUT_ETHPRM8
    nOption         As Integer
    nType           As Integer
    nDhcp           As Integer
    nValidDevice    As Integer
    nDtsvrChannel   As Integer
    nStorage        As Integer
    prm             As IN_OUT_ETHPRM_NETSRV
End Type
Type OUT_ETHPRM911
    nOption         As Integer
    nType           As Integer
    nDhcp           As Integer
    nValidDevice    As Integer
    nDtsvrChannel   As Integer
    nStorage        As Integer
    prm             As IN_OUT_ETHPRM_UNSOLICMSG11
End Type
Type OUT_ETHPRM912
    nOption         As Integer
    nType           As Integer
    nDhcp           As Integer
    nValidDevice    As Integer
    nDtsvrChannel   As Integer
    nStorage        As Integer
    prm             As IN_OUT_ETHPRM_UNSOLICMSG12
End Type
Type OUT_ETHPRM921
    nOption         As Integer
    nType           As Integer
    nDhcp           As Integer
    nValidDevice    As Integer
    nDtsvrChannel   As Integer
    nStorage        As Integer
    prm             As IN_OUT_ETHPRM_UNSOLICMSG21
End Type
Type OUT_ETHPRM922
    nOption         As Integer
    nType           As Integer
    nDhcp           As Integer
    nValidDevice    As Integer
    nDtsvrChannel   As Integer
    nStorage        As Integer
    prm             As IN_OUT_ETHPRM_UNSOLICMSG22
End Type


Type OUT_ETHDSMODE
  nDsMode(0 To 9) As Integer
End Type

' eth_rddsstate :
Type OUT_DSSTATE
  nDtsvrChannel As Integer
  npad As Integer
  nMode As Integer
  nEmptyCount As Integer
  lTotalSize As Long
  nWritePtr As Integer
  nReadPtr As Integer
End Type

' cnc_rddsfile: get file list infomation
Type IN_DSFILE
  sPath As String * 256  ' path name
  lFnum As Long          ' file number
  lOffset As Long        ' offset
  nReq_num As Integer    ' request file num
  nSize_type As Integer  ' size type
  nDetail As Integer     ' comment type
  nDummy  As Integer
End Type

' cnc_rddsfile: get file list infomation
Type OUT_DSINFO
  nType As Integer       ' type
  nDummy As Integer
  lFnum As Long          ' file num
  lTotal As Long         ' all file num
  lRemain_h As Long      ' remain(high)
  lRemain_l As Long      ' remain(low)
  sDir As String * 256   ' current folder
End Type

' cnc_rddsfile: get file list infomation
Type OUT_DSFILE
  nYear As Integer       ' year
  nMon As Integer        ' month
  nDay As Integer        ' day
  nHour As Integer       ' hour
  nMin As Integer        ' minute
  nSec As Integer        ' second
  lSize As Long          ' size
  lAttr As Long          ' attribute
  sFile As String * 36   ' file name
  sInfo As String * 128  ' file infomation
End Type



'----------------------------
' NET : PROFIBUS function
'----------------------------
' pbm_rd_param
' pbm_wr_param
Type T_MAS_USR
    master_user_data_len          As Integer
    master_user_data(0 To 61)     As Byte
End Type

Type T_BUS_PARA
    sFdl_add            As Byte
    sBaud_rate          As Byte
    nTsl                As Integer
    nMin_tsdr           As Integer
    nMax_tsdr           As Integer
    sTqui               As Byte
    sTset               As Byte
    lTtr                As Long
    sg                  As Byte
    sHsa                As Byte
    sMax_retry_limit    As Byte
    sBp_flag            As Byte
    nMin_slave_interval As Integer
    nPoll_timeout       As Integer
    nData_control_time  As Integer
    sReserved(0 To 5)   As Byte
    sMaster_class2_name As String * 32
    mas_usr             As T_MAS_USR
End Type

Type T_MODE_ADDR_ALLOC
    sMd_path        As Byte
    sMd_kind        As String * 1
    nMd_top_address As Integer
    sMd_size        As Byte
    sPad            As Byte
End Type

Type T_SLAVE_IND_PARA
    sSlv_idx As Byte
    sSlv_no  As Byte
End Type

Type T_SLAVE_SUB_PARA
    slv_ind_para As T_SLAVE_IND_PARA
    sSlv_enable   As Byte
    sSlt_num      As Byte
End Type

Type T_USR_PRM_DATA
    nUser_prm_data_len As Integer
    sUser_prm_data(0 To 200) As Byte
    sPad               As Byte
End Type

Type T_PRM_DATA
    sStation_status As Byte
    sWd_fact_1      As Byte
    sWd_fact_2      As Byte
    sMin_tsdr       As Byte
    nIdent_number   As Integer
    sGroup_ident    As Byte
    sPad            As Byte
    usr_prm         As T_USR_PRM_DATA
End Type

Type T_CFG_DATA
    nCfg_data_len       As Integer
    sCfg_data(0 To 127) As Byte
End Type

Type T_SLV_USR_DATA
    nSlave_user_data_len      As Integer
    sSlave_user_data(0 To 29) As Byte
End Type

Type T_SLAVE_PARA
    slv_ind_para       As T_SLAVE_IND_PARA
    sSl_flag           As Byte
    sSlave_type        As Byte
    sReserved(0 To 11) As Byte
    prm_data           As T_PRM_DATA
    cfg_data           As T_CFG_DATA
    slv_usr            As T_SLV_USR_DATA
End Type

Type T_DGN_ADDR_ALLOC
    slv_ind_para     As T_SLAVE_IND_PARA
    sDgn_path        As Byte
    sDgn_kind        As String * 1
    nDgn_top_address As Integer
    sDgn_size        As Byte
    sPad             As Byte
End Type

Type T_SLOT_IND_PARA
    sSlv_no As Byte
    sSlt_no As Byte
End Type

Type T_MODULE_DATA
    slt_ind_para           As T_SLOT_IND_PARA
    nModule_len            As Integer
    sModule_data(0 To 127) As Byte
End Type

Type T_DIDO_ADDR_ALLOC
    slt_ind_para    As T_SLOT_IND_PARA
    sDi_path        As Byte
    sDo_path        As Byte
    sDi_kind        As String * 1
    sDo_kind        As String * 1
    nDi_top_address As Integer
    nDo_top_address As Integer
    sDi_size        As Byte
    sDo_size        As Byte
    sModule_type    As Byte
    sPad            As Byte
End Type

Type IN_OUT_PBMPRM_PRM1
    bus_para            As T_BUS_PARA
    sDummy(0 To 261)    As Byte             'It make the same size as size of largest structure. (MaxSize)390 - (T_BUS_PARA Size)128 = 262byte
End Type

Type IN_OUT_PBMPRM_PRM2
    mode_addr_alloc     As T_MODE_ADDR_ALLOC
    sDummy(0 To 383)    As Byte             'It make the same size as size of largest structure. (MaxSize)390 - (T_MODE_ADDR_ALLOC Size)6 = 384byte
End Type

Type IN_OUT_PBMPRM_PRM3
    slv_sub_para        As T_SLAVE_SUB_PARA
    sDummy(0 To 385)    As Byte             'It make the same size as size of largest structure. (MaxSize)390 - (T_SLAVE_SUB_PARA Size)4 = 386byte
End Type

Type IN_OUT_PBMPRM_PRM4
    slv_para            As T_SLAVE_PARA     'The size of this structure is 390byte.(This is MaxSize)
End Type

Type IN_OUT_PBMPRM_PRM5
    dgn_addr_alloc      As T_DGN_ADDR_ALLOC
    sDummy(0 To 381)    As Byte             'It make the same size as size of largest structure. (MaxSize)390 - (T_DGN_ADDR_ALLOC Size)8 = 382byte
End Type

Type IN_OUT_PBMPRM_PRM6
    module_data         As T_MODULE_DATA
    sDummy(0 To 384)    As Byte             'It make the same size as size of largest structure. (MaxSize)390 - (T_MODULE_DATA Size)5 = 385byte
End Type

Type IN_OUT_PBMPRM_PRM7
    dido_addr_alloc     As T_DIDO_ADDR_ALLOC
    sDummy(0 To 375)    As Byte             'It make the same size as size of largest structure. (MaxSize)390 - (T_DIDO_ADDR_ALLOC Size)14 = 376byte
End Type

Type IN_OUT_PBMPRM1
    prm     As IN_OUT_PBMPRM_PRM1
End Type

Type IN_OUT_PBMPRM2
    prm     As IN_OUT_PBMPRM_PRM2
End Type

Type IN_OUT_PBMPRM3
    prm     As IN_OUT_PBMPRM_PRM3
End Type

Type IN_OUT_PBMPRM4
    prm     As IN_OUT_PBMPRM_PRM4
End Type

Type IN_OUT_PBMPRM5
    prm     As IN_OUT_PBMPRM_PRM5
End Type

Type IN_OUT_PBMPRM6
    prm     As IN_OUT_PBMPRM_PRM6
End Type

Type IN_OUT_PBMPRM7
    prm     As IN_OUT_PBMPRM_PRM7
End Type


' pbm_wr_param
Type T_MAS_USR_FLG
    sMaster_user_data_len As Byte
    sMaster_user_data As Byte
End Type

Type T_BUS_PARA_FLG
    sFdl_add            As Byte
    sBaud_rate          As Byte
    sTsl                As Byte
    sMin_tsdr           As Byte
    sMax_tsdr           As Byte
    sTqui               As Byte
    sTset               As Byte
    sTtr                As Byte
    sg                  As Byte
    sHsa                As Byte
    sMax_retry_limit    As Byte
    sBp_flag            As Byte
    sMin_slave_interval As Byte
    sPoll_timeout       As Byte
    sData_control_time  As Byte
    sPad1               As Byte
    sReserved(0 To 5)   As Byte
    sMaster_class2_name_rsv As Byte
    sPad2               As Byte
    mas_usr             As T_MAS_USR_FLG
End Type

Type T_MODE_ADDR_ALLOC_FLG
    sMd_path        As Byte
    sMd_kind        As Byte
    sMd_top_address As Byte
    sMd_size        As Byte
End Type

Type T_SLAVE_IND_PARA_FLG
    sSlv_idx As Byte
    sSlv_no  As Byte
End Type

Type T_SLAVE_SUB_PARA_FLG
    slv_ind_para As T_SLAVE_IND_PARA_FLG
    sSlv_enable  As Byte
    sSlt_num     As Byte
End Type

Type T_USR_PRM_DATA_FLG
    sUser_prm_data_len As Byte
    sUser_prm_data     As Byte
End Type

Type T_PRM_DATA_FLG
    sStation_status    As Byte
    sWd_fact_1         As Byte
    sWd_fact_2         As Byte
    sMin_tsdr          As Byte
    sIdent_number      As Byte
    sGroup_ident       As Byte
    usr_prm            As T_USR_PRM_DATA_FLG
End Type

Type T_CFG_DATA_FLG
    sCfg_data_len      As Byte
    sCfg_data          As Byte
End Type

Type T_SLV_USR_DATA_FLG
    sSlave_user_data_len    As Byte
    sSlave_user_data        As Byte
End Type

Type T_SLAVE_PARA_FLG
    slv_ind_para As T_SLAVE_IND_PARA_FLG
    sSl_flag      As Byte
    sSlave_type  As Byte
    sReserved(0 To 11) As Byte
    prm_data As T_PRM_DATA_FLG
    cfg_data_rsv As T_CFG_DATA_FLG
    slv_usr As T_SLV_USR_DATA_FLG
End Type

Type T_DGN_ADDR_ALLOC_FLG
    slv_ind_para        As T_SLAVE_IND_PARA_FLG
    sDgn_path           As Byte
    sDgn_kind           As Byte
    sDgn_top_address    As Byte
    sDgn_size           As Byte
End Type

Type T_SLOT_IND_PARA_FLG
    sSlv_no As Byte
    sSlt_no As Byte
End Type

Type T_MODULE_DATA_FLG
    slt_ind_para As T_SLOT_IND_PARA_FLG
    sModule_len  As Byte
    sModule_data As Byte
End Type

Type T_DIDO_ADDR_ALLOC_FLG
    slt_ind_para     As T_SLOT_IND_PARA_FLG
    sDi_path         As Byte
    sDo_path         As Byte
    sDi_kind         As Byte
    sDo_kind         As Byte
    sDi_top_address  As Byte
    sDo_top_address  As Byte
    sDi_size         As Byte
    sDo_size         As Byte
    sModule_type_rsv As Byte
    sPad             As Byte
End Type

Type IN_PBMPRMFLG_PRM1
    bus_para As T_BUS_PARA_FLG
End Type
Type IN_PBMPRMFLG_PRM2
    mode_addr_alloc As T_MODE_ADDR_ALLOC_FLG
End Type
Type IN_PBMPRMFLG_PRM3
    slv_sub_para As T_SLAVE_SUB_PARA_FLG
End Type
Type IN_PBMPRMFLG_PRM4
    slv_para As T_SLAVE_PARA_FLG
End Type
Type IN_PBMPRMFLG_PRM5
    dgn_addr_alloc As T_DGN_ADDR_ALLOC_FLG
End Type
Type IN_PBMPRMFLG_PRM6
    module_data As T_MODULE_DATA_FLG
End Type
Type IN_PBMPRMFLG_PRM7
    dido_addr_alloc As T_DIDO_ADDR_ALLOC_FLG
End Type

Type IN_PBMPRMFLG1
    prm As IN_PBMPRMFLG_PRM1
End Type
Type IN_PBMPRMFLG2
    prm As IN_PBMPRMFLG_PRM2
End Type
Type IN_PBMPRMFLG3
    prm As IN_PBMPRMFLG_PRM3
End Type
Type IN_PBMPRMFLG4
    prm As IN_PBMPRMFLG_PRM4
End Type
Type IN_PBMPRMFLG5
    prm As IN_PBMPRMFLG_PRM5
End Type
Type IN_PBMPRMFLG6
    prm As IN_PBMPRMFLG_PRM6
End Type
Type IN_PBMPRMFLG7
    prm As IN_PBMPRMFLG_PRM7
End Type

' pbm_ini_prm
Type T_SLVSLT_IND
    sSlv_no As Byte
    sSlt_no As Byte
End Type

' pbm_rd_allslvtbl
Type T_SLVTBL
    slv_ind_para As T_SLAVE_IND_PARA
    sSlv_enable   As Byte
    sSlt_num      As Byte
    sDgn_path     As Byte
    sDgn_kind     As String * 1
    nDgn_top_address As Integer
    sDgn_size     As Byte
    sPad          As Byte
End Type

Type OUT_ALLSLVTBL
    slv_tbl(0 To 79) As T_SLVTBL
End Type

' pbm_rd_subprm
Type T_MAXMODLENPRM
    sSlv_no      As Byte
    sSlt_no      As Byte
    sMax_mod_len As Byte
    sPad         As Byte
End Type

Type OUT_PBMSUBPRM1
    sMax_slv_num As Byte
End Type

Type OUT_PBMSUBPRM2
    sMax_slt_num As Byte
End Type

Type OUT_PBMSUBPRM3
    sEnb_slv_num As Byte
End Type

Type OUT_PBMSUBPRM4
    sTotal_slts As Byte
End Type

Type OUT_PBMSUBPRM5
    sShift_mode_stat As Byte
End Type

Type OUT_PBMSUBPRM_PRM6
    max_mod_len_prm As T_MAXMODLENPRM
End Type

Type OUT_PBMSUBPRM6
    subprm As OUT_PBMSUBPRM_PRM6
End Type

' pbm_rd_errcode
Type T_ERR_CODE
    nParam_err_code(0 To 3) As Integer
    nInter_err_code(0 To 3) As Integer
End Type

' pbm_chg_mode
Type OUT_CHGMODERESULT
    sCrnt_mode As Byte
    sPad       As Byte
    nResult    As Integer
End Type

' pbm_rd_cominfo
Type T_DATA_REF_TIM
    nTotal_ref_tim As Integer
    nBoard_ref_tim As Integer
    nCnc_ref_tim   As Integer
End Type

Type OUT_PBMCOMINFO1
    sCrnt_mode  As Byte
End Type

Type OUT_PBMCOMINFO2
    data_ref_tim As T_DATA_REF_TIM
End Type

' pbm_rd_nodeinfo
Type OUT_PBMNODEINFO
    sSlv_no     As Byte
    sCommstat   As Byte
    sStatus1    As Byte
    sStatus2    As Byte
    sStatus3    As Byte
    sMaster     As Byte
    nIdent      As Integer
End Type

' pbm_rd_slotinto
Type OUT_PBMSLOTINFO
    sSlv_no         As Byte
    sSlt_no         As Byte
    sDi_size        As Byte
    sDo_size        As Byte
    sDi_path        As Byte
    sDo_path        As Byte
    sDi_kind        As String * 1
    sDo_kind        As String * 1
    nDi_top_address As Integer
    nDo_top_address As Integer
    sModule_type    As Byte
    sCommstat       As Byte
    nReserved       As Integer
End Type

' pbs_rd_param
' pbs_wr_param
Type IN_OUT_PBSPRM
    sSlave_no       As Byte
    sPad            As Byte
    sDi_size        As Byte
    sDo_size        As Byte
    sDi_path        As Byte
    sDo_path        As Byte
    sDi_kind        As String * 1
    sDo_kind        As String * 1
    nDi_top_address As Integer
    nDo_top_address As Integer

End Type

' pbs_wr_param
Type IN_PBSPRMFLG
    sSlave_no       As Byte
    sPad            As Byte
    sDi_size        As Byte
    sDo_size        As Byte
    sDi_path        As Byte
    sDo_path        As Byte
    sDi_kind        As Byte
    sDo_kind        As Byte
    sDi_top_address As Byte
    sDo_top_address As Byte
End Type

' pbs_rd_cominfo
Type OUT_PBSSTATUS
    sConfig_sts     As Byte
    sParam_sts      As Byte
    sWatchdog_sts   As Byte
    sPad            As Byte
    nIdent_no       As Integer
End Type

' pbs_rd_param2
' pbs_wr_param2
Type IN_OUT_PBSPRM2
    sSlave_no        As Byte
    sPad             As Byte
    sDi_size         As Byte
    sDo_size         As Byte
    sDi_path         As Byte
    sDo_path         As Byte
    sDi_kind         As String * 1
    sDo_kind         As String * 1
    nDi_top_address  As Integer
    nDo_top_address  As Integer
    sSts_path        As Byte
    sSts_kind        As String * 1
    nSts_top_address As Integer
End Type

' pbs_wr_param2
Type IN_PBSPRMFLG2
    sSlave_no        As Byte
    sPad1            As Byte
    sDi_size         As Byte
    sDo_size         As Byte
    sDi_path         As Byte
    sDo_path         As Byte
    sDi_kind         As Byte
    sDo_kind         As Byte
    sDi_top_address  As Byte
    sDo_top_address  As Byte
    sSts_path        As Byte
    sSts_kind        As Byte
    sSts_top_address As Byte
    sPad2            As Byte
End Type

' pbs_rd_cominfo2
Type OUT_PBSSTATUS2
    sConfig_sts     As Byte
    sParam_sts      As Byte
    sWatchdog_sts   As Byte
    sPad1           As Byte
    nIdent_no       As Integer
    sSts            As Byte
    sPad2           As Byte
End Type


'--------------------------
' HSSB multiple connection
'--------------------------

' cnc_rdnodeinfo:read node informations
Type ODBNODE
  nNode_no As Long
  nIo_base As Long
  nStatus As Long
  nCnc_type As Long
  sNode_name As String * 20
End Type


'-------------------------------------
' CNC: Control axis / spindle related
'-------------------------------------

' read actual axis feedrate(F)
Declare Function cnc_actf Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As ODBACT) As Integer

' read absolute axis position
Declare Function cnc_absolute Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, Odb As ODBAXIS) As Integer

' read machine axis position
Declare Function cnc_machine Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, Odb As ODBAXIS) As Integer

' read machine axis position(2)
Declare Function cnc_machine2 Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, ByVal c As Integer, Odb As ODBAXIS) As Integer

' read relative axis position
Declare Function cnc_relative Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, Odb As ODBAXIS) As Integer

' read distance to go
Declare Function cnc_distance Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, Odb As ODBAXIS) As Integer

' read skip position
Declare Function cnc_skip Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, Odb As ODBAXIS) As Integer

' read servo delay value
Declare Function cnc_srvdelay Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, Odb As ODBAXIS) As Integer

' read acceleration/deceleration delay value
Declare Function cnc_accdecdly Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, Odb As ODBAXIS) As Integer

' read all dynamic data
#If ONO8D Then
Declare Function cnc_rddynamic Lib "fwlib32.dll" Alias "cnc_rddynamico8" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, Odb As Any) As Integer
#Else
Declare Function cnc_rddynamic Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, Odb As Any) As Integer
#End If

' read all dynamic data 2
Declare Function cnc_rddynamic2 Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, Odb As Any) As Integer

' read actual spindle speed(S)
Declare Function cnc_acts Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As ODBACT) As Integer

' read actual spindle speed(S) (All or specified)
Declare Function cnc_acts2 Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, Odb As ODBACT2) As Integer

' set origin / preset relative axis position
Declare Function cnc_wrrelpos Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, Idb As IDBWRR) As Integer

' preset work coordinate
Declare Function cnc_prstwkcd Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, Idb As IDBWRA) As Integer

' read manual overlapped motion value
Declare Function cnc_rdmovrlap Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, Odb As IODBOVL) As Integer

' cancel manual overlapped motion value
Declare Function cnc_canmovrlap Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer) As Integer

' read load information of serial spindle
Declare Function cnc_rdspload Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, Odb As ODBSPN) As Integer

' read maximum r.p.m. ratio of serial spindle
Declare Function cnc_rdspmaxrpm Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, Odb As ODBSPN) As Integer

' read gear ratio of serial spindle
Declare Function cnc_rdspgear Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, Odb As ODBSPN) As Integer

' read absolute axis position 2
Declare Function cnc_absolute2 Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, Odb As ODBAXIS) As Integer

' read relative axis position 2
Declare Function cnc_relative2 Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, Odb As ODBAXIS) As Integer

' set wire vertival position
Declare Function cnc_setvrtclpos Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal b As Integer) As Integer

' read tool position
Declare Function cnc_rdposition Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal nType As Integer, nLen As Integer, Odb As Any) As Integer

' read current speed
Declare Function cnc_rdspeed Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal nType As Integer, Odb As ODBSPEED) As Integer

' read servo load meter
Declare Function cnc_rdsvmeter Lib "fwlib32.dll" (ByVal FlibHndl As Integer, nLen As Integer, Odb As ODBSVLOAD) As Integer

' read spindle load meter
Declare Function cnc_rdspmeter Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal nType As Integer, nLen As Integer, Odb As Any) As Integer

' read handle interruption
Declare Function cnc_rdhndintrpt Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal nType As Integer, nLen As Integer, Odb As ODBHND) As Integer

' read manual feed for 5-axis machining
Declare Function cnc_rd5axmandt Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As ODB5AXMAN) As Integer

' read amount of machine axes movement of manual feed for 5-axis machining
Declare Function cnc_rd5axovrlap Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, Odb As ODBAXIS) As Integer

' clear pulse values of manual feed for 5-axis machining
Declare Function cnc_clr5axpls Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer) As Integer

' read the spindle speed
Declare Function cnc_rdspdlspeed Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, c As Long) As Integer

' read various axis data
Declare Function cnc_rdaxisdata Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, b As Integer, ByVal c As Integer, d As Integer, Odb As Any) As Integer


'----------------------
' CNC: Program related
'----------------------

' start downloading NC program
Declare Function cnc_dwnstart Lib "fwlib32.dll" (ByVal FlibHndl As Integer) As Integer

' download NC program
Declare Function cnc_download Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As String, ByVal b As Integer) As Integer

' download NC program(conditional)
Declare Function cnc_cdownload Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As String, ByVal b As Integer) As Integer

' end of downloading NC program
Declare Function cnc_dwnend Lib "fwlib32.dll" (ByVal FlibHndl As Integer) As Integer

' end of downloading NC program 2
Declare Function cnc_dwnend2 Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As String) As Integer

' start downloading NC program 3
Declare Function cnc_dwnstart3 Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer) As Integer

' start downloading NC program 3 (special)
Declare Function cnc_dwnstart3_f Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As String, ByVal c As String) As Integer

' download NC program 3
Declare Function cnc_download3 Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Long, ByVal b As String) As Integer

' end of downloading NC program 3
Declare Function cnc_dwnend3 Lib "fwlib32.dll" (ByVal FlibHndl As Integer) As Integer

' start downloading NC program 4
Declare Function cnc_dwnstart4 Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As String) As Integer

' download NC program 4
Declare Function cnc_download4 Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Long, ByVal b As String) As Integer

' end of downloading NC program 4
Declare Function cnc_dwnend4 Lib "fwlib32.dll" (ByVal FlibHndl As Integer) As Integer

' start verification of NC program
Declare Function cnc_vrfstart Lib "fwlib32.dll" (ByVal FlibHndl As Integer) As Integer

' verify NC program
Declare Function cnc_verify Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As String, ByVal b As Integer) As Integer

' verify NC program(conditional)
Declare Function cnc_cverify Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As String, ByVal b As Integer) As Integer

' end of verification
Declare Function cnc_vrfend Lib "fwlib32.dll" (ByVal FlibHndl As Integer) As Integer

' start verification of NC program
Declare Function cnc_vrfstart4 Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As String) As Integer

' verify NC program
Declare Function cnc_verify4 Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Long, ByVal b As String) As Integer

' end of verification
Declare Function cnc_vrfend4 Lib "fwlib32.dll" (ByVal FlibHndl As Integer) As Integer

' start downloading DNC program
Declare Function cnc_dncstart Lib "fwlib32.dll" (ByVal FlibHndl As Integer) As Integer

' download DNC program
Declare Function cnc_dnc Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As String, ByVal b As Long) As Integer

' download DNC program(conditional)
Declare Function cnc_cdnc Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As String, ByVal b As Long) As Integer

' end of downloading DNC program
Declare Function cnc_dncend Lib "fwlib32.dll" (ByVal FlibHndl As Integer) As Integer

' start downloading DNC program 2
Declare Function cnc_dncstart2 Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As String) As Integer

' download DNC program 2
Declare Function cnc_dnc2 Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Long, ByVal b As String) As Integer

' end of downloading DNC program 2
Declare Function cnc_dncend2 Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer) As Integer

' read the diagnosis data of DNC operation
Declare Function cnc_rddncdgndt Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As ODBDNCDGN) As Integer

' start uploading NC program
#If ONO8D Then
Declare Function cnc_upstart Lib "fwlib32.dll" Alias "cnc_upstarto8" (ByVal FlibHndl As Integer, ByVal a As Long) As Integer
#Else
Declare Function cnc_upstart Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer) As Integer
#End If

' upload NC program
Declare Function cnc_upload Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As ODBUP, a As Long) As Integer

' upload NC program(conditional)
Declare Function cnc_cupload Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As ODBUP, a As Long) As Integer

' end of uploading NC program
Declare Function cnc_upend Lib "fwlib32.dll" (ByVal FlibHndl As Integer) As Integer

' start uploading NC program 3
Declare Function cnc_upstart3 Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Long, ByVal c As Long) As Integer

' start uploading NC program 3 (special)
Declare Function cnc_upstart3_f Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As String, ByVal c As String) As Integer

' upload NC program 3
Declare Function cnc_upload3 Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Long, ByVal b As String) As Integer

' end of uploading NC program
Declare Function cnc_upend3 Lib "fwlib32.dll" (ByVal FlibHndl As Integer) As Integer

' start uploading NC program 4
Declare Function cnc_upstart4 Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As String) As Integer

' upload NC program 4
Declare Function cnc_upload4 Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Long, ByVal b As String) As Integer

' end of uploading NC program 4
Declare Function cnc_upend4 Lib "fwlib32.dll" (ByVal FlibHndl As Integer) As Integer

' read buffer status for downloading/verification NC program
Declare Function cnc_buff Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As ODBBUF) As Integer

' search specified program
#If ONO8D Then
Declare Function cnc_search Lib "fwlib32.dll" Alias "cnc_searcho8" (ByVal FlibHndl As Integer, ByVal a As Long) As Integer
#Else
Declare Function cnc_search Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer) As Integer
#End If

' delete all programs
Declare Function cnc_delall Lib "fwlib32.dll" (ByVal FlibHndl As Integer) As Integer

' delete all programs
Declare Function cnc_pdf_delall Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As String) As Integer

' delete specified program
#If ONO8D Then
Declare Function cnc_delete Lib "fwlib32.dll" Alias "cnc_deleteo8" (ByVal FlibHndl As Integer, ByVal a As Long) As Integer
#Else
Declare Function cnc_delete Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer) As Integer
#End If

' delete program (area specified)
Declare Function cnc_delrange Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Long, ByVal b As Long) As Integer

' read program directory
#If ONO8D Then
Declare Function cnc_rdprogdir Lib "fwlib32.dll" Alias "cnc_rdprogdiro8" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Long, ByVal c As Long, ByVal d As Long, Odb As PRGDIR) As Integer
#Else
Declare Function cnc_rdprogdir Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, ByVal c As Integer, ByVal d As Long, Odb As PRGDIR) As Integer
#End If

' read program information
Declare Function cnc_rdproginfo Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, Odb As Any) As Integer

' read program number under execution
#If ONO8D Then
Declare Function cnc_rdprgnum Lib "fwlib32.dll" Alias "cnc_rdprgnumo8" (ByVal FlibHndl As Integer, Odb As ODBPRO) As Integer
#Else
Declare Function cnc_rdprgnum Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As ODBPRO) As Integer
#End If

' read program name under execution
Declare Function cnc_exeprgname Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As ODBEXEPRG) As Integer

' read program name under execution (Full path)
Declare Function cnc_exeprgname2 Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As String) As Integer

' read sequence number under execution
Declare Function cnc_rdseqnum Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As ODBSEQ) As Integer

' search specified sequence number
Declare Function cnc_seqsrch Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Long) As Integer

' search specified sequence number (2)
Declare Function cnc_seqsrch2 Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Long) As Integer

' rewind cursor of NC program
Declare Function cnc_rewind Lib "fwlib32.dll" (ByVal FlibHndl As Integer) As Integer

' read block counter
Declare Function cnc_rdblkcount Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Long) As Integer

' read program under execution
Declare Function cnc_rdexecprog Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Long, b As Integer, ByVal c As String) As Integer
Declare Function cnc_rdexecprog2 Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Long, b As Integer, c As Integer, ByVal d As String) As Integer

' read program for MDI operation
Declare Function cnc_rdmdiprog Lib "fwlib32.dll" (ByVal FlibHndl As Integer, nLen As Integer, ByVal sProg As String) As Integer

' write program for MDI operation
Declare Function cnc_wrmdiprog Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal nLen As Integer, ByVal sProg As String) As Integer

' read execution pointer for MDI operation
#If ONO8D Then
Declare Function cnc_rdmdipntr Lib "fwlib32.dll" Alias "cnc_rdmdipntro8" (ByVal FlibHndl As Integer, Odb As ODBMDIP) As Integer
#Else
Declare Function cnc_rdmdipntr Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As ODBMDIP) As Integer
#End If

' write execution pointer for MDI operation
Declare Function cnc_wrmdipntr Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Long) As Integer

' read constantly spindle speed
Declare Function cnc_rdspcss Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As ODBCSS) As Integer

' read Ethernet board information
Declare Function cnc_rdetherinfo Lib "fwlib32.dll" (ByVal FlibHndl As Integer, nType As Integer, nDevice As Integer) As Integer

' read execution program pointer
Declare Function cnc_rdexecpt Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As PRGPNT, Odb As PRGPNT) As Integer

' read MDI program status
Declare Function cnc_rdmdiprgstat Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Integer) As Integer

' line edit (read program)
Declare Function cnc_rdprogline Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Long, ByVal b As Long, ByVal c As String, d As Long, e As Long) As Integer
Declare Function cnc_rdprogline2 Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Long, ByVal b As Long, ByVal c As String, d As Long, e As Long) As Integer

' line edit (write program)
Declare Function cnc_wrprogline Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Long, ByVal b As Long, c As Byte, ByVal d As Long) As Integer

' line edit (delete line in program)
Declare Function cnc_delprogline Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Long, ByVal b As Long, ByVal c As Long) As Integer

' line edit (search string)
Declare Function cnc_searchword Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Long, ByVal b As Long, ByVal c As Integer, ByVal d As Integer, e As Long, f As Byte) As Integer
Declare Function cnc_pdf_searchword Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As String, ByVal b As Long, ByVal c As Long, ByVal d As Long, ByVal e As Long, ByVal f As String) As Integer

' line edit (search string)
Declare Function cnc_searchresult Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Long) As Integer
Declare Function cnc_pdf_searchresult Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Long) As Integer

' program lock
Declare Function cnc_setpglock Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Long) As Integer

' program unlock
Declare Function cnc_resetpglock Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Long) As Integer

' read the status of the program lock
Declare Function cnc_rdpglockstat Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Long, b As Long) As Integer

' register new program
Declare Function cnc_newprog Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Long) As Integer

' NC program copy
Declare Function cnc_copyprog Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Long, ByVal b As Long) As Integer

' condense program
Declare Function cnc_condense Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Long) As Integer

' merge program
Declare Function cnc_mergeprog Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Long, ByVal c As Long, ByVal d As Long) As Integer

' read current program and its pointer
Declare Function cnc_rdactpt Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Long, b As Long) As Integer
Declare Function cnc_pdf_rdactpt Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As String, b As Long) As Integer

' set current program and its pointer
Declare Function cnc_wractpt Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Long, ByVal b As Integer, c As Long) As Integer
Declare Function cnc_pdf_wractpt Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As String, ByVal b As Integer, c As Long) As Integer

' line edit (read program by file name)
Declare Function cnc_rdpdf_line Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As String, ByVal b As Long, ByVal c As String, d As Long, e As Long) As Integer

' line edit (write program by file name)
Declare Function cnc_wrpdf_line Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As String, ByVal b As Long, ByVal c As String, ByVal d As Long) As Integer

' line edit (delete line by file name)
Declare Function cnc_pdf_delline Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As String, ByVal b As Long, ByVal c As Long) As Integer

' read program drive directory
Declare Function cnc_rdpdf_drive Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As ODBPDFDRV) As Integer

' read program drive information
Declare Function cnc_rdpdf_inf Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As String, ByVal b As Integer, Odb As ODBPDFINF) As Integer

' read current directory
Declare Function cnc_rdpdf_curdir Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As String) As Integer

' set current directory
Declare Function cnc_wrpdf_curdir Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As String) As Integer

' read directory (sub directories)
Declare Function cnc_rdpdf_subdir Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Integer, Idb As IDBPDFSDIR, Odb As Any) As Integer

' read directory (all files)
Declare Function cnc_rdpdf_alldir Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Integer, Idb As IDBPDFADIR, Odb As Any) As Integer

' read file count in directory
Declare Function cnc_rdpdf_subdirn Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As String, Odb As ODBPDFNFIL) As Integer

' create file or directory
Declare Function cnc_pdf_add Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As String) As Integer

' delete file or directory
Declare Function cnc_pdf_del Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As String) As Integer

' rename file or directory
Declare Function cnc_pdf_rename Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As String, ByVal b As String) As Integer

' read selected file name
Declare Function cnc_pdf_rdmain Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As String) As Integer

' select program file
Declare Function cnc_pdf_slctmain Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As String) As Integer

' condense program file
Declare Function cnc_pdf_cond Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As String) As Integer

' change attribute of program file and directory
Declare Function cnc_wrpdf_attr Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As String, b As IDBPDFTDIR) As Integer

' copy program file
Declare Function cnc_pdf_copy Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As String, ByVal b As String) As Integer

' move program file
Declare Function cnc_pdf_move Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As String, ByVal b As String) As Integer


'---------------------------
' CNC: NC file data related
'---------------------------

' read tool offset value
Declare Function cnc_rdtofs Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, ByVal c As Integer, Odb As ODBTOFS) As Integer

' write tool offset value
Declare Function cnc_wrtofs Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, ByVal c As Integer, ByVal d As Long) As Integer

' read tool offset value(area specified)
Declare Function cnc_rdtofsr Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, ByVal c As Integer, ByVal d As Integer, Odb As Any) As Integer

' write tool offset value(area specified)
Declare Function cnc_wrtofsr Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, Idb As Any) As Integer

' read work zero offset value
Declare Function cnc_rdzofs Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, ByVal c As Integer, Odb As IODBZOFS) As Integer

' write work zero offset value
Declare Function cnc_wrzofs Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, Idb As IODBZOFS) As Integer

' read work zero offset value(area specified)
Declare Function cnc_rdzofsr Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, ByVal c As Integer, ByVal d As Integer, Odb As IODBZOR) As Integer

' write work zero offset value(area specified)
Declare Function cnc_wrzofsr Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, Idb As IODBZOR) As Integer

' read mesured point value
Declare Function cnc_rdmsptype Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, ByVal c As Integer, d As Any) As Integer

' write mesured point value
Declare Function cnc_wrmsptype Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, b As Any) As Integer

' read parameter
Declare Function cnc_rdparam Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, ByVal c As Integer, Odb As Any) As Integer

' read parameter (3)
Declare Function cnc_rdparam3 Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, ByVal c As Integer, ByVal d As Integer, Odb As Any) As Integer

' write parameter
Declare Function cnc_wrparam Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, Idb As Any) As Integer

' read parameter(area specified)
Declare Function cnc_rdparar Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Integer, ByVal b As Integer, c As Integer, d As Integer, Odb As Any) As Integer

' write parameter(area specified)
Declare Function cnc_wrparas Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, Idb As Any) As Integer

' read setting data
Declare Function cnc_rdset Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, ByVal c As Integer, Odb As Any) As Integer

' write setting data
Declare Function cnc_wrset Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, Idb As Any) As Integer

' read setting data(area specified)
Declare Function cnc_rdsetr Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Integer, ByVal b As Integer, c As Integer, d As Integer, Odb As Any) As Integer

' write setting data(area specified)
Declare Function cnc_wrsets Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, Idb As Any) As Integer

' read parameters
Declare Function cnc_rdparam_ext Lib "fwlib32.dll" (ByVal FlibHndl As Integer, lPrmNo As Long, ByVal nLen As Integer, Odb As IODBPRM) As Integer

' read diagnosis data
Declare Function cnc_rddiag_ext Lib "fwlib32.dll" (ByVal FlibHndl As Integer, lPrmNo As Long, ByVal nLen As Integer, Odb As IODBPRM) As Integer

' read pitch error compensation data(area specified)
Declare Function cnc_rdpitchr Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, ByVal c As Integer, Odb As IODBPI) As Integer

' write pitch error compensation data(area specified)
Declare Function cnc_wrpitchr Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, Idb As IODBPI) As Integer

' read custom macro variable
Declare Function cnc_rdmacro Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, Odb As ODBM) As Integer

' write custom macro variable
Declare Function cnc_wrmacro Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, ByVal c As Long, ByVal d As Integer) As Integer

' read custom macro variable 2
Declare Function cnc_rdmacro2 Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, Odb As ODBM) As Integer

' read custom macro variable (3)
Declare Function cnc_rdmacro3 Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Long, ByVal b As Integer, Odb As ODBM3) As Integer

' write custom macro variable (3)
Declare Function cnc_wrmacro3 Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Long, ByVal b As Integer, ByVal c As Long, ByVal d As Integer) As Integer

' read custom macro variables(area specified)
Declare Function cnc_rdmacror Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, ByVal c As Integer, Odb As IODBMR) As Integer

' write custom macro variables(area specified)
Declare Function cnc_wrmacror Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, Idb As IODBMR) As Integer

' read custom macro variables(IEEE double version)
Declare Function cnc_rdmacror2 Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Long, b As Long, c As Double) As Integer

Declare Function cnc_rdmacror3 Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Long, b As Long, Odb As Any) As Integer

' write custom macro variables(IEEE double version)
Declare Function cnc_wrmacror2 Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Long, b As Long, c As Double) As Integer

' read P code macro variable
Declare Function cnc_rdpmacro Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Long, Odb As ODBPM) As Integer

' write P code macro variable
Declare Function cnc_wrpmacro Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Long, ByVal b As Long, ByVal c As Integer) As Integer

' read P code macro variables(area specified)
Declare Function cnc_rdpmacror Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Long, ByVal b As Long, ByVal c As Long, Odb As IODBPR) As Integer

' write P code macro variables(area specified)
Declare Function cnc_wrpmacror Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Long, Idb As IODBPR) As Integer

' read P code macro variables(IEEE double version)
Declare Function cnc_rdpmacror2 Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Long, b As Long, ByVal c As Integer, d As Double) As Integer

' write P code macro variables(IEEE double version)
Declare Function cnc_wrpmacror2 Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Long, b As Long, ByVal c As Integer, d As Double) As Integer

' read tool offset information
Declare Function cnc_rdtofsinfo Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As ODBTLINF) As Integer

' read tool offset information(2)
Declare Function cnc_rdtofsinfo2 Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As ODBTLINF2) As Integer

' read work zero offset information
Declare Function cnc_rdzofsinfo Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Integer) As Integer

' read pitch error compensation data information
Declare Function cnc_rdpitchinfo Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Integer) As Integer

' read custom macro variable information
Declare Function cnc_rdmacroinfo Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As ODBMVINF) As Integer

' read volumetric compensation data
Declare Function cnc_rdvolc Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Idb As ODBVOLC, Odb As Any) As Integer

' write volumetric compensation data
Declare Function cnc_wrvolc Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Idb As ODBVOLC, Idb As Any) As Integer

' get volumetric compensation amount of axes
Declare Function cnc_rdvolccomp Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As ODBVOLCOMP) As Integer

' read P code macro variable information
Declare Function cnc_rdpmacroinfo Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As ODBPMINF) As Integer

' read P code macro variable information (2)
Declare Function cnc_rdpmacroinfo2 Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As ODBPMINF2) As Integer

' read validity of tool offset
Declare Function cnc_tofs_rnge Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, Odb As ODBDATRNG) As Integer

' read validity of work zero offset
Declare Function cnc_zofs_rnge Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, Odb As ODBDATRNG) As Integer

' read validity of work shift value
Declare Function cnc_wksft_rnge Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, Odb As ODBDATRNG) As Integer

' read the information for function cnc_rdhsparam()
Declare Function cnc_rdhsprminfo Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Long, Odb As HSPINFO) As Integer

' read parameters at the high speed
Declare Function cnc_rdhsparam Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Long, Odb As Any, Odb As Any) As Integer

' read rotary table dynamic fixture offset
Declare Function cnc_rdfixoffs Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, Odb As ODBFOFS) As Integer


'----------------------------------------
' CNC: Tool life management data related
'----------------------------------------

' read tool life management data(tool group number)
Declare Function cnc_rdgrpid Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, Odb As ODBTLIFE1) As Integer

' read tool life management data(number of tool groups)
Declare Function cnc_rdngrp Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As ODBTLIFE2) As Integer

' read tool life management data(number of tools)
Declare Function cnc_rdntool Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, Odb As ODBTLIFE3) As Integer

' read tool life management data(tool life)
Declare Function cnc_rdlife Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, Odb As ODBTLIFE3) As Integer

' read tool life management data(tool lift counter)
Declare Function cnc_rdcount Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, Odb As ODBTLIFE3) As Integer

' read tool life management data(tool length number-1)
Declare Function cnc_rd1length Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, Odb As ODBTLIFE4) As Integer

' read tool life management data(tool length number-2)
Declare Function cnc_rd2length Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, Odb As ODBTLIFE4) As Integer

' read tool life management data(cutter compensation no.-1)
Declare Function cnc_rd1radius Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, Odb As ODBTLIFE4) As Integer

' read tool life management data(cutter compensation no.-2)
Declare Function cnc_rd2radius Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, Odb As ODBTLIFE4) As Integer

' read tool life management data(tool information-1)
Declare Function cnc_t1info Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, Odb As ODBTLIFE4) As Integer

' read tool life management data(tool information-2)
Declare Function cnc_t2info Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, Odb As ODBTLIFE4) As Integer

' read tool life management data(tool number)
Declare Function cnc_toolnum Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, Odb As ODBTLIFE4) As Integer

' read tool life management data(tool number, tool life, tool life counter)(area specified)
Declare Function cnc_rdtoolrng Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, ByVal c As Integer, Odb As IODBTR) As Integer

' read tool life management data(all data within group)
Declare Function cnc_rdtoolgrp Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, Odb As ODBTG) As Integer

' write tool life management data(tool life counter) (area specified)
Declare Function cnc_wrcountr Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, Idb As IDBWRC) As Integer

' read tool life management data(used tool group number)
Declare Function cnc_rdusegrpid Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As ODBUSEGR) As Integer

' read tool life management data(max. number of tool groups)
Declare Function cnc_rdmaxgrp Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As ODBLFNO) As Integer

' read tool life management data(maximum number of tool within group)
Declare Function cnc_rdmaxtool Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As ODBLFNO) As Integer

' read tool life management data(used tool no. within group)
Declare Function cnc_rdusetlno Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, ByVal c As Integer, Odb As ODBTLUSE) As Integer

' read tool life management data(tool data1)
Declare Function cnc_rd1tlifedata Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, Odb As IODBTD) As Integer

' read tool life management data(tool data2)
Declare Function cnc_rd2tlifedata Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, Odb As IODBTD) As Integer

' write tool life management data(tool data1)
Declare Function cnc_wr1tlifedata Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Idb As IODBTD) As Integer

' write tool life management data(tool data2)
Declare Function cnc_wr2tlifedata Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Idb As IODBTD) As Integer

' read tool life management data(tool group information)
Declare Function cnc_rdgrpinfo Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, ByVal c As Integer, Odb As IODBTGI) As Integer

' read tool life management data(tool group information 2)
Declare Function cnc_rdgrpinfo2 Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, ByVal c As Integer, Odb As IODBTGI2) As Integer

' read tool life management data(tool group information 3)
Declare Function cnc_rdgrpinfo3 Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, ByVal c As Integer, Odb As IODBTGI3) As Integer

' write tool life management data(tool group information)
Declare Function cnc_wrgrpinfo Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, Idb As IODBTGI) As Integer

' write tool life management data(tool group information 2)
Declare Function cnc_wrgrpinfo2 Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, Idb As IODBTGI2) As Integer

' write tool life management data(tool group information 3)
Declare Function cnc_wrgrpinfo3 Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, Idb As IODBTGI3) As Integer

' delete tool life management data(tool group)
Declare Function cnc_deltlifegrp Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer) As Integer

' insert tool life management data(tool data)
Declare Function cnc_instlifedt Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Idb As IDBITD) As Integer

' delete tool life management data(tool data)
Declare Function cnc_deltlifedt Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer) As Integer

' clear tool life management data(tool life counter, tool information)(area specified)
Declare Function cnc_clrcntinfo Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer) As Integer

' read tool life management data(tool group number) 2
Declare Function cnc_rdgrpid2 Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Long, Odb As ODBTLIFE5) As Integer

' read tool life management data(tool data1) 2
Declare Function cnc_rd1tlifedat2 Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Long, Odb As IODBTD2) As Integer

' write tool life management data(tool data1) 2
Declare Function cnc_wr1tlifedat2 Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Idb As IODBTD2) As Integer

' read tool life management data
Declare Function cnc_rdtlinfo Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As ODBTLINFO) As Integer

' read tool life management data(used tool group number)
Declare Function cnc_rdtlusegrp Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As ODBUSEGRP) As Integer

' read tool life management data(tool group information 2)
Declare Function cnc_rdtlgrp Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal lStartNo As Long, nLen As Integer, Odb As IODBTLGRP) As Integer

' read tool life management data (tool data1)
Declare Function cnc_rdtltool Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal lGrpNo As Long, ByVal lStartNo As Long, nLen As Integer, Odb As IODBTLTOOL) As Integer

' read tool life management data(exchange tool group number)
Declare Function cnc_rdexchgtgrp Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Integer, Odb As Any) As Integer

' read tool life management data(tool group information 4)
Declare Function cnc_rdgrpinfo4 Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, ByVal c As Integer, d As Integer, Odb As Any) As Integer


'-----------------------------------
' CNC: Tool management data related
'-----------------------------------
' new registration of tool management data
Declare Function cnc_regtool Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, b As Integer, Odb As IODBTLMNG) As Integer

' register new tool management data
Declare Function cnc_regtool_f2 Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, b As Integer, Odb As IODBTLMNG_F2) As Integer

' deletion of tool management data
Declare Function cnc_deltool Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, b As Integer) As Integer

' lead of tool management data
Declare Function cnc_rdtool Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, b As Integer, Odb As IODBTLMNG) As Integer

' read tool management data
Declare Function cnc_rdtool_f2 Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, b As Integer, c As IODBTLMNG_F2) As Integer

' write of tool management data
Declare Function cnc_wrtool Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, Odb As IODBTLMNG) As Integer

' write tool management data
Declare Function cnc_wrtool_f2 Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, b As IODBTLMNG_F2) As Integer

' write of individual data of tool management data
Declare Function cnc_wrtool2 Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, Odb As Any) As Integer

' new registration of magazine management data
Declare Function cnc_regmagazine Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Integer, Odb As IODBTLMAG) As Integer

' deletion of magazine management data
Declare Function cnc_delmagazine Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Integer, Odb As IODBTLMAG2) As Integer

' lead of magazine management data
Declare Function cnc_rdmagazine Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Integer, Odb As IODBTLMAG) As Integer

' Individual write of magazine management data
Declare Function cnc_wrmagazine Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, ByVal c As Integer) As Integer

' write  bigtool geom set data
Declare Function cnc_rdtoolgeom_tlm Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, b As Integer, Idb As IODBTLGEOM) As Integer

' read  bigtool geom set data
Declare Function cnc_wrtoolgeom_tlm Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, b As Integer, Odb As IODBTLGEOM) As Integer

' search  freepot
Declare Function cnc_btlfpotsrh Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, ByVal c As Integer, d As Integer) As Integer

' read  magazineproperty data */
Declare Function cnc_rdmag_property Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Integer, Odb As IODBMAGPRTY) As Integer

' write  magazineproperty data
Declare Function cnc_wrmag_property Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Integer, Idb As IODBMAGPRTY) As Integer

' delete magazineproperty data
Declare Function cnc_delmag_property Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Integer, Idb As IODBMAGPRTY2) As Integer

' read  potproperty data
Declare Function cnc_rdpot_property Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, a As Integer, Odb As IODBPOTPRTY) As Integer

' write  potproperty data
Declare Function cnc_wrpot_property Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, a As Integer, Odb As IODBPOTPRTY) As Integer

' delete potproperty data
Declare Function cnc_delpot_property Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, c As Integer) As Integer

' read extended tool geom size data
Declare Function cnc_rdtlgeomsize_ext Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, c As Integer, Odb As IODBTLGSEXT) As Integer

' write extended tool geom size data
Declare Function cnc_wrtlgeomsize_ext Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, ByVal c As Integer, d As Integer, Odb As IODBTLGSEXT) As Integer


'-------------------------------------
' CNC: Operation history data related
'-------------------------------------

' stop logging operation history data
Declare Function cnc_stopophis Lib "fwlib32.dll" (ByVal FlibHndl As Integer) As Integer

' restart logging operation history data
Declare Function cnc_startophis Lib "fwlib32.dll" (ByVal FlibHndl As Integer) As Integer

' read number of operation history data
Declare Function cnc_rdophisno Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Long) As Integer

' read operation history data
Declare Function cnc_rdophistry Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Long, ByVal b As Long, ByVal c As Long, Odb As Any) As Integer

' read operation history data
Declare Function cnc_rdophistry2 Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, b As Integer, c As Integer, Odb As Any) As Integer

' read number of alarm history data
Declare Function cnc_rdalmhisno Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Long) As Integer

' read alarm history data
Declare Function cnc_rdalmhistry Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Long, ByVal b As Long, ByVal c As Long, Odb As ODBAHIS) As Integer

' read alarm history data 2
Declare Function cnc_rdalmhistry2 Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Long, ByVal b As Long, ByVal c As Long, Odb As ODBAHIS2) As Integer

' read alarm history data 3
Declare Function cnc_rdalmhistry3 Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, ByVal c As Integer, Odb As ODBAHIS3) As Integer

' read alarm history data F30i
Declare Function cnc_rdalmhistry4 Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Long, ByVal b As Long, ByVal c As Long, Odb As ODBAHIS4) As Integer

' clear operation history data
Declare Function cnc_clearophis Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer) As Integer

' read signals related operation history
Declare Function cnc_rdhissgnl Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As IODBSIG) As Integer

' read signals related operation history 2
Declare Function cnc_rdhissgnl2 Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As IODBSIG2) As Integer

' read signals related operation history for F30i
Declare Function cnc_rdhissgnl3 Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As IODBSIG3) As Integer

' write signals related operation history
Declare Function cnc_wrhissgnl Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Idb As IODBSIG) As Integer

' write signals related operation history 2
Declare Function cnc_wrhissgnl2 Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Idb As IODBSIG2) As Integer

' write signals related operation history for F30i
Declare Function cnc_wrhissgnl3 Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Idb As IODBSIG3) As Integer

' read operation history data F30i
Declare Function cnc_rdophistry4 Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, b As Integer, c As Long, d As Any) As Integer

' read number of operater message history data
Declare Function cnc_rdomhisno Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Integer) As Integer

' read operater message history data F30i
Declare Function cnc_rdomhistry2 Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, ByVal c As Long, d As Any) As Integer

' read alarm history data F30i
Declare Function cnc_rdalmhistry5 Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, ByVal c As Long, d As Any) As Integer

' write key history to operation history data
Declare Function cnc_wrkeyhistry Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Byte) As Integer


'-------------
' CNC: Others
'-------------

' read CNC system information
Declare Function cnc_sysinfo Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As ODBSYS) As Integer

' read CNC status information
Declare Function cnc_statinfo Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As ODBST) As Integer

' read CNC status information
Declare Function cnc_statinfo2 Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As ODBST2) As Integer

' read alarm status
Declare Function cnc_alarm Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As ODBALM) As Integer

' read alarm status 2
Declare Function cnc_alarm2 Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Long) As Integer

' read alarm information
Declare Function cnc_rdalminfo Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, ByVal c As Integer, Odb As Any) As Integer

' read alarm message
Declare Function cnc_rdalmmsg Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal nType As Integer, nLen As Integer, Odb As Any) As Integer

' read alarm message
Declare Function cnc_rdalmmsg2 Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, b As Integer, Odb As Any) As Integer

' clear CNC alarm
Declare Function cnc_clralm Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal id As Integer) As Integer

' read modal data
Declare Function cnc_modal Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, Odb As Any) As Integer

' read G code
Declare Function cnc_rdgcode Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, c As Integer, Odb As ODBGCD) As Integer

' read command value
Declare Function cnc_rdcommand Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, c As Integer, Odb As ODBCMD) As Integer

' read diagnosis data
Declare Function cnc_diagnoss Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, ByVal c As Integer, Odb As Any) As Integer

' read diagnosis data(area specified)
Declare Function cnc_diagnosr Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Integer, ByVal b As Integer, c As Integer, d As Integer, Odb As Any) As Integer

' read A/D conversion data
Declare Function cnc_adcnv Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, Odb As ODBAD) As Integer

' read operator's message
Declare Function cnc_rdopmsg Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, Odb As Any) As Integer

' read operator's message2
Declare Function cnc_rdopmsg2 Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, Odb As Any) As Integer

' read operator's message3
Declare Function cnc_rdopmsg3 Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, b As Integer, Odb As Any) As Integer

' read operator message for MAPPS
Declare Function cnc_rdopmsgmps Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, b As Integer, c As Any) As Integer

' set path number(for 4 axes lathes, multi-path)
Declare Function cnc_setpath Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer) As Integer

' get path number(for 4 axes lathes, multi-path)
Declare Function cnc_getpath Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Integer, b As Integer) As Integer

' allocate library handle
Declare Function cnc_allclibhndl Lib "fwlib32.dll" (FlibHndl As Integer) As Integer

' free library handle
Declare Function cnc_freelibhndl Lib "fwlib32.dll" (ByVal FlibHndl As Integer) As Integer

' get library option
Declare Function cnc_getlibopt Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Long, b As Byte, c As Long) As Integer

' set library option
Declare Function cnc_setlibopt Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Long, b As Byte, ByVal c As Long) As Integer

' get custom macro type
Declare Function cnc_getmactype Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Integer) As Integer

' set custom macro type
Declare Function cnc_setmactype Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer) As Integer

' get P code macro type
Declare Function cnc_getpmactype Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Integer) As Integer

' set P code macro type
Declare Function cnc_setpmactype Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer) As Integer

' get screen status
Declare Function cnc_getcrntscrn Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Integer) As Integer

' change screen mode
Declare Function cnc_slctscrn Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer) As Integer

' read CNC configuration information
Declare Function cnc_sysconfig Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As ODBSYSC) As Integer

' read program restart information
Declare Function cnc_rdprstrinfo Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As ODBPRS) As Integer

' search sequence number for program restart
Declare Function cnc_rstrseqsrch Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Long, ByVal b As Long, ByVal c As Integer, ByVal d As Integer) As Integer

' search sequence number for program restart 2
Declare Function cnc_rstrseqsrch2 Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Long, ByVal b As Long, ByVal c As Integer, ByVal d As Integer, ByVal e As Long) As Integer

' read output signal image of software operator's panel
Declare Function cnc_rdopnlsgnl Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, Odb As IODBSGNL) As Integer

' write output signal of software operator's panel
Declare Function cnc_wropnlsgnl Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Idb As IODBSGNL) As Integer

' read general signal image of software operator's panel
Declare Function cnc_rdopnlgnrl Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, Odb As IODBGNRL) As Integer

' write general signal image of software operator's panel
Declare Function cnc_wropnlgnrl Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Idb As IODBGNRL) As Integer

' read general signal name of software operator's panel
Declare Function cnc_rdopnlgsname Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, Odb As IODBRDNA) As Integer

' write general signal name of software operator's panel
Declare Function cnc_wropnlgsname Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Idb As IODBRDNA) As Integer

' get detail error
Declare Function cnc_getdtailerr Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As ODBERR) As Integer

' read informations of CNC parameter
Declare Function cnc_rdparainfo Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Long, Odb As ODBPARAIF) As Integer

' read informations of CNC parameter(3)
Declare Function cnc_rdparainfo3 Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, b As Integer, c As Integer, d As Integer, Odb As ODBPARAIF2) As Integer

' read informations of CNC setting data
Declare Function cnc_rdsetinfo Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Long, Odb As ODBSETIF) As Integer

' read informations of CNC diagnose data
Declare Function cnc_rddiaginfo Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Long, Odb As ODBDIAGIF) As Integer

' read maximum, minimum and total number of CNC parameter
Declare Function cnc_rdparanum Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As ODBPARANUM) As Integer

' read maximum, minimum and total number of CNC setting data
Declare Function cnc_rdsetnum Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As ODBSETNUM) As Integer

' read maximum, minimum and total number of CNC diagnose data
Declare Function cnc_rddiagnum Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As ODBDIAGNUM) As Integer

' get maximum valid figures and number of decimal places
Declare Function cnc_getfigure Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, b As Integer, c As Integer, d As Integer) As Integer

' read F-ROM information on CNC
Declare Function cnc_rdfrominfo Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, Odb As ODBFINFO) As Integer

' start of reading F-ROM data from CNC
Declare Function cnc_fromsvstart Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As String, ByVal c As Long) As Integer

' read F-ROM data from CNC
Declare Function cnc_fromsave Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Integer, b As Byte, c As Long) As Integer

' end of reading F-ROM data from CNC
Declare Function cnc_fromsvend Lib "fwlib32.dll" (ByVal FlibHndl As Integer) As Integer

' start of writing F-ROM data to CNC
Declare Function cnc_fromldstart Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Long) As Integer

' write F-ROM data to CNC
Declare Function cnc_fromload Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Byte, b As Long) As Integer

' end of writing F-ROM data to CNC
Declare Function cnc_fromldend Lib "fwlib32.dll" (ByVal FlibHndl As Integer) As Integer

' delete F-ROM data on CNC
Declare Function cnc_fromdelete Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As String, ByVal c As Long) As Integer

' read S-RAM information on CNC
Declare Function cnc_rdsraminfo Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As ODBSINFO) As Integer

' start of reading S-RAM data from CNC
Declare Function cnc_srambkstart Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As String, ByVal b As Long) As Integer

' read S-RAM data from CNC
Declare Function cnc_srambackup Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Integer, b As Byte, c As Long) As Integer

' end of reading S-RAM data from CNC
Declare Function cnc_srambkend Lib "fwlib32.dll" (ByVal FlibHndl As Integer) As Integer

' read F-ROM information on CNC
Declare Function cnc_getfrominfo Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, b As Integer, Odb As Any) As Integer

' start of reading F-ROM data from CNC
Declare Function cnc_fromgetstart Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As String) As Integer

' read F-ROM data from CNC
Declare Function cnc_fromget Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Integer, b As Byte, c As Long) As Integer

' end of reading F-ROM data from CNC
Declare Function cnc_fromgetend Lib "fwlib32.dll" (ByVal FlibHndl As Integer) As Integer

' start of writing F-ROM data to CNC
Declare Function cnc_fromputstart Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer) As Integer

' write F-ROM data to CNC
Declare Function cnc_fromput Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Byte, b As Long) As Integer

' end of writing F-ROM data to CNC
Declare Function cnc_fromputend Lib "fwlib32.dll" (ByVal FlibHndl As Integer) As Integer

' delete F-ROM data on CNC
Declare Function cnc_fromremove Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As String) As Integer

' read S-RAM information on CNC
Declare Function cnc_getsraminfo Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As ODBSINFO) As Integer

' start of reading S-RAM data from CNC
Declare Function cnc_sramgetstart Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As String) As Integer

' start of reading S-RAM data from CNC (2)
Declare Function cnc_sramgetstart2 Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As String) As Integer

' read S-RAM data from CNC
Declare Function cnc_sramget Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Integer, b As Byte, c As Long) As Integer

' read S-RAM data from CNC (2)
Declare Function cnc_sramget2 Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Integer, b As Byte, c As Long) As Integer

' end of reading S-RAM data from CNC
Declare Function cnc_sramgetend Lib "fwlib32.dll" (ByVal FlibHndl As Integer) As Integer

' end of reading S-RAM data from CNC (2)
Declare Function cnc_sramgetend2 Lib "fwlib32.dll" (ByVal FlibHndl As Integer) As Integer

' read number of S-RAM data kind on CNC
Declare Function cnc_rdsramnum Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Integer) As Integer

' read S-RAM data address information on CNC
Declare Function cnc_rdsramaddr Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Integer, b As SRAMADDR) As Integer

' get current NC data protection information
Declare Function cnc_getlockstat Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, b As Any) As Integer

' change NC data protection status
Declare Function cnc_chgprotbit Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, b As Any, ByVal c As Long) As Integer

' transfer a file from host computer to CNC by FTP
Declare Function cnc_dtsvftpget Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As String, ByVal b As String) As Integer

' transfer a file from CNC to host computer by FTP
Declare Function cnc_dtsvftpput Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As String, ByVal b As String) As Integer

' get transfer status for FTP
Declare Function cnc_dtsvftpstat Lib "fwlib32.dll" (ByVal FlibHndl As Integer) As Integer

' read file directory in Data Server
Declare Function cnc_dtsvrdpgdir Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As String, ByVal b As Integer, Odb As ODBDSDIR) As Integer

' delete files in Data Server
Declare Function cnc_dtsvdelete Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As String) As Integer

' down load from CNC (transfer a file from CNC to MMC)
Declare Function cnc_dtsvdownload Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As String) As Integer

' up load to CNC (transfer a file from MMC to CNC)
Declare Function cnc_dtsvupload Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As String) As Integer

' close upload/download between Data Server and CNC
Declare Function cnc_dtsvcnclupdn Lib "fwlib32.dll" (ByVal FlibHndl As Integer) As Integer

' get transfer status for up/down load
Declare Function cnc_dtsvupdnstat Lib "fwlib32.dll" (ByVal FlibHndl As Integer) As Integer

' get file name for DNC operation in Data Server
Declare Function cnc_dtsvgetdncpg Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As String) As Integer

' set program number of DNC oparation to CNC
Declare Function cnc_dtsvsetdncpg Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As String) As Integer

' read setting data for Data Server
Declare Function cnc_dtsvrdset Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As IODBDSSET) As Integer

' write setting data for Data Server
Declare Function cnc_dtsvwrset Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Idb As IODBDSSET) As Integer

' check hard disk in Data Server
Declare Function cnc_dtsvchkdsk Lib "fwlib32.dll" (ByVal FlibHndl As Integer) As Integer

' format hard disk in Data Server
Declare Function cnc_dtsvhdformat Lib "fwlib32.dll" (ByVal FlibHndl As Integer) As Integer

' save interface area in Data Server
Declare Function cnc_dtsvsavecram Lib "fwlib32.dll" (ByVal FlibHndl As Integer) As Integer

' get interface area in Data Server
Declare Function cnc_dtsvrdcram Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Long, b As Long, c As Byte) As Integer

' read maintenance information for Data Server
Declare Function cnc_dtsvmntinfo Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As ODBDSMNT) As Integer

' get Data Server mode
Declare Function cnc_dtsvgetmode Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Integer) As Integer

' set Data Server mode
Declare Function cnc_dtsvsetmode Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer) As Integer

' read error message for Data Server
Declare Function cnc_dtsvrderrmsg Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As String) As Integer

' transfar file from PC to Data Server  1999.8.23
Declare Function cnc_dtsvwrfile Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As String, ByVal b As String, ByVal c As Integer) As Integer

' transfar file from Data Server to PC  1999.8.23
Declare Function cnc_dtsvrdfile Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As String, ByVal b As String, ByVal c As Integer) As Integer

' read the loop gain for each axis
Declare Function cnc_rdloopgain Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Long) As Integer

' read the actual current for each axis
Declare Function cnc_rdcurrent Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Integer) As Integer

' read the actual speed for each axis
Declare Function cnc_rdsrvspeed Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Long) As Integer

' read the operation mode
Declare Function cnc_rdopmode Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Integer) As Integer

' read the position deviation S
Declare Function cnc_rdposerrs Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Long) As Integer

' read the position deviation S1 and S2
Declare Function cnc_rdposerrs2 Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As ODBPSER) As Integer

' read the position deviation Z in the rigid tap mode
Declare Function cnc_rdposerrz Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Long) As Integer

' read the synchronous error in the synchronous control mode
Declare Function cnc_rdsynerrsy Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Long) As Integer

' read the synchronous error in the rigid tap mode
Declare Function cnc_rdsynerrrg Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Long) As Integer

' read the spindle alarm
Declare Function cnc_rdspdlalm Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Byte) As Integer

' read the control input signal
Declare Function cnc_rdctrldi Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As ODBSPDI) As Integer

' read the control output signal
Declare Function cnc_rdctrldo Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As ODBSPDO) As Integer

' read the number of controled spindle
Declare Function cnc_rdnspdl Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Integer) As Integer

' read Servo feedback multiplication data
Declare Function cnc_rdsvfeedback Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, Odb As ODBSVFBACK) As Integer

' read data from FANUC BUS
Declare Function cnc_rdfbusmem Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, ByVal c As Long, ByVal d As Long, Odb As Any) As Integer

' write data to FANUC BUS
Declare Function cnc_wrfbusmem Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, ByVal c As Long, ByVal d As Long, Odb As Any) As Integer

' read the parameter of wave diagnosis
Declare Function cnc_rdwaveprm Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As Any) As Integer

' write the parameter of wave diagnosis
Declare Function cnc_wrwaveprm Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Idb As Any) As Integer

' read the parameter of wave diagnosis 2
Declare Function cnc_rdwaveprm2 Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As Any) As Integer

' write the parameter of wave diagnosis 2
Declare Function cnc_wrwaveprm2 Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Idb As Any) As Integer

' start the sampling for wave diagnosis
Declare Function cnc_wavestart Lib "fwlib32.dll" (ByVal FlibHndl As Integer) As Integer

' stop the sampling for wave diagnosis
Declare Function cnc_wavestop Lib "fwlib32.dll" (ByVal FlibHndl As Integer) As Integer

' read the status of wave diagnosis
Declare Function cnc_wavestat Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Integer) As Integer

' read the data of wave diagnosis
Declare Function cnc_rdwavedata Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, ByVal c As Long, d As Long, Odb As Any) As Integer

' read M-code group data
Declare Function cnc_rdmgrpdata Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, b As Integer, Odb As ODBMGRP) As Integer

' write M-code group data
Declare Function cnc_wrmgrpdata Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Idb As IDBMGRP) As Integer

' read executing M-code group data
Declare Function cnc_rdexecmcode Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, b As Integer, Odb As ODBEXEM) As Integer

' read program restart M-code group data
Declare Function cnc_rdrstrmcode Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, b As Integer, Odb As ODBRSTRM) As Integer

' read processing time stamp data
Declare Function cnc_rdproctime Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As ODBPTIME) As Integer

' read program directory for processing time data
Declare Function cnc_rdprgdirtime Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Long, b As Integer, Odb As PRGDIRTM) As Integer

' read program directory 2
#If ONO8D Then
Declare Function cnc_rdprogdir2 Lib "fwlib32.dll" Alias "cnc_rdprogdir2o8" (ByVal FlibHndl As Integer, ByVal a As Integer, b As Long, c As Integer, Odb As Any) As Integer
#Else
Declare Function cnc_rdprogdir2 Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, b As Integer, c As Integer, Odb As Any) As Integer
#End If

' read program directory 3
Declare Function cnc_rdprogdir3 Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, b As Long, c As Integer, Odb As Any) As Integer

' read program directory 4
Declare Function cnc_rdprogdir4 Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Long, c As Integer, Odb As Any) As Integer

' rename NC program
Declare Function cnc_renameprog Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Long, ByVal b As Long) As Integer

' read DNC file name for DNC1, DNC2, OSI-Ethernet
Declare Function cnc_rddncfname Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As String) As Integer

' write DNC file name for DNC1, DNC2, OSI-Ethernet
Declare Function cnc_wrdncfname Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As String) As Integer

' read communication parameter for DNC1, DNC2, OSI-Ethernet
Declare Function cnc_rdcomparam Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As IODBCPRM) As Integer

' write communication parameter for DNC1, DNC2, OSI-Ethernet
Declare Function cnc_wrcomparam Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Idb As IODBCPRM) As Integer

' read log message for DNC2
Declare Function cnc_rdcomlogmsg Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As String) As Integer

' read operator message for DNC1, DNC2
Declare Function cnc_rdcomopemsg Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As String) As Integer

' read recieve message for OSI-Ethernet
Declare Function cnc_rdrcvmsg Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As String) As Integer

' read send message for OSI-Ethernet
Declare Function cnc_rdsndmsg Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As String) As Integer

' send message for OSI-Ethernet
Declare Function cnc_sendmessage Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As String) As Integer

' clear message buffer for OSI-Ethernet
Declare Function cnc_clrmsgbuff Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer) As Integer

' read message recieve status for OSI-Ethernet
Declare Function cnc_rdrcvstat Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Integer) As Integer

' read interference check
Declare Function cnc_rdintchk Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, ByVal c As Integer, ByVal d As Integer, Odb As IODBINT) As Integer

' write interference check
Declare Function cnc_wrintchk Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, Idb As IODBINT) As Integer

' read interference check information
Declare Function cnc_rdintinfo Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Integer) As Integer

' read work coordinate shift
Declare Function cnc_rdwkcdshft Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, Odb As IODBWCSF) As Integer

' write work coordinate shift
Declare Function cnc_wrwkcdshft Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, Idb As IODBWCSF) As Integer

' read work coordinate shift measure
Declare Function cnc_rdwkcdsfms Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, Odb As IODBWCSF) As Integer

' write work coordinate shift measure
Declare Function cnc_wrwkcdsfms Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, Idb As IODBWCSF) As Integer

' stop the sampling for operator message history
Declare Function cnc_stopomhis Lib "fwlib32.dll" (ByVal FlibHndl As Integer) As Integer

' start the sampling for operator message history
Declare Function cnc_startomhis Lib "fwlib32.dll" (ByVal FlibHndl As Integer) As Integer

' read operator message history information
Declare Function cnc_rdomhisinfo Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As ODBOMIF) As Integer

' read operator message history
Declare Function cnc_rdomhistry Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, b As Long, Odb As Any) As Integer

' clear operator message history
Declare Function cnc_clearomhis Lib "fwlib32.dll" (ByVal FlibHndl As Integer) As Integer

' read b-axis tool offset value(area specified)
Declare Function cnc_rdbtofsr Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, ByVal c As Integer, ByVal d As Integer, Odb As IODBBTO) As Integer

' write b-axis tool offset value(area specified)
Declare Function cnc_wrbtofsr Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, Idb As IODBBTO) As Integer

' read b-axis tool offset information
Declare Function cnc_rdbtofsinfo Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As ODBBTLINF) As Integer

' read b-axis command
Declare Function cnc_rdbaxis Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As Any) As Integer

' read current program and its pointer  and UV macro pointer
Declare Function cnc_rduvactpt Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Long, b As Long, c As Long) As Integer

' read CNC system soft series and version
Declare Function cnc_rdsyssoft Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As ODBSYSS) As Integer

' read CNC system soft series and version(2)
Declare Function cnc_rdsyssoft2 Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As ODBSYSS2) As Integer

' read CNC system soft series and version (3)
Declare Function cnc_rdsyssoft3 Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, b As Integer, c As Integer, Odb As ODBSYSS3) As Integer

' read CNC system hard info
Declare Function cnc_rdsyshard Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, b As Integer, Odb As Any) As Integer

' read CNC system soft series and version
Declare Function cnc_rdsyssoft_str Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, b As Integer, c As Integer, Odb As Any) As Integer

' read CNC system hard info
Declare Function cnc_rdsyshard_str Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, b As Integer, c As Integer, Odb As Any) As Integer

' read CNC module configuration information
Declare Function cnc_rdmdlconfig Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As ODBMDLC) As Integer

' read CNC module configuration information 2
Declare Function cnc_rdmdlconfig2 Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As RDMDLCONFIG2) As Integer

' read processing condition file (processing data)
Declare Function cnc_rdpscdproc Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, b As Integer, Odb As IODBPSCD) As Integer

' write processing condition file (processing data)
Declare Function cnc_wrpscdproc Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, b As Integer, Idb As IODBPSCD) As Integer

' read processing condition file (piercing data)
Declare Function cnc_rdpscdpirc Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, b As Integer, Odb As IODBPIRC) As Integer

' write processing condition file (piercing data)
Declare Function cnc_wrpscdpirc Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, b As Integer, Idb As IODBPIRC) As Integer

' read processing condition file (edging data)
Declare Function cnc_rdpscdedge Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, b As Integer, Odb As IODBEDGE) As Integer

' write processing condition file (edging data)
Declare Function cnc_wrpscdedge Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, b As Integer, Idb As IODBEDGE) As Integer

' read processing condition file (slope data)
Declare Function cnc_rdpscdslop Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, b As Integer, Odb As IODBSLOP) As Integer

' write processing condition file (slope data)
Declare Function cnc_wrpscdslop Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, b As Integer, Idb As IODBSLOP) As Integer

' read power controll duty data
Declare Function cnc_rdlpwrdty Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As IODBLPWDT) As Integer

' write power controll duty data
Declare Function cnc_wrlpwrdty Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Idb As IODBLPWDT) As Integer

' read laser power data
Declare Function cnc_rdlpwrdat Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As ODBLOPDT) As Integer

' read power complement
Declare Function cnc_rdlpwrcpst Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Integer) As Integer

' write power complement
Declare Function cnc_wrlpwrcpst Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer) As Integer

' read laser assist gas selection
Declare Function cnc_rdlagslt Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As IODBLAGSL) As Integer

' write laser assist gas selection
Declare Function cnc_wrlagslt Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Idb As IODBLAGSL) As Integer

' read laser assist gas flow
Declare Function cnc_rdlagst Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As IODBLAGST) As Integer

' write laser assist gas flow
Declare Function cnc_wrlagst Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Idb As IODBLAGST) As Integer

' read laser power for edge processing
Declare Function cnc_rdledgprc Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As IODBLEGPR) As Integer

' write laser power for edge processing
Declare Function cnc_wrledgprc Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Idb As IODBLEGPR) As Integer

' read laser power for piercing
Declare Function cnc_rdlprcprc Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As IODBLPCPR) As Integer

' write laser power for piercing
Declare Function cnc_wrlprcprc Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Idb As IODBLPCPR) As Integer

' read laser command data
Declare Function cnc_rdlcmddat Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As ODBLCMDT) As Integer

' read displacement
Declare Function cnc_rdldsplc Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Integer) As Integer

' write displacement
Declare Function cnc_wrldsplc Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer) As Integer

' read error for axis z
Declare Function cnc_rdlerrz Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Integer) As Integer

' read active number
Declare Function cnc_rdlactnum Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As ODBLACTN) As Integer

' read laser comment
Declare Function cnc_rdlcmmt Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As ODBLCMMT) As Integer

' read laser power select
Declare Function cnc_rdlpwrslt Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Integer) As Integer

' write laser power select
Declare Function cnc_wrlpwrslt Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer) As Integer

' read laser power controll
Declare Function cnc_rdlpwrctrl Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Integer) As Integer

' write laser power controll
Declare Function cnc_wrlpwrctrl Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer) As Integer

' read power correction factor history data
Declare Function cnc_rdpwofsthis Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Long, b As Long, Odb As Any) As Integer

' read management time
Declare Function cnc_rdmngtime Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Long, b As Long, Odb As Any) As Integer

' write management time
Declare Function cnc_wrmngtime Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Long, Odb As Any) As Integer

' read data related to electrical discharge at power correction ends
Declare Function cnc_rddischarge Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As Any) As Integer

' read alarm history data related to electrical discharg
Declare Function cnc_rddischrgalm Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Long, b As Long, Odb As Any) As Integer

' get date and time from cnc
Declare Function cnc_gettimer Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As Any) As Integer

' set date and time for cnc
Declare Function cnc_settimer Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Idb As Any) As Integer

' read timer data from cnc
Declare Function cnc_rdtimer Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, Odb As IODBTIME) As Integer

' write timer data for cnc
Declare Function cnc_wrtimer Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, Idb As IODBTIME) As Integer

' read tool control data
Declare Function cnc_rdtlctldata Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As IODBTLCTL) As Integer

' write tool control data
Declare Function cnc_wrtlctldata Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Idb As IODBTLCTL) As Integer

' read tool data
Declare Function cnc_rdtooldata Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, b As Integer, Odb As IODBTLDT) As Integer

' read tool data
Declare Function cnc_wrtooldata Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, b As Integer, Idb As IODBTLDT) As Integer

' read multi tool data
Declare Function cnc_rdmultitldt Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, b As Integer, Odb As IODBMLTTL) As Integer

' write multi tool data
Declare Function cnc_wrmultitldt Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, b As Integer, Idb As IODBMLTTL) As Integer

' read multi tap data
Declare Function cnc_rdmtapdata Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, b As Integer, Odb As IODBMTAP) As Integer

' write multi tap data
Declare Function cnc_wrmtapdata Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, b As Integer, Idb As IODBMTAP) As Integer

' read tool information
Declare Function cnc_rdtoolinfo Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As ODBPTLINF) As Integer

' read safetyzone data
Declare Function cnc_rdsafetyzone Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, b As Integer, Odb As IODBSAFE) As Integer

' write safetyzone data
Declare Function cnc_wrsafetyzone Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, b As Integer, Idb As IODBSAFE) As Integer

' read toolzone data
Declare Function cnc_rdtoolzone Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, b As Integer, Odb As IODBTLZN) As Integer

' write toolzone data
Declare Function cnc_wrtoolzone Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, b As Integer, Idb As IODBTLZN) As Integer

' read active toolzone data
Declare Function cnc_rdacttlzone Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As ODBACTTLZN) As Integer

' read setzone number
Declare Function cnc_rdsetzone Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Integer) As Integer

' write setzone number
Declare Function cnc_wrsetzone Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer) As Integer

' read multi-piece machining number
Declare Function cnc_rdmultipieceno Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Long) As Integer

' read block restart information
Declare Function cnc_rdbrstrinfo Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As ODBBRS) As Integer

' read menu switch signal
Declare Function cnc_rdmenuswitch Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Integer) As Integer

' write menu switch signal
Declare Function cnc_wrmenuswitch Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer) As Integer

' read tool radius offset for position data
Declare Function cnc_rdradofs Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As ODBROFS) As Integer

' read tool length offset for position data
Declare Function cnc_rdlenofs Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As ODBLOFS) As Integer

' read fixed cycle for position data
Declare Function cnc_rdfixcycle Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As ODBFIX) As Integer

' read coordinate rotate for position data
Declare Function cnc_rdcdrotate Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As ODBROT) As Integer

' read 3D coordinate convert for position data
Declare Function cnc_rd3dcdcnv Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As ODB3DCD) As Integer

' read programable mirror image for position data
Declare Function cnc_rdmirimage Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As ODBMIR) As Integer

' read scaling for position data
Declare Function cnc_rdscaling Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As ODBSCL) As Integer

' read 3D tool offset for position data
Declare Function cnc_rd3dtofs Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As ODB3DTO) As Integer

' read tool position offset for position data
Declare Function cnc_rdposofs Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As ODBPOFS) As Integer

' read hpcc setting data
Declare Function cnc_rdhpccset Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As IODBHPST) As Integer

' write hpcc setting data
Declare Function cnc_wrhpccset Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Idb As IODBHPST) As Integer

' hpcc data auto setting data
Declare Function cnc_hpccatset Lib "fwlib32.dll" (ByVal FlibHndl As Integer) As Integer

' read hpcc tuning data ( parameter input )
Declare Function cnc_rdhpcctupr Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As IODBHPPR) As Integer

' write hpcc tuning data ( parameter input )
Declare Function cnc_wrhpcctupr Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Idb As IODBHPPR) As Integer

' read hpcc tuning data ( acc input )
Declare Function cnc_rdhpcctuac Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As IODBHPAC) As Integer

' write hpcc tuning data ( acc input )
Declare Function cnc_wrhpcctuac Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Idb As IODBHPAC) As Integer

' hpcc data auto tuning
Declare Function cnc_hpccattune Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, b As Integer) As Integer

' read hpcc fine level
Declare Function cnc_hpccactfine Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Integer) As Integer

' select hpcc fine level
Declare Function cnc_hpccselfine Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer) As Integer

' read active fixture offset
Declare Function cnc_rdactfixofs Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, Odb As IODBZOFS) As Integer

' read fixture offset
Declare Function cnc_rdfixofs Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, ByVal c As Integer, ByVal d As Integer, Odb As IODBZOFS) As Integer

' write fixture offset
Declare Function cnc_wrfixofs Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, Idb As IODBZOFS) As Integer

' read tip of tool for 3D handle
Declare Function cnc_rd3dtooltip Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As Any) As Integer

' read pulse for 3D handle
Declare Function cnc_rd3dpulse Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As Any) As Integer

' read move overrlap of tool for 3D handle
Declare Function cnc_rd3dmovrlap Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As Any) As Integer

' read change offset for 3D handle
Declare Function cnc_rd3dofschg Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Long) As Integer

' clear pulse and change offset for 3D handle
Declare Function cnc_clr3dplsmov Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer) As Integer

' cycle start
Declare Function cnc_start Lib "fwlib32.dll" (ByVal FlibHndl As Integer) As Integer

' reset CNC
Declare Function cnc_reset Lib "fwlib32.dll" (ByVal FlibHndl As Integer) As Integer

' reset CNC 2
Declare Function cnc_reset2 Lib "fwlib32.dll" (ByVal FlibHndl As Integer) As Integer

' read axis name CNC
Declare Function cnc_rdaxisname Lib "fwlib32.dll" (ByVal FlibHndl As Integer, nLen As Integer, Odb As Any) As Integer

' read spindle name
Declare Function cnc_rdspdlname Lib "fwlib32.dll" (ByVal FlibHndl As Integer, nLen As Integer, Odb As Any) As Integer

' read spindle name (m)
Declare Function cnc_rdspdlnamem Lib "fwlib32.dll" (ByVal FlibHndl As Integer, nLen As Integer, Odb As Any) As Integer

' read extended axis name
Declare Function cnc_exaxisname Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, b As Integer, Odb As Any) As Integer
Declare Function cnc_exaxisname2 Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, c As Integer, Odb As Any) As Integer

' read SRAM variable area for C language executor
Declare Function cnc_rdcexesram Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Long, b As Byte, c As Long) As Integer

' write SRAM variable area for C language executor
Declare Function cnc_wrcexesram Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Long, b As Byte, c As Long) As Integer

' read maximum size and linear address of SRAM variable area for C language executor
Declare Function cnc_cexesraminfo Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Integer, b As Long, c As Long) As Integer

' read maximum size of SRAM variable area for C language executor
Declare Function cnc_cexesramsize Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Long) As Integer

' read additional workpiece coordinate systems number
Declare Function cnc_rdcoordnum Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Integer) As Integer

' converts from FANUC code to Shift JIS code
Declare Function cnc_ftosjis Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Byte, b As Byte) As Integer

' Display of optional message
Declare Function cnc_dispoptmsg Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Byte) As Integer

' Reading of answer for optional message display
Declare Function cnc_optmsgans Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Integer) As Integer

' Get CNC Model
Declare Function cnc_getcncmodel Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Integer) As Integer

' read number of repeats
Declare Function cnc_rdrepeatval Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Long) As Integer

' read number of repeats(2)
Declare Function cnc_rdrepeatval_ext Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Long, b As Long) As Integer

' read machine specific maintenance item
Declare Function cnc_rdpm_mcnitem Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, b As Integer, Odb As Any) As Integer

' write machine specific maintenance item
Declare Function cnc_wrpm_mcnitem Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, Idb As Any) As Integer

' read cnc maintenance item
Declare Function cnc_rdpm_cncitem Lib "fwlib32.dll" (ByVal FlibHndl%, ByVal a As Integer, ByVal b%, Odb As Any) As Integer

' read maintenance item status
Declare Function cnc_rdpm_item Lib "fwlib32.dll" (ByVal FlibHndl%, ByVal a As Integer, ByVal b%, Odb As Any) As Integer

' write maintenance item status
Declare Function cnc_wrpm_item Lib "fwlib32.dll" (ByVal FlibHndl%, ByVal a As Integer, ByVal b As Integer, ByVal c As Integer, Idb As Any) As Integer

' read CNC system path information
Declare Function cnc_sysinfo_ex Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As ODBSYSEX) As Integer

' read digit of program number
Declare Function cnc_progdigit Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Integer) As Integer

' Get CNC display language
Declare Function cnc_getlanguage Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Integer) As Integer

'-----
' CNC
'-----

Declare Function cnc_srcsrsvchnl Lib "fwlib32.dll" (ByVal FlibHndl As Integer) As Integer

Declare Function cnc_srcsrdidinfo Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Long, ByVal b As Integer, ByVal c As Integer, Odb As IODBIDINF) As Integer

Declare Function cnc_srcswridinfo Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Idb As IODBIDINF) As Integer

Declare Function cnc_srcsstartrd Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Long, ByVal b As Integer) As Integer

Declare Function cnc_srcsstartwrt Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Long, ByVal b As Integer) As Integer

Declare Function cnc_srcsstopexec Lib "fwlib32.dll" (ByVal FlibHndl As Integer) As Integer

Declare Function cnc_srcsrdexstat Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As ODBSRCSST) As Integer

Declare Function cnc_srcsrdopdata Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Long, b As Long, Odb As Any) As Integer

Declare Function cnc_srcswropdata Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Long, ByVal b As Long, Idb As Any) As Integer

Declare Function cnc_srcsfreechnl Lib "fwlib32.dll" (ByVal FlibHndl As Integer) As Integer

Declare Function cnc_srcsrdlayout Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As ODBSRCSLYT) As Integer

Declare Function cnc_srcsrddrvcp Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Integer) As Integer


'---------------------------
' CNC : Graphic command data
'---------------------------

' Start drawing position
Declare Function cnc_startdrawpos Lib "fwlib32.dll" (ByVal FlibHndl As Integer) As Integer

' Stop drawing position
Declare Function cnc_stopdrawpos Lib "fwlib32.dll" (ByVal FlibHndl As Integer) As Integer

' Start dynamic graphic
Declare Function cnc_startdyngrph Lib "fwlib32.dll" (ByVal FlibHndl As Integer) As Integer

' Stop dynamic graphic
Declare Function cnc_stopdyngrph Lib "fwlib32.dll" (ByVal FlibHndl As Integer) As Integer

' Read graphic command data
Declare Function cnc_rdgrphcmd Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Integer, b As Integer) As Integer

' Update graphic command read pointer
Declare Function cnc_wrgrphcmdptr Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer) As Integer
' Read cancel flag
Declare Function cnc_rdgrphcanflg Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Integer) As Integer

' Clear graphic command
Declare Function cnc_clrgrphcmd Lib "fwlib32.dll" (ByVal FlibHndl As Integer) As Integer



'---------------------------
' CNC : Servo learning data
'---------------------------

' Servo learning data read start
Declare Function cnc_svdtstartrd Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer) As Integer

' Servo learning data write start
Declare Function cnc_svdtstartwr Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer) As Integer

' Servo learning data read end
Declare Function cnc_svdtendrd Lib "fwlib32.dll" (ByVal FlibHndl As Integer) As Integer

' Servo learning data write end
Declare Function cnc_svdtendwr Lib "fwlib32.dll" (ByVal FlibHndl As Integer) As Integer

' Servo learning data read/write stop
Declare Function cnc_svdtstopexec Lib "fwlib32.dll" (ByVal FlibHndl As Integer) As Integer

' Servo learning data read from I/F buffer
Declare Function cnc_svdtrddata Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Integer, b As Long, Odb As Any) As Integer

' Servo learning data write from I/F buffer
Declare Function cnc_svdtwrdata Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Integer, b As Long, Odb As Any) As Integer


'-----
' CNC
'-----
Declare Function cnc_sfbsetchnl Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Long, Odb As Any) As Integer

Declare Function cnc_sfbclrchnl Lib "fwlib32.dll" (ByVal FlibHndl As Integer) As Integer

Declare Function cnc_sfbstartsmpl Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Long) As Integer

Declare Function cnc_sfbcancelsmpl Lib "fwlib32.dll" (ByVal FlibHndl As Integer) As Integer

Declare Function cnc_sfbreadsmpl Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Integer, ByVal b As Long, Odb As Any) As Integer

Declare Function cnc_sfbendsmpl Lib "fwlib32.dll" (ByVal FlibHndl As Integer) As Integer

Declare Function cnc_sdtsetchnl Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Long, Odb As Any) As Integer

Declare Function cnc_sdtclrchnl Lib "fwlib32.dll" (ByVal FlibHndl As Integer) As Integer

Declare Function cnc_sdtstartsmpl Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Long) As Integer

Declare Function cnc_sdtcancelsmpl Lib "fwlib32.dll" (ByVal FlibHndl As Integer) As Integer

Declare Function cnc_sdtreadsmpl Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Integer, ByVal b As Long, Odb As Any) As Integer

Declare Function cnc_sdtendsmpl Lib "fwlib32.dll" (ByVal FlibHndl As Integer) As Integer

Declare Function cnc_sdtread1shot Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Integer) As Integer


'---------------------------
' CNC : NC display function
'---------------------------

' Start NC display
Declare Function cnc_startnccmd Lib "fwlib32.dll" (ByVal FlibHndl As Integer) As Integer

' Start NC display (2)
Declare Function cnc_startnccmd2 Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As String) As Integer

' Stop NC display
Declare Function cnc_stopnccmd Lib "fwlib32.dll" (ByVal FlibHndl As Integer) As Integer

' Get NC display mode
Declare Function cnc_getdspmode Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Integer) As Integer

' Set current display screen
Declare Function cnc_setcurscrn Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer) As Integer

' Change status of SDF
Declare Function cnc_sdfstatchg Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, ByVal c As Long, ByVal d As Long) As Integer

' Set manager application for SDF
Declare Function cnc_sdfmnghwnd Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Long, ByVal c As Long) As Integer


'-----------------------------------
' CNC : Remote diagnostics function
'-----------------------------------

' Start remote diagnostics function
Declare Function cnc_startrmtdgn Lib "fwlib32.dll" (ByVal FlibHndl As Integer) As Integer

' Stop remote diagnostics function
Declare Function cnc_stoprmtdgn Lib "fwlib32.dll" (ByVal FlibHndl As Integer) As Integer

' Read data from remote diagnostics I/F
Declare Function cnc_rdrmtdgn Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Long, ByVal b As String) As Integer

' Write data to remote diagnostics I/F
Declare Function cnc_wrrmtdgn Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Long, ByVal b As String) As Integer

' Set CommStatus of remote diagnostics I/F area
Declare Function cnc_wrcommstatus Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer) As Integer

' Check remote diagnostics I/F
Declare Function cnc_chkrmtdgn Lib "fwlib32.dll" (ByVal FlibHndl As Integer) As Integer

'------------------------
' CNC : FS18-LN function
'------------------------

' read allowance
Declare Function cnc_allowance Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, Odb As ODBAXIS) As Integer

' read allowanced state
Declare Function cnc_allowcnd Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, Odb As ODBCAXIS) As Integer

' set work zero
Declare Function cnc_workzero Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, Idb As IODBZOFS) As Integer

' set slide position
Declare Function cnc_slide Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, Odb As ODBAXIS) As Integer


#If PMD Then        ' only Power Mate D/H
'------------------------------
' MAXIS: Axis Movement Control
'------------------------------

' cnc_opdi:signal operation command
Declare Function cnc_opdi Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, Odb As Any) As Integer

' cnc_refpoint:reference point return
Declare Function cnc_refpoint Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, ByVal c As Integer, Odb As ODBEXEC) As Integer

' cnc_abspoint:absolute movement
Declare Function cnc_abspoint Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, ByVal c As Integer, ByVal d As Long, Odb1 As ODBPOS, Odb2 As ODBEXEC) As Integer

' cnc_incpoint:incremental movement
Declare Function cnc_incpoint Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, ByVal c As Integer, ByVal d As Long, Odb1 As ODBPOS, Odb2 As ODBEXEC) As Integer

' cnc_dwell:dwell
Declare Function cnc_dwell Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, ByVal c As Integer, Odb1 As ODBPOS, Odb2 As ODBEXEC) As Integer

' cnc_coordre:coordinate establihment
Declare Function cnc_coordre Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, ByVal c As Integer, Odb1 As ODBPOS, Odb2 As ODBEXEC) As Integer

' cnc_exebufstat:reading of the executive buffer condition
Declare Function cnc_exebufstat Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As ODBEXEC) As Integer

' cnc_finstate:Reading of the execution completion condition
Declare Function cnc_finstate Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As ODBFIN) As Integer

' cnc_setfin:Release of the reading mode of the execution completion condition
Declare Function cnc_setfin Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As ODBFIN) As Integer
#End If


'-----
' PMC
'-----

' read message from PMC to MMC
Declare Function pmc_rdmsg Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Integer, b As Integer) As Integer

' write message from MMC to PMC
Declare Function pmc_wrmsg Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, b As Integer) As Integer

' read message from PMC to MMC(conditional)
Declare Function pmc_crdmsg Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Integer, b As Integer) As Integer

' write message from MMC to PMC(conditional)
Declare Function pmc_cwrmsg Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, b As Integer) As Integer

' read PMC data(area specified)
Declare Function pmc_rdpmcrng Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, ByVal c As Long, ByVal d As Long, ByVal e As Long, Odb As Any) As Integer

' write PMC data(area specified)
Declare Function pmc_wrpmcrng Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Long, Idb As Any) As Integer

' read data from extended backup memory
Declare Function pmc_rdkpm Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Long, b As Byte, ByVal c As Long) As Integer

' write data to extended backup memory
Declare Function pmc_wrkpm Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Long, b As Byte, ByVal c As Long) As Integer

' read data from extended backup memory 2
Declare Function pmc_rdkpm2 Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Long, b As Byte, ByVal c As Long) As Integer

' write data to extended backup memory 2
Declare Function pmc_wrkpm2 Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Long, b As Byte, ByVal c As Long) As Integer

' read maximum size of extended backup memory
Declare Function pmc_kpmsiz Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Long) As Integer

' read informations of PMC data
Declare Function pmc_rdpmcinfo Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, Odb As ODBPMCINF) As Integer

' read PMC parameter data table contorol data
Declare Function pmc_rdcntldata Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, ByVal c As Integer, Odb As IODBPMCCNTL) As Integer

' write PMC parameter data table contorol data
Declare Function pmc_wrcntldata Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, Idb As IODBPMCCNTL) As Integer

' read PMC parameter data table contorol data group number
Declare Function pmc_rdcntlgrp Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Integer) As Integer

' write PMC parameter data table contorol data group number
Declare Function pmc_wrcntlgrp Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer) As Integer

' read PMC alarm message
Declare Function pmc_rdalmmsg Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, b As Integer, c As Integer, Odb As Any) As Integer

' get detail error for pmc
Declare Function pmc_getdtailerr Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As ODBPMCERR) As Integer

' read PMC memory data
Declare Function pmc_rdpmcmem Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Long, ByVal c As Long, Odb As Any) As Integer

' write PMC memory data
Declare Function pmc_wrpmcmem Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Long, ByVal c As Long, Idb As Any) As Integer

' read PMC-SE memory data
Declare Function pmc_rdpmcsemem Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Long, ByVal c As Long, Odb As Any) As Integer

' write PMC-SE memory data
Declare Function pmc_wrpmcsemem Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Long, ByVal c As Long, Idb As Any) As Integer

' select PMC unit
Declare Function pmc_select_pmc_unit Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Long) As Integer

' get current PMC unit
Declare Function pmc_get_current_pmc_unit Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Long) As Integer

' get number of PMC
Declare Function pmc_get_number_of_pmc Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Long) As Integer

' get PMC unit types
Declare Function pmc_get_pmc_unit_types Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Long, b As Long) As Integer

' read pmc title data
Declare Function pmc_rdpmctitle Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As ODBPMCTITLE) As Integer

' read PMC parameter start
Declare Function pmc_rdprmstart Lib "fwlib32.dll" (ByVal FlibHndl As Integer) As Integer

' read PMC parameter
Declare Function pmc_rdpmcparam Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Long, b As Byte) As Integer

' read PMC parameter end
Declare Function pmc_rdprmend Lib "fwlib32.dll" (ByVal FlibHndl As Integer) As Integer

' write PMC parameter start
Declare Function pmc_wrprmstart Lib "fwlib32.dll" (ByVal FlibHndl As Integer) As Integer

' write PMC parameter
Declare Function pmc_wrpmcparam Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Long, b As Byte) As Integer

' write PMC parameter end
Declare Function pmc_wrprmend Lib "fwlib32.dll" (ByVal FlibHndl As Integer) As Integer

' set PMC Timer type
Declare Function pmc_set_timer_type Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal b As Integer, ByVal c As Integer, d As Integer) As Integer

' get PMC Timer type
Declare Function pmc_get_timer_type Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal b As Integer, ByVal c As Integer, d As Integer) As Integer

' Reads sequence program and momory type
Declare Function pmc_read_seq_program_and_memory_type Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As ODBPMCLADMEMTYPE) As Integer


'----------------------------
' PMC : PROFIBUS function
'----------------------------

' read PROFIBUS configration data
Declare Function pmc_prfrdconfig Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As ODBPRFCNF) As Integer

' read bus parameter for master function
Declare Function pmc_prfrdbusprm Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As IODBBUSPRM) As Integer

' write bus parameter for master function
Declare Function pmc_prfwrbusprm Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Idb As IODBBUSPRM) As Integer

' read slave parameter for master function
Declare Function pmc_prfrdslvprm Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, Odb As Any) As Integer

' write slave parameter for master function
Declare Function pmc_prfwrslvprm Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, Idb As Any) As Integer

' read allocation address for master function
Declare Function pmc_prfrdallcadr Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, Odb As IODBPRFADR) As Integer

' set allocation address for master function
Declare Function pmc_prfwrallcadr Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, Idb As IODBPRFADR) As Integer

' read allocation address for slave function
Declare Function pmc_prfrdslvaddr Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As IODBSLVADR) As Integer

' set allocation address for slave function
Declare Function pmc_prfwrslvaddr Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Idb As IODBSLVADR) As Integer

' read status for slave function
Declare Function pmc_prfrdslvstat Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As ODBSLVST) As Integer

' Reads slave index data of master function
Declare Function pmc_prfrdslvid Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, b As IODBSLVID) As Integer

' Writes slave index data of master function
Declare Function pmc_prfwrslvid Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, b As IODBSLVID) As Integer

' Reads slave parameter of master function(2)
Declare Function pmc_prfrdslvprm2 Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, b As IODBSLVPRM3) As Integer

' Writes slave parameter of master function(2)
Declare Function pmc_prfwrslvprm2 Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, b As IODBSLVPRM3) As Integer

' Reads DI/DO parameter of master function
Declare Function pmc_prfrddido Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, b As IODBDIDO) As Integer

' Writes DI/DO parameter of master function
Declare Function pmc_prfwrdido Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, b As IODBDIDO) As Integer

' Reads indication address of master function
Declare Function pmc_prfrdindiadr Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As IODBINDEADR) As Integer

' Writes indication address of master function
Declare Function pmc_prfwrindiadr Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As IODBINDEADR) As Integer

' Reads operation mode of master function
Declare Function pmc_prfrdopmode Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Integer) As Integer

' Writes operation mode of master function
Declare Function pmc_prfwropmode Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, b As Integer) As Integer


'----------------------------
' Customer's Board function
'----------------------------

Declare Function cnc_rdcbmem Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Long, ByVal b As Long, c As Byte) As Integer

Declare Function cnc_wrcbmem Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Long, ByVal b As Long, c As Byte) As Integer

'-----------------------------------
' CB : CUSTOMER'S BOARD function
'-----------------------------------

' start downloading Customer's board data
Declare Function cb_dwnstart Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Long) As Integer

' download Customer's board data
Declare Function cb_download Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Long, b As Byte) As Integer

' end of downloading Customer's board data
Declare Function cb_dwnend Lib "fwlib32.dll" (ByVal FlibHndl As Integer) As Integer

' start uploading Customer's board data
Declare Function cb_upstart Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Integer, b As Long) As Integer

' upload Customer's board data
Declare Function cb_upload Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Long, b As Byte) As Integer

' end of uploading Customer's board data
Declare Function cb_upend Lib "fwlib32.dll" (ByVal FlibHndl As Integer) As Integer

' get transfer informations
Declare Function cb_transinfo Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As ODBTRANSINFO) As Integer


'-----------------------------------------------
' DS : Data server & Ethernet board function
'-----------------------------------------------

' read the parameter of the Ethernet board
Declare Function etb_rdparam Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, Odb As Any) As Integer

' write the parameter of the Ethernet board
Declare Function etb_wrparam Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As Any) As Integer

' read the error message of the Ethernet board
Declare Function etb_rderrmsg Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, Odb As ODBETMSG) As Integer

' read the mode of the Data Server
Declare Function ds_rdmode Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Integer) As Integer

' write the mode of the Data Server
Declare Function ds_wrmode Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer) As Integer

' read information of the Data Server's HDD
Declare Function ds_rdhddinfo Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As ODBHDDINF) As Integer

' read the file list of the Data Server's HDD
Declare Function ds_rdhdddir Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Byte, ByVal b As Long, c As Integer, Odb As Any) As Integer

' delete the file of the Data Serve's HDD
Declare Function ds_delhddfile Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Byte) As Integer

' copy the file of the Data Server's HDD
Declare Function ds_copyhddfile Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Byte, b As Byte) As Integer

' change the file name of the Data Server's HDD
Declare Function ds_renhddfile Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Byte, b As Byte) As Integer

' execute the PUT command of the FTP
Declare Function ds_puthddfile Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Byte, b As Byte) As Integer

' execute the MPUT command of the FTP
Declare Function ds_mputhddfile Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Byte) As Integer

' read information of the host
Declare Function ds_rdhostinfo Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Long, ByVal b As Long) As Integer

' read the file list of the host
Declare Function ds_rdhostdir Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Long, c As Integer, Odb As Any, ByVal e As Long) As Integer

' read the file list of the host 2
Declare Function ds_rdhostdir2 Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Long, c As Integer, d As Long, Odb As Any, ByVal f As Long) As Integer

' delete the file of the host
Declare Function ds_delhostfile Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Byte, ByVal b As Long) As Integer

' execute the GET command of the FTP
Declare Function ds_gethostfile Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Byte, b As Byte) As Integer

' execute the MGET command of the FTP
Declare Function ds_mgethostfile Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Byte) As Integer

' read the execution result
Declare Function ds_rdresult Lib "fwlib32.dll" (ByVal FlibHndl As Integer) As Integer

' stop the execution of the command
Declare Function ds_cancel Lib "fwlib32.dll" (ByVal FlibHndl As Integer) As Integer

' read the file from the Data Server
Declare Function ds_rdncfile Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, b As Byte) As Integer

' read the file from the Data Server 2
Declare Function ds_rdncfile2 Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Byte) As Integer

' write the file to the Data Server
Declare Function ds_wrncfile Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Long) As Integer

' read the file name for the DNC operation in the Data Server's HDD
Declare Function ds_rddnchddfile Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Byte) As Integer

' write the file name for the DNC operation in the Data Server's HDD
Declare Function ds_wrdnchddfile Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Byte) As Integer

' read the file name for the DNC operation in the host
Declare Function ds_rddnchostfile Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Byte, b As Byte) As Integer

' write the file name for the DNC operation in the host
Declare Function ds_wrdnchostfile Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Byte) As Integer

' read the connecting host number
Declare Function ds_rdhostno Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Integer) As Integer

' read maintenance information
Declare Function ds_rdmntinfo Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, Odb As DSMNTINFO) As Integer

' check the Data Server's HDD
Declare Function ds_checkhdd Lib "fwlib32.dll" (ByVal FlibHndl As Integer) As Integer

' format the Data Server's HDD
Declare Function ds_formathdd Lib "fwlib32.dll" (ByVal FlibHndl As Integer) As Integer

' create the directory in the Data Server's HDD
Declare Function ds_makehdddir Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Byte) As Integer

' delete directory in the Data Server's HDD
Declare Function ds_delhdddir Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Byte) As Integer

' change the current directory
Declare Function ds_chghdddir Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Byte) As Integer

' execute the PUT command according to the list file
Declare Function ds_lputhddfile Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Byte) As Integer

' delete files according to the list file
Declare Function ds_ldelhddfile Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Byte) As Integer

' execute the GET command according to the list file
Declare Function ds_lgethostfile Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Byte) As Integer

' read the directory for M198 operation
Declare Function ds_rdm198hdddir Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Byte) As Integer

' write the directory for M198 operation
Declare Function ds_wrm198hdddir Lib "fwlib32.dll" (ByVal FlibHndl As Integer) As Integer

' read the connecting host number for the M198 operation
Declare Function ds_rdm198host Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Integer) As Integer

' write the connecting host number for the M198 operation
Declare Function ds_wrm198host Lib "fwlib32.dll" (ByVal FlibHndl As Integer) As Integer

' write the connecting host number
Declare Function ds_wrhostno Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer) As Integer

' search string in data server program
Declare Function ds_searchword Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Byte) As Integer

' read the searching result
Declare Function ds_searchresult Lib "fwlib32.dll" (ByVal FlibHndl As Integer) As Integer

' read file in the Data Server's HDD
Declare Function ds_rdfile Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Byte, b As Byte) As Integer

' write file in the Data Server's HDD
Declare Function ds_wrfile Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Byte, b As Byte) As Integer

' start downloading Data Server data
Declare Function ds_dwnstart Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As String, b As Integer) As Integer

' download Data Server data
Declare Function ds_download Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Long, ByVal b As String) As Integer

' end of downloading Data Server data
Declare Function ds_dwnend Lib "fwlib32.dll" (ByVal FlibHndl As Integer) As Integer

'----------------------------
' NET : Ethernet function
'----------------------------
' read parameter for ethernet function
Declare Function eth_rdparam Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, Odb As Any) As Integer

' write parameter for ethernet function
Declare Function eth_wrparam Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, Idb1 As Any, Idb2 As Any) As Integer

' read mode for dataserver function
Declare Function eth_rddsmode Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As OUT_ETHDSMODE) As Integer

' write mode for dataserver function
Declare Function eth_wrdsmode Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer) As Integer

' read state for dataserver function
Declare Function eth_rddsstate Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, Odb As OUT_DSSTATE) As Integer

' read host number for ethernet function
Declare Function eth_rdhost Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, b As Integer) As Integer

' write host number for ethernet function
Declare Function eth_wrhost Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer) As Integer

' read m198 folder for dataserver function
Declare Function eth_rddsm198dir Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As String) As Integer

' write m198 folder for dataserver function
Declare Function eth_wrdsm198dir Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As String) As Integer

' read m198 host number for dataserver function
Declare Function eth_rddsm198host Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, b As Integer, ByVal b As String) As Integer

' write m198 host number for dataserver function
Declare Function eth_wrdsm198host Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, ByVal c As String) As Integer

' read ATA card format type
Declare Function eth_rddsformat Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Integer) As Integer

' format ATA card
Declare Function eth_dsformat Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, b As Integer) As Integer

' checkdisk ATA card
Declare Function eth_dschkdsk Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Integer) As Integer

' get DNC operation file
Declare Function cnc_rddsdncfile Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As String, b As Integer, ByVal c As String) As Integer

' set DNC operation file
Declare Function cnc_wrdsdncfile Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As String, ByVal b As String) As Integer

' get device infomation for dataserver function
Declare Function cnc_rddsdevinfo Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, Odb As ODBPDFINF) As Integer

' change operation folder
Declare Function cnc_rddsdir Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As String, b As Integer, ByVal c As String) As Integer

' get file list infomation
Declare Function cnc_rddsfile Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As String, Idb As IN_DSFILE, Odb1 As OUT_DSINFO, Odb2 As Any) As Integer

' make folder
Declare Function cnc_dsmkdir Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As String, ByVal b As String) As Integer

' delete folder
Declare Function cnc_dsrmdir Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As String, ByVal b As String) As Integer

' delete file
Declare Function cnc_dsremove Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As String, ByVal b As String) As Integer

' change current folder
Declare Function cnc_dschdir Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As String, ByVal b As String, Idb As IN_DSFILE, Odb1 As OUT_DSINFO, Odb2 As Any) As Integer

' rename folder / file
Declare Function cnc_dsrename Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As String, ByVal b As String, ByVal c As String) As Integer

' copy file for dataserver function
Declare Function cnc_dscopyfile Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As String, ByVal b As String) As Integer

' start GET command for dataserver function
Declare Function cnc_dsget_req Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As String, ByVal b As String, ByVal c As Integer) As Integer

' start PUT command for dataserver function
Declare Function cnc_dsput_req Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As String, ByVal b As String) As Integer

' start MGET command for dataserver function
Declare Function cnc_dsmget_req Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As String) As Integer

' start MPUT command for dataserver function
Declare Function cnc_dsmput_req Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As String) As Integer

' start List-GET command for dataserver function
Declare Function cnc_dslistget_req Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As String) As Integer

' start List-PUT command for dataserver function
Declare Function cnc_dslistput_req Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As String) As Integer

' start List delete for dataserver function
Declare Function cnc_dslistdel_req Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As String) As Integer

' read file transport result for dataserver function
Declare Function cnc_dsftpstat Lib "fwlib32.dll" (ByVal FlibHndl As Integer) As Integer

' cancel to file transport for dataserver function
Declare Function cnc_dsftpcancel Lib "fwlib32.dll" (ByVal FlibHndl As Integer) As Integer

'----------------------------
' NET : PROFIBUS function
'----------------------------

' read parameter for PROFIBUS MASTER function
Declare Function pbm_rd_param Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, Idb As T_SLVSLT_IND, Odb As Any) As Integer

' write parameter for PROFIBUS MASTER function
Declare Function pbm_wr_param Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, Idb1 As Any, Idb2 As Any) As Integer

' initialize parameter for PROFIBUS MASTER function
Declare Function pbm_ini_prm Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, Idb As T_SLVSLT_IND) As Integer

' read slave index table for PROFIBUS MASTER function
Declare Function pbm_rd_allslvtbl Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As OUT_ALLSLVTBL) As Integer

' execute sub function for PROFIBUS MASTER function
Declare Function pbm_exe_subfunc Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, Idb As T_SLVSLT_IND) As Integer

' read sub parameter for PROFIBUS MASTER function
Declare Function pbm_rd_subprm Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, Idb As T_SLVSLT_IND, Odb As Any) As Integer

' read error code for PROFIBUS MASTER function
Declare Function pbm_rd_errcode Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As T_ERR_CODE) As Integer

' change mode for PROFIBUS MASTER function
Declare Function pbm_chg_mode Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Byte, Odb As OUT_CHGMODERESULT) As Integer

' read communication information for PROFIBUS MASTER function
Declare Function pbm_rd_cominfo Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, Odb As Any) As Integer

' read node information table for PROFIBUS MASTER function
Declare Function pbm_rd_nodetable Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, b As Byte) As Integer

' read node information for PROFIBUS MASTER function
Declare Function pbm_rd_nodeinfo Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, Odb As OUT_PBMNODEINFO) As Integer

' read slot number for PROFIBUS MASTER function
Declare Function pbm_rd_slot Lib "fwlib32.dll" (ByVal FlibHndl As Integer, a As Integer) As Integer

' read slot information for PROFIBUS MASTER function
Declare Function pbm_rd_slotinfo Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, Odb As OUT_PBMSLOTINFO) As Integer

' read parameter for PROFIBUS SLAVE fucntion
Declare Function pbs_rd_param Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As IN_OUT_PBSPRM) As Integer

'j write parameter for PROFIBUS SLAVE function
Declare Function pbs_wr_param Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Idb1 As IN_PBSPRMFLG, Idb2 As IN_OUT_PBSPRM) As Integer

'j initialize parameter for PROFIBUS SLAVE function
Declare Function pbs_ini_prm Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal a As Integer) As Integer

'j read communication information for PROFIBUS SLAVE function
Declare Function pbs_rd_cominfo Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As OUT_PBSSTATUS) As Integer

' read parameter 2 for PROFIBUS SLAVE fucntion
Declare Function pbs_rd_param2 Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As IN_OUT_PBSPRM2) As Integer

'j write parameter 2 for PROFIBUS SLAVE function
Declare Function pbs_wr_param2 Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Idb1 As IN_PBSPRMFLG2, Idb2 As IN_OUT_PBSPRM2) As Integer

'j read communication information 2 for PROFIBUS SLAVE function
Declare Function pbs_rd_cominfo2 Lib "fwlib32.dll" (ByVal FlibHndl As Integer, Odb As OUT_PBSSTATUS2) As Integer

'--------------------------
' HSSB multiple connection
'--------------------------

' read number of node
Declare Function cnc_rdnodenum Lib "fwlib32.dll" (a As Long) As Integer

' read node informations
Declare Function cnc_rdnodeinfo Lib "fwlib32.dll" (ByVal a As Long, Odb As ODBNODE) As Integer

' set default node number
Declare Function cnc_setdefnode Lib "fwlib32.dll" (ByVal a As Long) As Integer

' allocate library handle 2
Declare Function cnc_allclibhndl2 Lib "fwlib32.dll" (ByVal a As Long, FlibHndl As Integer) As Integer

'---------------------
' Ethernet connection
'---------------------

' allocate library handle 3
Declare Function cnc_allclibhndl3 Lib "fwlib32.dll" (ByVal sIPaddr As String, ByVal nPort As Integer, ByVal nTimeout As Long, FlibHndl As Integer) As Integer

' set timeout for socket
Declare Function cnc_settimeout Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal nTimeout As Long) As Integer

' reset all socket connection
Declare Function cnc_resetconnect Lib "fwlib32.dll" (ByVal FlibHndl As Integer) As Integer

' get option state for FOCAS1/Ethernet
Declare Function cnc_getfocas1opt Lib "fwlib32.dll" (ByVal FlibHndl As Integer, ByVal nLen As Integer, nOpt As Long) As Integer

