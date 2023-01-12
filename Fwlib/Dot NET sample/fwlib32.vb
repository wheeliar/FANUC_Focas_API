'-------------------------------------------------------------------
' fwlib32.vb                                                        
'                                                                   
' CNC/PMC Data Window Library for FOCAS1/Ethernet                   
'                                                                   
' Copyright (C) 2003-2011 by FANUC CORPORATION All rights reserved. 
'                                                                   
'-------------------------------------------------------------------
Imports System
Imports System.Runtime.InteropServices


Public class Focas1
    ' Axis define
#If FS30D Then
    Public Const MAX_AXIS = 32
#Else
#If M_AXIS2 Then
    Public Const MAX_AXIS = 24
#Else
#If FS15D Then
    Public Const MAX_AXIS = 10
#Else
    Public Const MAX_AXIS = 8
#End If
#End If
#End If

    Public Const ALL_AXES     = (-1)
    Public Const ALL_SPINDLES = (-1)

    ' Error Codes 
    Public Const EW_PROTOCOL =     (-17)           ' protocol error 
    Public Const EW_SOCKET   =     (-16)           ' Windows socket error 
    Public Const EW_NODLL    =     (-15)           ' DLL not exist error 
    Public Const EW_BUS      =     (-11)           ' bus error 
    Public Const EW_SYSTEM2  =     (-10)           ' system error 
    Public Const EW_HSSB     =     (-9)            ' hssb communication error 
    Public Const EW_HANDLE   =     (-8)            ' Windows library handle error 
    Public Const EW_VERSION  =     (-7)            ' CNC/PMC version missmatch 
    Public Const EW_UNEXP    =     (-6)            ' abnormal error 
    Public Const EW_SYSTEM   =     (-5)            ' system error 
    Public Const EW_PARITY   =     (-4)            ' shared RAM parity error 
    Public Const EW_MMCSYS   =     (-3)            ' emm386 or mmcsys install error 
    Public Const EW_RESET    =     (-2)            ' reset or stop occured error 
    Public Const EW_BUSY     =     (-1)            ' busy error 
    Public Const EW_OK       =     0               ' no problem 
    Public Const EW_FUNC     =     1               ' command prepare error 
    Public Const EW_NOPMC    =     1               ' pmc not exist 
    Public Const EW_LENGTH   =     2               ' data block length error 
    Public Const EW_NUMBER   =     3               ' data number error 
    Public Const EW_RANGE    =     3               ' address range error 
    Public Const EW_ATTRIB   =     4               ' data attribute error 
    Public Const EW_TYPE     =     4               ' data type error 
    Public Const EW_DATA     =     5               ' data error 
    Public Const EW_NOOPT    =     6               ' no option error 
    Public Const EW_PROT     =     7               ' write protect error 
    Public Const EW_OVRFLOW  =     8               ' memory overflow error 
    Public Const EW_PARAM    =     9               ' cnc parameter not correct error 
    Public Const EW_BUFFER   =     10              ' buffer error 
    Public Const EW_PATH     =     11              ' path error 
    Public Const EW_MODE     =     12              ' cnc mode error 
    Public Const EW_REJECT   =     13              ' execution rejected error 
    Public Const EW_DTSRVR   =     14              ' data server error 
    Public Const EW_ALARM    =     15              ' alarm has been occurred 
    Public Const EW_STOP     =     16              ' CNC is not running 
    Public Const EW_PASSWD   =     17              ' protection data error 

    ' Result codes of DNC operation
    
    Public Const DNC_NORMAL  =  (-1)               ' normal completed 
    Public Const DNC_CANCEL  =  (-32768)           ' DNC operation was canceled by CNC 
    Public Const DNC_OPENERR =  (-514)             ' file open error 
    Public Const DNC_NOFILE  =  (-516)             ' file not found 
    Public Const DNC_READERR =  (-517)             ' read error 

'--------------------
'                    
' Structure Template 
'                    
'--------------------
'-------------------------------------
' CNC: Control axis / spindle related 
'-------------------------------------

    ' cnc_actf:read actual axis feedrate(F) 
    ' cnc_acts:read actual spindle speed(S) 
    <StructLayout(LayoutKind.Sequential, Pack:=4)> _
    Public Structure ODBACT
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=2)> _
        Public dummy As Short()    ' dummy
        Public data As Integer     ' actual feed / actual spindle
    End Structure 'ODBACT

    ' cnc_acts2:read actual spindle speed(S) 
    ' (All or specified ) 
    <StructLayout(LayoutKind.Sequential, Pack:=4)> _
    Public Structure ODBACT2
        Public datano As Short     ' spindle number 
        Public type As Short       ' dummy 
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=4)> _
        Public data As Integer()   ' spindle data 
    End Structure 'ODBACT2

    ' cnc_absolute:read absolute axis position 
    ' cnc_machine:read machine axis position 
    ' cnc_relative:read relative axis position 
    ' cnc_distance:read distance to go 
    ' cnc_skip:read skip position 
    ' cnc_srvdelay:read servo delay value 
    ' cnc_accdecdly:read acceleration/deceleration delay value 
    ' cnc_absolute2:read absolute axis position 2 
    ' cnc_relative2:read relative axis position 2 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBAXIS 
        Public dummy As Short       ' dummy 
        Public type As Short        ' axis number 
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=MAX_AXIS)> _
        Public data As Integer()    ' data value 
    End Structure 'ODBAXIS

    ' cnc_rddynamic:read all dynamic data 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure FAXIS 
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=MAX_AXIS)> _
        Public absolute As Integer()    ' absolute position 
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=MAX_AXIS)> _
        Public machine As Integer()     ' machine position 
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=MAX_AXIS)> _
        Public relative As Integer()    ' relative position 
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=MAX_AXIS)> _
        Public distance As Integer()    ' distance to go 
    End Structure 'FAXIS
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure OAXIS 
        Public absolute  As Integer  ' absolute position 
        Public machine  As Integer   ' machine position 
        Public relative  As Integer  ' relative position 
        Public distance  As Integer  ' distance to go 
    End Structure
#If ONO8D = Nothing Then
    <StructLayout(LayoutKind.Sequential, Pack:=4)> _
    Public Structure ODBDY_1
        Public dummy As Short
        Public axis As Short      ' axis number 
        Public alarm As Short     ' alarm status 
        Public prgnum As Short    ' current program number 
        Public prgmnum As Short   ' main program number 
        Public seqnum As Integer  ' current sequence number 
        Public actf As Integer    ' actual feedrate 
        Public acts As Integer    ' actual spindle speed 
        Public pos As FAXIS
    End Structure

    <StructLayout(LayoutKind.Sequential, Pack:=4)> _
    Public Structure ODBDY_2
        Public dummy As Short
        Public axis As Short      ' axis number 
        Public alarm As Short     ' alarm status 
        Public prgnum As Short    ' current program number 
        Public prgmnum As Short   ' main program number 
        Public seqnum As Integer  ' current sequence number 
        Public actf As Integer    ' actual feedrate 
        Public acts As Integer    ' actual spindle speed 
        Public pos As OAXIS
    End Structure
#Else
    <StructLayout(LayoutKind.Sequential, Pack:=4)> _
    Public Structure ODBDY_1
        Public dummy As Short
        Public axis As Short      ' axis number 
        Public alarm As Short     ' alarm status 
        Public prgnum As Integer    ' current program number 
        Public prgmnum As Integer   ' main program number 
        Public seqnum As Integer    ' current sequence number 
        Public actf As Integer      ' actual feedrate 
        Public acts As Integer      ' actual spindle speed 
        Public pos As FAXIS
    End Structure
    <StructLayout(LayoutKind.Sequential, Pack:=4)> _
    Public Structure ODBDY_2
        Public dummy As Short
        Public axis As Short      ' axis number 
        Public alarm As Short     ' alarm status 
        Public prgnum As Integer    ' current program number 
        Public prgmnum As Integer   ' main program number 
        Public seqnum As Integer    ' current sequence number 
        Public actf As Integer      ' actual feedrate 
        Public acts As Integer      ' actual spindle speed 
        Public pos As OAXIS
    End Structure
#End If

    ' cnc_rddynamic2:read all dynamic data 
    <StructLayout(LayoutKind.Sequential, Pack:=4)> _
    Public Structure ODBDY2_1
        Public dummy As Short
        Public axis As Short      ' axis number 
        Public alarm As Integer     ' alarm status 
        Public prgnum As Integer    ' current program number 
        Public prgmnum As Integer   ' main program number 
        Public seqnum As Integer    ' current sequence number 
        Public actf As Integer      ' actual feedrate 
        Public acts As Integer      ' actual spindle speed 
        Public pos As FAXIS
    End Structure
    <StructLayout(LayoutKind.Sequential, Pack:=4)> _
    Public Structure ODBDY2_2
        Public dummy As Short
        Public axis As Short       ' axis number 
        Public alarm As Integer      ' alarm status 
        Public prgnum As Integer     ' current program number 
        Public prgmnum As Integer    ' main program number 
        Public seqnum As Integer     ' current sequence number 
        Public actf As Integer       ' actual feedrate 
        Public acts As Integer       ' actual spindle speed 
        Public pos As OAXIS          ' In case of 1 axis  
    End Structure

    ' cnc_wrrelpos:set origin / preset relative axis position 
    <StructLayout(LayoutKind.Sequential, Pack:=4)> _
    Public Structure IDBWRR
        Public datano As Short ' dummy 
        Public type As Short   ' axis number 
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=MAX_AXIS)> _
        Public data As Integer()   ' preset data 
    End Structure

    ' cnc_prstwkcd:preset work coordinate 
    <StructLayout(LayoutKind.Sequential, Pack:=4)> _
    Public Structure IDBWRA
        Public datano As Short ' dummy 
        Public type As Short   ' axis number 
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=MAX_AXIS)> _
        Public data As Integer()   ' preset data 
    End Structure

    ' cnc_rdmovrlap:read manual overlapped motion value 
    <StructLayout(LayoutKind.Sequential, Pack:=4)> _
    Public Structure IODBOVL
        Public datano As Short ' dummy 
        Public type As Short   ' axis number 
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=2 * MAX_AXIS)> _
        Public data As Integer()   ' data value:[2][MAX_AXIS] 
    End Structure

    ' cnc_rdspload:read load information of serial spindle 
    ' cnc_rdspmaxrpm:read maximum r.p.m. ratio of serial spindle 
    ' cnc_rdspgear:read gear ratio of serial spindle 
    <StructLayout(LayoutKind.Sequential, Pack:=4)> _
    Public Structure ODBSPN
        Public datano As Short ' dummy 
        Public type As Short   ' axis number 
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=4)> _
        Public data As Short()   ' preset data 
    End Structure

    ' cnc_rdposition:read tool position 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure POSELM
        Public data As Integer         ' position data 
        Public dec As Short        ' place of decimal point of position data 
        Public unit As Short       ' unit of position data 
        Public disp As Short       ' status of display 
        Public name As Char        ' axis name 
        Public suff As Char        ' axis name preffix 
    End Structure

    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure POSELMALL
        Public abs As POSELM
        Public mach As POSELM
        Public rel As POSELM
        Public dist As POSELM
    End Structure

#If M_AXIS2 Then
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBPOS
        Public p1 As POSELMALL
        Public p2 As POSELMALL
        Public p3 As POSELMALL
        Public p4 As POSELMALL
        Public p5 As POSELMALL
        Public p6 As POSELMALL
        Public p7 As POSELMALL
        Public p8 As POSELMALL
        Public p9 As POSELMALL
        Public p10 As POSELMALL
        Public p11 As POSELMALL
        Public p12 As POSELMALL
        Public p13 As POSELMALL
        Public p14 As POSELMALL
        Public p15 As POSELMALL
        Public p16 As POSELMALL
        Public p17 As POSELMALL
        Public p18 As POSELMALL
        Public p19 As POSELMALL
        Public p20 As POSELMALL
        Public p21 As POSELMALL
        Public p22 As POSELMALL
        Public p23 As POSELMALL
        Public p24 As POSELMALL
        ' In case of 24 axes.
        ' if you need the more information, you must be add the member.
    End Structure
#Else
#If FS15D Then
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBPOS
        Public p1 As POSELMALL
        Public p2 As POSELMALL
        Public p3 As POSELMALL
        Public p4 As POSELMALL
        Public p5 As POSELMALL
        Public p6 As POSELMALL
        Public p7 As POSELMALL
        Public p8 As POSELMALL
        Public p9 As POSELMALL
        Public p10 As POSELMALL
        ' In case of 10 axes.
        ' if you need the more information, you must be add the member.
    End Structure
#Else
    <StructLayout(LayoutKind.Sequential, Pack:=4)> _
    Public Structure ODBPOS
        Public p1 As POSELMALL
        Public p2 As POSELMALL
        Public p3 As POSELMALL
        Public p4 As POSELMALL
        Public p5 As POSELMALL
        Public p6 As POSELMALL
        Public p7 As POSELMALL
        Public p8 As POSELMALL
        ' In case of 8 axes.
        ' if you need the more information, you must be add the member.
    End Structure
#End If
#End If

    ' cnc_rdhndintrpt:read handle interruption 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBHND_data
        Public input As POSELM   ' input unit 
        Public output As POSELM  ' output unit 
    End Structure
#If M_AXIS2 Then
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBHND
        Public p1 As ODBHND_data
        Public p2 As ODBHND_data
        Public p3 As ODBHND_data
        Public p4 As ODBHND_data
        Public p5 As ODBHND_data
        Public p6 As ODBHND_data
        Public p7 As ODBHND_data
        Public p8 As ODBHND_data
        Public p9 As ODBHND_data
        Public p10 As ODBHND_data
        Public p11 As ODBHND_data
        Public p12 As ODBHND_data
        Public p13 As ODBHND_data
        Public p14 As ODBHND_data
        Public p15 As ODBHND_data
        Public p16 As ODBHND_data
        Public p17 As ODBHND_data
        Public p18 As ODBHND_data
        Public p19 As ODBHND_data
        Public p20 As ODBHND_data
        Public p21 As ODBHND_data
        Public p22 As ODBHND_data
        Public p23 As ODBHND_data
        Public p24 As ODBHND_data
        ' In case of 24 axes.
        ' if you need the more information, you must be add the member.
    End Structure
#Else
#If FS15D Then
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBHND
        Public p1 As ODBHND_data
        Public p2 As ODBHND_data
        Public p3 As ODBHND_data
        Public p4 As ODBHND_data
        Public p5 As ODBHND_data
        Public p6 As ODBHND_data
        Public p7 As ODBHND_data
        Public p8 As ODBHND_data
        Public p9 As ODBHND_data
        Public p10 As ODBHND_data
        ' In case of 10 axes.
        ' if you need the more information, you must be add the member.
    End Structure
#Else
    <StructLayout(LayoutKind.Sequential, Pack:=4)> _
    Public Structure ODBHND
        Public p1 As ODBHND_data
        Public p2 As ODBHND_data
        Public p3 As ODBHND_data
        Public p4 As ODBHND_data
        Public p5 As ODBHND_data
        Public p6 As ODBHND_data
        Public p7 As ODBHND_data
        Public p8 As ODBHND_data
        ' In case of 8 axes.
        ' if you need the more information, you must be add the member.
    End Structure
#End If
#End If

    ' cnc_rdspeed:read current speed 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure SPEEDELM
        Public data As Integer      ' speed data 
        Public dec As Short       ' decimal position 
        Public unit As Short       ' data unit 
        Public disp As Short       ' display flag 
        Public name As Byte      ' name of data 
        Public suff As Byte       ' suffix 
    End Structure

    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBSPEED
        Public actf As SPEEDELM   ' actual feed rate 
        Public acts As SPEEDELM   ' actual spindle speed 
    End Structure

    ' cnc_rdsvmeter:read servo load meter 
    ' cnc_rdspmeter:read spindle load meter 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure LOADELM
        Public data As Integer       ' load meter 
        Public dec  As Short      ' decimal position 
        Public unit As Short       ' unit 
        Public name As Byte      ' name of data 
        Public suff1 As Byte      ' suffix 
        Public suff2 As Byte      ' suffix 
        Public reserve As Byte    ' reserve 
    End Structure

#If M_AXIS2 Then
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBSVLOAD
        Public svload1 As LOADELM     ' servo load meter 
        Public svload2 As LOADELM     ' servo load meter 
        Public svload3 As LOADELM     ' servo load meter 
        Public svload4 As LOADELM     ' servo load meter 
        Public svload5 As LOADELM     ' servo load meter 
        Public svload6 As LOADELM     ' servo load meter 
        Public svload7 As LOADELM     ' servo load meter 
        Public svload8 As LOADELM     ' servo load meter 
        Public svload9 As LOADELM     ' servo load meter 
        Public svload10 As LOADELM    ' servo load meter 
        Public svload11 As LOADELM    ' servo load meter 
        Public svload12 As LOADELM    ' servo load meter 
        Public svload13 As LOADELM    ' servo load meter 
        Public svload14 As LOADELM    ' servo load meter 
        Public svload15 As LOADELM    ' servo load meter 
        Public svload16 As LOADELM    ' servo load meter 
        Public svload17 As LOADELM    ' servo load meter 
        Public svload18 As LOADELM    ' servo load meter 
        Public svload19 As LOADELM    ' servo load meter 
        Public svload20 As LOADELM    ' servo load meter 
        Public svload21 As LOADELM    ' servo load meter 
        Public svload22 As LOADELM    ' servo load meter 
        Public svload23 As LOADELM    ' servo load meter 
        Public svload24 As LOADELM    ' servo load meter 
    End Structure
#Else
#If FS15D Then
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBSVLOAD
        Public svload1 As LOADELM     ' servo load meter 
        Public svload2 As LOADELM     ' servo load meter 
        Public svload3 As LOADELM     ' servo load meter 
        Public svload4 As LOADELM     ' servo load meter 
        Public svload5 As LOADELM     ' servo load meter 
        Public svload6 As LOADELM     ' servo load meter 
        Public svload7 As LOADELM     ' servo load meter 
        Public svload8 As LOADELM     ' servo load meter 
        Public svload9 As LOADELM     ' servo load meter 
        Public svload10 As LOADELM    ' servo load meter 
    End Structure
#Else
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBSVLOAD
        Public svload1 As LOADELM     ' servo load meter 
        Public svload2 As LOADELM     ' servo load meter 
        Public svload3 As LOADELM     ' servo load meter 
        Public svload4 As LOADELM     ' servo load meter 
        Public svload5 As LOADELM     ' servo load meter 
        Public svload6 As LOADELM     ' servo load meter 
        Public svload7 As LOADELM     ' servo load meter 
        Public svload8 As LOADELM     ' servo load meter 
    End Structure
#End If
#End If

    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBSPLOAD_data
        Public spload As LOADELM     ' spindle load meter 
        Public spspeed As LOADELM    ' spindle speed 
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBSPLOAD
        Public spload1 As ODBSPLOAD_data     ' spindle load 
        Public spload2 As ODBSPLOAD_data     ' spindle load 
        Public spload3 As ODBSPLOAD_data     ' spindle load 
        Public spload4 As ODBSPLOAD_data     ' spindle load 
    End Structure

    ' cnc_rdexecpt:read execution program pointer
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure PRGPNT
        Public prog_no As Integer            ' program number
        Public blk_no  As Integer            ' block number
    End Structure

    ' cnc_rd5axmandt:read manual feed for 5-axis machining
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODB5AXMAN
        Public type1 As Short
        Public type2 As Short
        Public type3 As Short
        Public data1 As Integer
        Public data2 As Integer
        Public data3 As Integer
        Public c1    As Integer
        Public c2    As Integer
        Public dummy As Integer
        Public td    As Integer
        Public r1    As Integer
        Public r2    As Integer
        Public vr    As Integer
        Public h1    As Integer
        Public h2    As Integer
    End Structure

'----------------------
' CNC: Program related 
'----------------------

    ' cnc_rddncdgndt:read the diagnosis data of DNC operation 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBDNCDGN
        Public ctrl_word As Short
        Public can_word As Short
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=16)> _
        Public nc_file As Char()
        Public read_ptr As Short
        Public write_ptr As Short
        Public empty_cnt As Short
        Public total_size As Integer
    End Structure

    ' cnc_upload:upload NC program 
    ' cnc_cupload:upload NC program(conditional) 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBUP
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=2)> _
        Public dummy As Short() ' dummy 
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=256)> _
        Public data As String   ' data 
    End Structure ' In case that the number of data is 256 

    ' cnc_buff:read buffer status for downloading/verification NC program 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBBUF
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=2)> _
        Public dummy As Short() ' dummy 
        Public data As Short    ' buffer status 
    End Structure

    ' cnc_rdprogdir:read program directory 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure PRGDIR
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=256)> _
        Public prg_data As Char() ' directory data 
    End Structure ' In case that the number of data is 256 

    ' cnc_rdproginfo:read program information 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBNC_1
        Public reg_prg As Short        ' registered program number 
        Public unreg_prg As Short      ' unregistered program number 
        Public used_mem As Integer       ' used memory area 
        Public unused_mem As Integer     ' unused memory area 
    End Structure

    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBNC_2
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=31)> _
        Public asc As Char() ' ASCII string type 
    End Structure

    ' cnc_rdprgnum:read program number under execution 
#If ONO8D = Nothing Then
    <StructLayout(LayoutKind.Sequential, Pack:=4)> _
    Public Structure ODBPRO
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=2)> _
        Public dummy As Short()   ' dummy 
        Public data As Short      ' running program number 
        Public mdata As Short     ' main program number 
    End Structure
#Else
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBPRO
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=2)> _
        Public dummy As Short()      ' dummy 
        Public data As Integer      ' running program number 
        Public mdata As Integer     ' main program number 
    End Structure
#End If

    ' cnc_exeprgname:read program name under execution
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBEXEPRG
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=36)> _
        Public name  As Char()      ' running program name
        Public o_num As Integer     ' running program number
    End Structure

    ' cnc_rdseqnum:read sequence number under execution 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBSEQ
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=2)> _
        Public dummy As Short()     ' dummy 
        Public data As Integer       ' sequence number 
    End Structure

    ' cnc_rdmdipntr:read execution pointer for MDI operation 
#If ONO8D = Nothing Then
    <StructLayout(LayoutKind.Sequential, Pack:=4)> _
    Public Structure ODBMDIP
        Public mdiprog As Short    ' exec. program number 
        Public mdipntr As Integer  ' exec. pointer 
        Public crntprog As Short   ' prepare program number 
        Public crntpntr As Integer ' prepare pointer 
    End Structure
#Else
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBMDIP
        Public mdiprog As Integer   ' exec. program number 
        Public mdipntr As Integer   ' exec. pointer 
        Public crntprog As Integer  ' prepare program number 
        Public crntpntr As Integer  ' prepare pointer 
    End Structure
#End If

    ' cnc_rdaxisdata:read various axis data
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBAXDT_data
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=4)> _
        Public name    As String    ' data
        Public data    As Integer   ' position data
        Public dec     As Short     ' decimal position
        Public unit    As Short     ' data unit
        Public flag    As Short     ' flags
        Public reserve As Short     ' reserve
    End Structure

    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBAXDT
        Public data1   As ODBAXDT_data
        Public data2   As ODBAXDT_data
        Public data3   As ODBAXDT_data
        Public data4   As ODBAXDT_data
        Public data5   As ODBAXDT_data
        Public data6   As ODBAXDT_data
        Public data7   As ODBAXDT_data
        Public data8   As ODBAXDT_data
        Public data9   As ODBAXDT_data
        Public data10  As ODBAXDT_data
        Public data11  As ODBAXDT_data
        Public data12  As ODBAXDT_data
        Public data13  As ODBAXDT_data
        Public data14  As ODBAXDT_data
        Public data15  As ODBAXDT_data
        Public data16  As ODBAXDT_data
        Public data17  As ODBAXDT_data
        Public data18  As ODBAXDT_data
        Public data19  As ODBAXDT_data
        Public data20  As ODBAXDT_data
        Public data21  As ODBAXDT_data
        Public data22  As ODBAXDT_data
        Public data23  As ODBAXDT_data
        Public data24  As ODBAXDT_data
        Public data25  As ODBAXDT_data
        Public data26  As ODBAXDT_data
        Public data27  As ODBAXDT_data
        Public data28  As ODBAXDT_data
        Public data29  As ODBAXDT_data
        Public data30  As ODBAXDT_data
        Public data31  As ODBAXDT_data
        Public data32  As ODBAXDT_data
        Public data33  As ODBAXDT_data
        Public data34  As ODBAXDT_data
        Public data35  As ODBAXDT_data
        Public data36  As ODBAXDT_data
        Public data37  As ODBAXDT_data
        Public data38  As ODBAXDT_data
        Public data39  As ODBAXDT_data
        Public data40  As ODBAXDT_data
        Public data41  As ODBAXDT_data
        Public data42  As ODBAXDT_data
        Public data43  As ODBAXDT_data
        Public data44  As ODBAXDT_data
        Public data45  As ODBAXDT_data
        Public data46  As ODBAXDT_data
        Public data47  As ODBAXDT_data
        Public data48  As ODBAXDT_data
        Public data49  As ODBAXDT_data
        Public data50  As ODBAXDT_data
        Public data51  As ODBAXDT_data
        Public data52  As ODBAXDT_data
        Public data53  As ODBAXDT_data
        Public data54  As ODBAXDT_data
        Public data55  As ODBAXDT_data
        Public data56  As ODBAXDT_data
        Public data57  As ODBAXDT_data
        Public data58  As ODBAXDT_data
        Public data59  As ODBAXDT_data
        Public data60  As ODBAXDT_data
        Public data61  As ODBAXDT_data
        Public data62  As ODBAXDT_data
        Public data63  As ODBAXDT_data
        Public data64  As ODBAXDT_data
        Public data65  As ODBAXDT_data
        Public data66  As ODBAXDT_data
        Public data67  As ODBAXDT_data
        Public data68  As ODBAXDT_data
        Public data69  As ODBAXDT_data
        Public data70  As ODBAXDT_data
        Public data71  As ODBAXDT_data
        Public data72  As ODBAXDT_data
        Public data73  As ODBAXDT_data
        Public data74  As ODBAXDT_data
        Public data75  As ODBAXDT_data
        Public data76  As ODBAXDT_data
        Public data77  As ODBAXDT_data
        Public data78  As ODBAXDT_data
        Public data79  As ODBAXDT_data
        Public data80  As ODBAXDT_data
        Public data81  As ODBAXDT_data
        Public data82  As ODBAXDT_data
        Public data83  As ODBAXDT_data
        Public data84  As ODBAXDT_data
        Public data85  As ODBAXDT_data
        Public data86  As ODBAXDT_data
        Public data87  As ODBAXDT_data
        Public data88  As ODBAXDT_data
        Public data89  As ODBAXDT_data
        Public data90  As ODBAXDT_data
        Public data91  As ODBAXDT_data
        Public data92  As ODBAXDT_data
        Public data93  As ODBAXDT_data
        Public data94  As ODBAXDT_data
        Public data95  As ODBAXDT_data
        Public data96  As ODBAXDT_data
        Public data97  As ODBAXDT_data
        Public data98  As ODBAXDT_data
        Public data99  As ODBAXDT_data
        Public data100 As ODBAXDT_data
        Public data101 As ODBAXDT_data
        Public data102 As ODBAXDT_data
        Public data103 As ODBAXDT_data
        Public data104 As ODBAXDT_data
        Public data105 As ODBAXDT_data
        Public data106 As ODBAXDT_data
        Public data107 As ODBAXDT_data
        Public data108 As ODBAXDT_data
        Public data109 As ODBAXDT_data
        Public data110 As ODBAXDT_data
        Public data111 As ODBAXDT_data
        Public data112 As ODBAXDT_data
        Public data113 As ODBAXDT_data
        Public data114 As ODBAXDT_data
        Public data115 As ODBAXDT_data
        Public data116 As ODBAXDT_data
        Public data117 As ODBAXDT_data
        Public data118 As ODBAXDT_data
        Public data119 As ODBAXDT_data
        Public data120 As ODBAXDT_data
        Public data121 As ODBAXDT_data
        Public data122 As ODBAXDT_data
        Public data123 As ODBAXDT_data
        Public data124 As ODBAXDT_data
        Public data125 As ODBAXDT_data
        Public data126 As ODBAXDT_data
        Public data127 As ODBAXDT_data
        Public data128 As ODBAXDT_data
    End Structure

    ' cnc_rdspcss:read constant surface speed data 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBCSS
        Public srpm As Integer      ' order spindle speed 
        Public sspm As Integer      ' order constant spindle speed 
        Public smax As Integer      ' order maximum spindle speed 
    End Structure

    ' cnc_rdpdf_drive:read program drive directory
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBPDFDRV
        Public max_num As Short     ' maximum drive number
        Public dummy   As Short
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=12)> _
        Public drive1  As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=12)> _
        Public drive2  As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=12)> _
        Public drive3  As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=12)> _
        Public drive4  As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=12)> _
        Public drive5  As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=12)> _
        Public drive6  As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=12)> _
        Public drive7  As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=12)> _
        Public drive8  As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=12)> _
        Public drive9  As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=12)> _
        Public drive10 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=12)> _
        Public drive11 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=12)> _
        Public drive12 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=12)> _
        Public drive13 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=12)> _
        Public drive14 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=12)> _
        Public drive15 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=12)> _
        Public drive16 As String
    End Structure

    ' cnc_rdpdf_inf:read program drive information 
    <StructLayout(LayoutKind.Sequential,CharSet:=CharSet.Ansi,Pack:=4)> _
    Public Structure ODBPDFINF
        Public used_page As Integer ' used capacity
        Public all_page  As Integer ' all capacity
        Public used_dir  As Integer ' used directory number
        Public all_dir   As Integer ' all directory number
    End Structure

    ' cnc_rdpdf_subdir:read directory (sub directories)
    <StructLayout(LayoutKind.Sequential,CharSet:=CharSet.Ansi,Pack:=4)> _
    Public Structure IDBPDFSDIR
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=212)> _
        Public path      As String  ' path name
        Public req_num   As Short   ' entry number
        Public dummy     As Short
    End Structure

    ' cnc_rdpdf_subdir:read directory (sub directories)
    <StructLayout(LayoutKind.Sequential,CharSet:=CharSet.Ansi,Pack:=4)> _
    Public Structure ODBPDFSDIR
        Public sub_exist As Short   ' existence of sub directory
        Public dummy     As Short
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=36)> _
        Public d_f       As String  ' directory name
    End Structure

    ' cnc_rdpdf_alldir:read directory (all files)
    <StructLayout(LayoutKind.Sequential,CharSet:=CharSet.Ansi,Pack:=4)> _
    Public Structure IDBPDFADIR
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=212)> _
        Public path      As String  ' path name
        Public req_num   As Short   ' entry number
        Public size_kind As Short   ' kind of size
        Public type      As Short   ' kind of format
        Public dummy     As Short
    End Structure

    ' cnc_rdpdf_alldir:read directory (all files)
    <StructLayout(LayoutKind.Sequential,CharSet:=CharSet.Ansi,Pack:=4)> _
    Public Structure ODBPDFADIR
        Public data_kind As Short    ' kinf of data
        Public year      As Short    ' last date and time
        Public mon       As Short    ' last date and time
        Public day       As Short    ' last date and time
        Public hour      As Short    ' last date and time
        Public min       As Short    ' last date and time
        Public sec       As Short    ' last date and time
        Public dummy     As Short
        Public dummy2    As Integer
        Public size      As Integer  ' size
        Public attr      As Integer  ' attribute
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=36)> _
        Public d_f       As String   ' path name
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=52)> _
        Public comment   As String   ' comment
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=12)> _
        Public o_time    As String   ' comment
    End Structure

    ' cnc_rdpdf_subdirn:read file count the directory has
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBPDFNFIL
        Public dir_num  As Short ' directory
        Public file_num As Short ' file
    End Structure
    
    ' cnc_wrpdf_attr:change attribute of program file and directory
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IDBPDFTDIR
        Public slct     As Integer      ' selection
        Public attr     As Integer      ' data
    End Structure

'---------------------------
' CNC: NC file data related 
'---------------------------

    ' cnc_rdtofs:read tool offset value 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBTOFS
        Public datano As Short ' data number 
        Public type As Short  ' data type 
        Public data As Integer  ' data 
    End Structure

    ' cnc_rdtofsr:read tool offset value(area specified) 
    ' cnc_wrtofsr:write tool offset value(area specified) 
    <StructLayout(LayoutKind.Explicit)> _
    Public Structure OFS_1
        <FieldOffset( 0 ), _
        MarshalAs(UnmanagedType.ByValArray,SizeConst:=5)> _
        Public m_ofs As Integer()     ' M Each 
        <FieldOffset( 0 ), _
        MarshalAs(UnmanagedType.ByValArray,SizeConst:=5)> _
        Public m_ofs_a As Integer()   ' M-A All 
        <FieldOffset( 0 ), _
        MarshalAs(UnmanagedType.ByValArray,SizeConst:=5)> _
        Public t_tip As Short()       ' T Each, 2-byte 
        <FieldOffset( 0 ), _
        MarshalAs(UnmanagedType.ByValArray,SizeConst:=5)> _
        Public t_ofs As Integer()     ' T Each, 4-byte 
    End Structure ' In case that the number of data is 5 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure OFS_2
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=2*5)> _
        Public m_ofs_b As Integer()   ' M-B All 
    End Structure ' In case that the number of data is 5 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure OFS_3
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=4*5)> _
        Public m_ofs_c As Integer()  ' M-C All 
    End Structure ' In case that the number of data is 5 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure T_OFS_A
        Public tip As Short
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=4)> _
        Public data As Integer()
    End Structure ' T-A All 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure T_OFS_A_data
        Public data1 As T_OFS_A
        Public data2 As T_OFS_A
        Public data3 As T_OFS_A
        Public data4 As T_OFS_A
        Public data5 As T_OFS_A
    End Structure ' In case that the number of data is 5 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure T_OFS_B
        Public tip As Short
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=8)> _
        Public data As Integer()
    End Structure ' T-B All 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure T_OFS_B_data
        Public data1 As T_OFS_B
        Public data2 As T_OFS_B
        Public data3 As T_OFS_B
        Public data4 As T_OFS_B
        Public data5 As T_OFS_B
    End Structure ' In case that the number of data is 5 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBTO_1_1
        Public datano_s As Short  ' start offset number 
        Public type As Short      ' offset type 
        Public datano_e As Short  ' end offset number 
        Public ofs As OFS_1
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBTO_1_2
        Public datano_s As Short   ' start offset number 
        Public type As Short      ' offset type 
        Public datano_e As Short  ' end offset number 
        Public ofs As OFS_2
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBTO_1_3
        Public datano_s As Short  ' start offset number 
        Public type As Short      ' offset type 
        Public datano_e As Short  ' end offset number 
        Public ofs As OFS_3
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBTO_2
        Public datano_s As Short  ' start offset number 
        Public type As Short      ' offset type 
        Public datano_e As Short  ' end offset number 
        Public tofsa As T_OFS_A_data
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBTO_3
        Public datano_s As Short  ' start offset number 
        Public type As Short      ' offset type 
        Public datano_e As Short  ' end offset number 
        Public tofsb As T_OFS_B_data
    End Structure

    ' cnc_rdzofs:read work zero offset value 
    ' cnc_wrzofs:write work zero offset value 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBZOFS
        Public datano As Short     ' offset NO. 
        Public type As Short       ' axis number 
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=MAX_AXIS)> _
        Public data As Integer()   ' data value 
    End Structure

    ' cnc_rdzofsr:read work zero offset value(area specified) 
    ' cnc_wrzofsr:write work zero offset value(area specified) 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBZOR
        Public datano_s As Short  ' start offset number 
        Public type As Short      ' axis number 
        Public datano_e As Short  ' end offset number 
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=7 * MAX_AXIS)> _
        Public data As Integer()  ' offset value 
    End Structure ' In case that the number of axes is MAX_AXIS, the number of data is 7 

    ' cnc_rdmsptype:read mesured point value 
    ' cnc_wrmsptype:write mesured point value 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBMSTP
        Public datano_s As Short  ' start offset number 
        Public dummy As Short     ' dummy 
        Public datano_e As Short  ' end offset number 
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=7)> _
        Public data As SByte()    ' mesured point value 
    End Structure

    ' cnc_rdparam:read parameter 
    ' cnc_wrparam:write parameter 
    ' cnc_rdset:read setting data 
    ' cnc_wrset:write setting data 
    ' cnc_rdparar:read parameter(area specified) 
    ' cnc_wrparas:write parameter(plural specified) 
    ' cnc_rdsetr:read setting data(area specified) 
    ' cnc_wrsets:write setting data(plural specified) 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure REALPRM
        Public prm_val As Integer    ' data of real parameter 
        Public dec_val As Integer    ' decimal point of real parameter 
    End Structure
#If M_AXIS2 Then
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure REALPRMS
        Public rdata1 As REALPRM
        Public rdata2 As REALPRM
        Public rdata3 As REALPRM
        Public rdata4 As REALPRM
        Public rdata5 As REALPRM
        Public rdata6 As REALPRM
        Public rdata7 As REALPRM
        Public rdata8 As REALPRM
        Public rdata9 As REALPRM
        Public rdata10 As REALPRM
        Public rdata11 As REALPRM
        Public rdata12 As REALPRM
        Public rdata13 As REALPRM
        Public rdata14 As REALPRM
        Public rdata15 As REALPRM
        Public rdata16 As REALPRM
        Public rdata17 As REALPRM
        Public rdata18 As REALPRM
        Public rdata19 As REALPRM
        Public rdata20 As REALPRM
        Public rdata21 As REALPRM
        Public rdata22 As REALPRM
        Public rdata23 As REALPRM
        Public rdata24 As REALPRM
    End Structure ' In case that the number of alarm is 24 
#Else
#If FS15D Then
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure REALPRMS
        Public rdata1 As REALPRM
        Public rdata2 As REALPRM
        Public rdata3 As REALPRM
        Public rdata4 As REALPRM
        Public rdata5 As REALPRM
        Public rdata6 As REALPRM
        Public rdata7 As REALPRM
        Public rdata8 As REALPRM
        Public rdata9 As REALPRM
        Public rdata10 As REALPRM
    End Structure ' In case that the number of alarm is 10 
#Else
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure REALPRMS
        Public rdata1 As REALPRM
        Public rdata2 As REALPRM
        Public rdata3 As REALPRM
        Public rdata4 As REALPRM
        Public rdata5 As REALPRM
        Public rdata6 As REALPRM
        Public rdata7 As REALPRM
        Public rdata8 As REALPRM
    End Structure ' In case that the number of alarm is 8 
#End If
#End If

    <StructLayout(LayoutKind.Explicit)> _
    Public Structure IODBPSD_1
        <FieldOffset( 0 )> _
        Public datano As Short    ' data number 
        <FieldOffset( 2 )> _
        Public type As Short      ' axis number 
        <FieldOffset( 4 )> _
        Public cdata As Byte      ' parameter / setting data 
        <FieldOffset( 4 )> _
        Public idata As Short
        <FieldOffset( 4 )> _
        Public ldata As Integer
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBPSD_2
        Public datano As Short    ' data number 
        Public type As Short      ' axis number 
        Public rdata As REALPRM
    End Structure
    <StructLayout(LayoutKind.Explicit)> _
    Public Structure IODBPSD_3
        <FieldOffset( 0 )> _
        Public datano As Short    ' data number 
        <FieldOffset( 2 )> _
        Public type As Short       ' axis number 
        <FieldOffset( 4 ), _
        MarshalAs(UnmanagedType.ByValArray,SizeConst:=MAX_AXIS)> _
        Public cdatas As Byte()
        <FieldOffset( 4 ), _
        MarshalAs(UnmanagedType.ByValArray,SizeConst:=MAX_AXIS)> _
        Public idatas As Short()
        <FieldOffset( 4 ), _
        MarshalAs(UnmanagedType.ByValArray,SizeConst:=MAX_AXIS)> _
        Public ldatas As Integer()
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBPSD_4
        Public datano As Short    ' data number 
        Public type As Short      ' axis number 
        Public rdatas As REALPRMS
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBPSD_A
        Public data1 As IODBPSD_1
        Public data2 As IODBPSD_1
        Public data3 As IODBPSD_1
        Public data4 As IODBPSD_1
        Public data5 As IODBPSD_1
        Public data6 As IODBPSD_1
        Public data7 As IODBPSD_1
    End Structure ' (sample) must be modified
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBPSD_B
        Public data1 As IODBPSD_2
        Public data2 As IODBPSD_2
        Public data3 As IODBPSD_2
        Public data4 As IODBPSD_2
        Public data5 As IODBPSD_2
        Public data6 As IODBPSD_2
        Public data7 As IODBPSD_2
    End Structure ' (sample) must be modified
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBPSD_C
        Public data1 As IODBPSD_3
        Public data2 As IODBPSD_3
        Public data3 As IODBPSD_3
        Public data4 As IODBPSD_3
        Public data5 As IODBPSD_3
        Public data6 As IODBPSD_3
        Public data7 As IODBPSD_3
    End Structure ' (sample) must be modified
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBPSD_D
        Public data1 As IODBPSD_4
        Public data2 As IODBPSD_4
        Public data3 As IODBPSD_4
        Public data4 As IODBPSD_4
        Public data5 As IODBPSD_4
        Public data6 As IODBPSD_4
        Public data7 As IODBPSD_4
    End Structure ' (sample) must be modified

    ' cnc_rdparam_ext:read parameAers 
    ' cnc_rddiag_ext:read diagnosis data 
    ' cnc_start_async_wrparam:async parameter write start 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBPRMNO
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=10)> _
        Public prm As Integer()
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBPRM_data
        Public prm_val As Integer   ' parameter / setting data 
        Public dec_val As Integer
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBPRM1
        Public data1 As IODBPRM_data
        Public data2 As IODBPRM_data
        Public data3 As IODBPRM_data
        Public data4 As IODBPRM_data
        Public data5 As IODBPRM_data
        Public data6 As IODBPRM_data
        Public data7 As IODBPRM_data
        Public data8 As IODBPRM_data
        Public data9 As IODBPRM_data
        Public data10 As IODBPRM_data
        Public data11 As IODBPRM_data
        Public data12 As IODBPRM_data
        Public data13 As IODBPRM_data
        Public data14 As IODBPRM_data
        Public data15 As IODBPRM_data
        Public data16 As IODBPRM_data
        Public data17 As IODBPRM_data
        Public data18 As IODBPRM_data
        Public data19 As IODBPRM_data
        Public data20 As IODBPRM_data
        Public data21 As IODBPRM_data
        Public data22 As IODBPRM_data
        Public data23 As IODBPRM_data
        Public data24 As IODBPRM_data
        Public data25 As IODBPRM_data
        Public data26 As IODBPRM_data
        Public data27 As IODBPRM_data
        Public data28 As IODBPRM_data
        Public data29 As IODBPRM_data
        Public data30 As IODBPRM_data
        Public data31 As IODBPRM_data
        Public data32 As IODBPRM_data
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBPRM2
        Public datano As Integer   ' data number 
        Public type As Short       ' data type 
        Public axis As Short       ' axis information 
        Public info As Short       ' misc information 
        Public unit As Short       ' unit information 
        Public data As IODBPRM1
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBPRM
        Public prm1 As IODBPRM2
        Public prm2 As IODBPRM2
        Public prm3 As IODBPRM2
        Public prm4 As IODBPRM2
        Public prm5 As IODBPRM2
        Public prm6 As IODBPRM2
        Public prm7 As IODBPRM2
        Public prm8 As IODBPRM2
        Public prm9 As IODBPRM2
        Public prm10 As IODBPRM2
    End Structure ' In case that the number of alarm is 10 

    ' cnc_rdpitchr:read pitch error compensation data(area specified) 
    ' cnc_wrpitchr:write pitch error compensation data(area specified) 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBPI
        Public datano_s As Short   ' start pitch number 
        Public dummy As Short      ' dummy 
        Public datano_e As Short   ' end pitch number 
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=5)> _
        Public data As SByte()     ' offset value 
    End Structure ' In case that the number of data is 5 

    ' cnc_rdmacro:read custom macro variable 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBM
        Public datano As Short    ' variable number 
        Public dummy As Short     ' dummy 
        Public mcr_val As Integer ' macro variable 
        Public dec_val As Short   ' decimal point 
    End Structure

    ' cnc_rdmacror:read custom macro variables(area specified) 
    ' cnc_wrmacror:write custom macro variables(area specified) 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBMR_data
        Public mcr_val As Integer ' macro variable 
        Public dec_val As Short   ' decimal point 
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBMR1
        Public data1 As IODBMR_data
        Public data2 As IODBMR_data
        Public data3 As IODBMR_data
        Public data4 As IODBMR_data
        Public data5 As IODBMR_data
    End Structure  ' In case that the number of data is 5 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBMR
        Public datano_s As Short  ' start macro number 
        Public dummy As Short     ' dummy 
        Public datano_e As Short  ' end macro number 
        Public data As IODBMR1
    End Structure

    ' cnc_rdpmacro:read P code macro variable 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBPM
        Public datano As Integer    ' variable number 
        Public dummy As Short       ' dummy 
        Public mcr_val As Integer   ' macro variable 
        Public dec_val As Short     ' decimal point 
    End Structure

    ' cnc_rdpmacror:read P code macro variables(area specified) 
    ' cnc_wrpmacror:write P code macro variables(area specified) 
    Public Structure IODBPR_data
        Public mcr_val As Integer   ' macro variable 
        Public dec_val As Short     ' decimal point 
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBPR1
        Public data1 As IODBPR_data
        Public data2 As IODBPR_data
        Public data3 As IODBPR_data
        Public data4 As IODBPR_data
        Public data5 As IODBPR_data
    End Structure  ' In case that the number of data is 5 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBPR
        Public datano_s As Integer  ' start macro number 
        Public dummy As Short       ' dummy 
        Public datano_e As Integer  ' end macro number 
        Public data As IODBPR1
    End Structure

    ' cnc_rdtofsinfo:read tool offset information 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBTLINF
        Public ofs_type As Short
        Public use_no As Short
    End Structure

    ' cnc_rdtofsinfo2:read tool offset information(2)
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBTLINF2
        Public ofs_type As Short
        Public use_no As Short
        Public ofs_enable As Short
    End Structure

    ' cnc_rdmacroinfo:read custom macro variable information 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBMVINF
        Public use_no1 As Short
        Public use_no2 As Short
    End Structure

    ' cnc_rdpmacroinfo:read P code macro variable information 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBPMINF
        Public use_no1 As Short
#If PCD_UWORD Then
        Public use_no2 As Short ' C# ushort
#Else
        Public use_no2 As Short
#End If
        Public v2_type As Short
    End Structure

    ' cnc_tofs_rnge:read validity of tool offset
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBDATRNG
        Public data_min As Integer   ' lower limit
        Public data_max As Integer   ' upper limit
        Public status   As Integer   ' status of setting
    End Structure

    ' cnc_rdhsprminfo:read the information for function cnc_rdhsparam()
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure HSPINFO_data
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=16)> _
        Public data1  As Byte()
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=16)> _
        Public data2  As Byte()
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=16)> _
        Public data3  As Byte()
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=16)> _
        Public data4  As Byte()
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=16)> _
        Public data5  As Byte()
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=16)> _
        Public data6  As Byte()
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=16)> _
        Public data7  As Byte()
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=16)> _
        Public data8  As Byte()
    End Structure

    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure HSPINFO
        Public prminfo1  As HSPINFO_data
        Public prminfo2  As HSPINFO_data
        Public prminfo3  As HSPINFO_data
        Public prminfo4  As HSPINFO_data
        Public prminfo5  As HSPINFO_data
        Public prminfo6  As HSPINFO_data
        Public prminfo7  As HSPINFO_data
        Public prminfo8  As HSPINFO_data
        Public prminfo9  As HSPINFO_data
        Public prminfo10 As HSPINFO_data
    End Structure   ' In case that the number of data is 10

    ' cnc_rdhsparam:read parameters at the high speed
    <StructLayout(LayoutKind.Explicit)> _
    Public Structure HSPDATA_1
        <FieldOffset( 0 ), _
        MarshalAs(UnmanagedType.ByValArray,SizeConst:=MAX_AXIS)> _
        Public cdatas1  As Byte()
        <FieldOffset( 4*MAX_AXIS ), _
        MarshalAs(UnmanagedType.ByValArray,SizeConst:=MAX_AXIS)> _
        Public cdatas2  As Byte()
        <FieldOffset( 4*2*MAX_AXIS ), _
        MarshalAs(UnmanagedType.ByValArray,SizeConst:=MAX_AXIS)> _
        Public cdatas3  As Byte()
        <FieldOffset( 4*3*MAX_AXIS ), _
        MarshalAs(UnmanagedType.ByValArray,SizeConst:=MAX_AXIS)> _
        Public cdatas4  As Byte()
        <FieldOffset( 4*4*MAX_AXIS ), _
        MarshalAs(UnmanagedType.ByValArray,SizeConst:=MAX_AXIS)> _
        Public cdatas5  As Byte()
        <FieldOffset( 4*5*MAX_AXIS ), _
        MarshalAs(UnmanagedType.ByValArray,SizeConst:=MAX_AXIS)> _
        Public cdatas6  As Byte()
        <FieldOffset( 4*6*MAX_AXIS ), _
        MarshalAs(UnmanagedType.ByValArray,SizeConst:=MAX_AXIS)> _
        Public cdatas7  As Byte()
        <FieldOffset( 4*7*MAX_AXIS ), _
        MarshalAs(UnmanagedType.ByValArray,SizeConst:=MAX_AXIS)> _
        Public cdatas8  As Byte()
        <FieldOffset( 4*8*MAX_AXIS ), _
        MarshalAs(UnmanagedType.ByValArray,SizeConst:=MAX_AXIS)> _
        Public cdatas9  As Byte()
        <FieldOffset( 4*9*MAX_AXIS ), _
        MarshalAs(UnmanagedType.ByValArray,SizeConst:=MAX_AXIS)> _
        Public cdatas10 As Byte()
    End Structure   ' In case that the number of data is 10

    <StructLayout(LayoutKind.Explicit)> _
    Public Structure HSPDATA_2
        <FieldOffset( 0 ), _
        MarshalAs(UnmanagedType.ByValArray,SizeConst:=MAX_AXIS)> _
        Public idatas1  As Short()
        <FieldOffset( 2*MAX_AXIS ), _
        MarshalAs(UnmanagedType.ByValArray,SizeConst:=MAX_AXIS)> _
        Public idatas2  As Short()
        <FieldOffset( 2*2*MAX_AXIS ), _
        MarshalAs(UnmanagedType.ByValArray,SizeConst:=MAX_AXIS)> _
        Public idatas3  As Short()
        <FieldOffset( 2*3*MAX_AXIS ), _
        MarshalAs(UnmanagedType.ByValArray,SizeConst:=MAX_AXIS)> _
        Public idatas4  As Short()
        <FieldOffset( 2*4*MAX_AXIS ), _
        MarshalAs(UnmanagedType.ByValArray,SizeConst:=MAX_AXIS)> _
        Public idatas5  As Short()
        <FieldOffset( 2*5*MAX_AXIS ), _
        MarshalAs(UnmanagedType.ByValArray,SizeConst:=MAX_AXIS)> _
        Public idatas6  As Short()
        <FieldOffset( 2*6*MAX_AXIS ), _
        MarshalAs(UnmanagedType.ByValArray,SizeConst:=MAX_AXIS)> _
        Public idatas7  As Short()
        <FieldOffset( 2*7*MAX_AXIS ), _
        MarshalAs(UnmanagedType.ByValArray,SizeConst:=MAX_AXIS)> _
        Public idatas8  As Short()
        <FieldOffset( 2*8*MAX_AXIS ), _
        MarshalAs(UnmanagedType.ByValArray,SizeConst:=MAX_AXIS)> _
        Public idatas9  As Short()
        <FieldOffset( 2*9*MAX_AXIS ), _
        MarshalAs(UnmanagedType.ByValArray,SizeConst:=MAX_AXIS)> _
        Public idatas10 As Short()
    End Structure   ' In case that the number of data is 10

    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure HSPDATA_3
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=MAX_AXIS)> _
        Public ldatas1  As Integer()
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=MAX_AXIS)> _
        Public ldatas2  As Integer()
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=MAX_AXIS)> _
        Public ldatas3  As Integer()
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=MAX_AXIS)> _
        Public ldatas4  As Integer()
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=MAX_AXIS)> _
        Public ldatas5  As Integer()
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=MAX_AXIS)> _
        Public ldatas6  As Integer()
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=MAX_AXIS)> _
        Public ldatas7  As Integer()
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=MAX_AXIS)> _
        Public ldatas8  As Integer()
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=MAX_AXIS)> _
        Public ldatas9  As Integer()
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=MAX_AXIS)> _
        Public ldatas10 As Integer()
    End Structure   ' In case that the number of data is 10

'----------------------------------------
' CNC: Tool life management data related 
'----------------------------------------

    ' cnc_rdgrpid:read tool life management data(tool group number) 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBTLIFE1
        Public dummy As Short   ' dummy 
        Public type As Short    ' data type 
        Public data As Integer  ' data 
    End Structure

    ' cnc_rdngrp:read tool life management data(number of tool groups) 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBTLIFE2
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=2)> _
        Public dummy As Short() ' dummy 
        Public data As Integer  ' data 
    End Structure

    ' cnc_rdntool:read tool life management data(number of tools) 
    ' cnc_rdlife:read tool life management data(tool life) 
    ' cnc_rdcount:read tool life management data(tool lift counter) 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBTLIFE3
        Public datano As Short      ' data number 
        Public dummy As Short       ' dummy 
        Public data As Integer      ' data 
    End Structure

    ' cnc_rd1length:read tool life management data(tool length number-1) 
    ' cnc_rd2length:read tool life management data(tool length number-2) 
    ' cnc_rd1radius:read tool life management data(cutter compensation no.-1) 
    ' cnc_rd2radius:read tool life management data(cutter compensation no.-2) 
    ' cnc_t1info:read tool life management data(tool information-1) 
    ' cnc_t2info:read tool life management data(tool information-2) 
    ' cnc_toolnum:read tool life management data(tool number) 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBTLIFE4
        Public datano As Short      ' data number 
        Public type As Short        ' data type 
        Public data As Integer      ' data 
    End Structure

    ' cnc_rdgrpid2:read tool life management data(tool group number) 2 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBTLIFE5
        Public dummy As Integer ' dummy 
        Public type As Integer  ' data type 
        Public data As Integer  ' data 
    End Structure

    ' cnc_rdtoolrng:read tool life management data(tool number, tool life, tool life counter)(area specified) 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBTR_data
        Public ntool As Integer     ' tool number 
        Public life As Integer      ' tool life 
        Public count As Integer     ' tool life counter 
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBTR1
        Public data1 As IODBTR_data
        Public data2 As IODBTR_data
        Public data3 As IODBTR_data
        Public data4 As IODBTR_data
        Public data5 As IODBTR_data
    End Structure ' In case that the number of data is 5 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBTR
        Public datano_s As Short  ' start group number 
        Public dummy As Short     ' dummy 
        Public datano_e As Short  ' end group number 
        Public data As IODBTR1
    End Structure

    ' cnc_rdtoolgrp:read tool life management data(all data within group) 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBTG_data
        Public tuse_num As Integer      ' tool number 
        Public tool_num As Integer      ' tool life 
        Public length_num As Integer    ' tool life counter 
        Public radius_num As Integer    ' tool life counter 
        Public tinfo As Integer         ' tool life counter 
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBTG1
        Public data1 As ODBTG_data
        Public data2 As ODBTG_data
        Public data3 As ODBTG_data
        Public data4 As ODBTG_data
        Public data5 As ODBTG_data
    End Structure ' In case that the number of data is 5 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBTG
        Public grp_num As Short   ' start group number 
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=2)> _
        Public dummy As Short()  ' dummy 
        Public ntool As Integer      ' tool number 
        Public life As Integer       ' tool life 
        Public count As Integer      ' tool life counter 
        Public data As ODBTG1
    End Structure

    ' cnc_wrcountr:write tool life management data(tool life counter) (area specified) 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IDBWRC_data
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=2)> _
        Public dummy As Integer()    ' dummy 
        Public count As Integer      ' tool life counter 
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IDBWRC1
        Public data1 As IDBWRC_data
        Public data2 As IDBWRC_data
        Public data3 As IDBWRC_data
        Public data4 As IDBWRC_data
        Public data5 As IDBWRC_data
    End Structure ' In case that the number of data is 5 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IDBWRC
        Public datano_s As Short  ' start group number 
        Public dummy As Short     ' dummy 
        Public datano_e As Short  ' end group number 
        Public data As IDBWRC1
    End Structure

    ' cnc_rdusegrpid:read tool life management data(used tool group number) 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBUSEGR
        Public datano As Short   ' dummy 
        Public type As Short     ' dummy 
        Public iNext As Integer   ' next use group number 
        Public use As Integer    ' using group number 
        Public slct As Integer   ' selecting group number 
    End Structure

    ' cnc_rdmaxgrp:read tool life management data(max. number of tool groups) 
    ' cnc_rdmaxtool:read tool life management data(maximum number of tool within group) 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBLFNO
        Public datano As Short ' dummy 
        Public type As Short   ' dummy 
        Public data As Short   ' number of data 
    End Structure

    ' cnc_rdusetlno:read tool life management data(used tool no within group) 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBTLUSE
        Public s_grp As Short ' start group number 
        Public dummy As Short ' dummy 
        Public e_grp As Short ' end group number 
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=5)> _
        Public data  As Integer()  ' tool using number 
    End Structure ' In case that the number of group is 5 

    ' cnc_rd1tlifedata:read tool life management data(tool data1) 
    ' cnc_rd2tlifedata:read tool life management data(tool data2) 
    ' cnc_wr1tlifedata:write tool life management data(tool data1) 
    ' cnc_wr2tlifedata:write tool life management data(tool data2) 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBTD
        Public datano As Short      ' tool group number 
        Public type As Short        ' tool using number 
        Public tool_num As Integer  ' tool number 
        Public h_code As Integer    ' H code 
        Public d_code As Integer    ' D code 
        Public tool_inf As Integer  ' tool information 
    End Structure

    ' cnc_rd1tlifedat2:read tool life management data(tool data1) 2 
    ' cnc_wr1tlifedat2:write tool life management data(tool data1) 2 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBTD2
        Public datano As Short       ' tool group number 
        Public dummy As Short        ' dummy 
        Public type As Integer       ' tool using number 
        Public tool_num As Integer   ' tool number 
        Public h_code As Integer     ' H code 
        Public d_code As Integer     ' D code 
        Public tool_inf As Integer   ' tool information 
    End Structure

    ' cnc_rdgrpinfo:read tool life management data(tool group information) 
    ' cnc_wrgrpinfo:write tool life management data(tool group information) 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBTGI_data
        Public n_tool As Integer         ' number of tool 
        Public count_value As Integer    ' tool life 
        Public counter As Integer        ' tool life counter 
        Public count_type As Integer     ' tool life counter type 
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBTGI1
        Public data1 As IODBTGI_data
        Public data2 As IODBTGI_data
        Public data3 As IODBTGI_data
        Public data4 As IODBTGI_data
        Public data5 As IODBTGI_data
    End Structure ' In case that the number of data is 5 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBTGI
        Public s_grp As Short ' start group number 
        Public dummy As Short ' dummy 
        Public e_grp As Short ' end group number 
        Public data As IODBTGI1
    End Structure

    ' cnc_rdgrpinfo2:read tool life management data(tool group information 2) 
    ' cnc_wrgrpinfo2:write tool life management data(tool group information 2) 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBTGI2
        Public s_grp As Short     ' start group number 
        Public dummy As Short     ' dummy 
        Public e_grp As Short     ' end group number 
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=5)> _
        Public opt_grpno As Integer()  ' optional group number of tool 
    End Structure ' In case that the number of group is 5 

    ' cnc_rdgrpinfo3:read tool life management data(tool group information 3) 
    ' cnc_wrgrpinfo3:write tool life management data(tool group information 3) 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBTGI3
        Public s_grp As Short     ' start group number 
        Public dummy As Short     ' dummy 
        Public e_grp As Short     ' end group number 
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=5)> _
        Public life_rest As Integer()  ' tool life rest count 
    End Structure ' In case that the number of group is 5 

    ' cnc_rdgrpinfo4:read tool life management data(tool group information 4)
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBTGI4
        Public grp_no      As Short
        Public n_tool      As Integer
        Public count_value As Integer
        Public counter     As Integer
        Public count_type  As Integer
        Public opt_grpno   As Integer
        Public life_rest   As Integer
    End Structure

    ' cnc_instlifedt:insert tool life management data(tool data) 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IDBITD
        Public datano As Short   ' tool group number 
        Public type As Short     ' tool using number 
        Public data As Integer   ' tool number 
    End Structure

    ' cnc_rdtlinfo:read tool life management data 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBTLINFO
        Public max_group As Integer  ' maximum number of tool groups 
        Public max_tool As Integer   ' maximum number of tool within group 
        Public max_minute As Integer ' maximum number of life count (minutes) 
        Public max_cycle As Integer  ' maximum number of life count (cycles) 
    End Structure

    ' cnc_rdtlusegrp:read tool life management data(used tool group number) 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBUSEGRP
        Public iNext As Integer       ' next use group number 
        Public use As Integer        ' using group number 
        Public slct As Integer       ' selecting group number 
        Public opt_next As Integer   ' next use optional group number 
        Public opt_use As Integer    ' using optional group number 
        Public opt_slct As Integer   ' selecting optional group number 
    End Structure

    ' cnc_rdtlgrp:read tool life management data(tool group information 2) 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBTLGRP_data
        Public ntool As Integer      ' number of all tool 
        Public nfree As Integer      ' number of free tool 
        Public life As Integer       ' tool life 
        Public count As Integer      ' tool life counter 
        Public use_tool As Integer   ' using tool number 
        Public opt_grpno As Integer  ' optional group number 
        Public life_rest As Integer  ' tool life rest count 
        Public rest_sig As Short     ' tool life rest signal 
        Public count_type As Short   ' tool life counter type 
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBTLGRP
        Public data1 As IODBTLGRP_data
        Public data2 As IODBTLGRP_data
        Public data3 As IODBTLGRP_data
        Public data4 As IODBTLGRP_data
        Public data5 As IODBTLGRP_data
    End Structure ' In case that the number of group is 5 

    ' cnc_rdtltool:read tool life management data (tool data1) 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBTLTOOL_data
        Public tool_num As Integer   ' tool number 
        Public h_code As Integer     ' H code 
        Public d_code As Integer     ' D code 
        Public tool_inf As Integer   ' tool information 
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBTLTOOL
        Public data1 As IODBTLTOOL_data
        Public data2 As IODBTLTOOL_data
        Public data3 As IODBTLTOOL_data
        Public data4 As IODBTLTOOL_data
        Public data5 As IODBTLTOOL_data
    End Structure ' In case that the number of group is 5 

    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBEXGP_data
        Public grp_no As Integer     ' group number
        Public opt_grpno As Integer  ' optional group number
    End Structure 

    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBEXGP
        Public data1  As ODBEXGP_data
        Public data2  As ODBEXGP_data
        Public data3  As ODBEXGP_data
        Public data4  As ODBEXGP_data
        Public data5  As ODBEXGP_data
        Public data6  As ODBEXGP_data
        Public data7  As ODBEXGP_data
        Public data8  As ODBEXGP_data
        Public data9  As ODBEXGP_data
        Public data10 As ODBEXGP_data
        Public data11 As ODBEXGP_data
        Public data12 As ODBEXGP_data
        Public data13 As ODBEXGP_data
        Public data14 As ODBEXGP_data
        Public data15 As ODBEXGP_data
        Public data16 As ODBEXGP_data
        Public data17 As ODBEXGP_data
        Public data18 As ODBEXGP_data
        Public data19 As ODBEXGP_data
        Public data20 As ODBEXGP_data
        Public data21 As ODBEXGP_data
        Public data22 As ODBEXGP_data
        Public data23 As ODBEXGP_data
        Public data24 As ODBEXGP_data
        Public data25 As ODBEXGP_data
        Public data26 As ODBEXGP_data
        Public data27 As ODBEXGP_data
        Public data28 As ODBEXGP_data
        Public data29 As ODBEXGP_data
        Public data30 As ODBEXGP_data
        Public data31 As ODBEXGP_data
        Public data32 As ODBEXGP_data
    End Structure 

'-----------------------------------
' CNC: Tool management data related 
'-----------------------------------

    ' cnc_regtool:new registration of tool management data 
    ' cnc_rdtool:lead of tool management data 
    ' cnc_wrtool:write of tool management data 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBTLMNG_data
        Public T_code As Integer
        Public life_count As Integer
        Public max_life As Integer
        Public rest_life As Integer
        Public life_stat As Byte
        Public cust_bits As Byte
        Public tool_info As Short ' C# ushort
        Public H_code As Short
        Public D_code As Short
        Public spindle_speed As Integer
        Public feedrate As Integer
        Public magazine As Short
        Public pot As Short
        Public G_code As Short
        Public W_code As Short
        Public gno    As Short
        public m_ofs  As Short
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=4)> _
        Public reserved As Integer()
        Public custom1 As Integer
        Public custom2 As Integer
        Public custom3 As Integer
        Public custom4 As Integer
        Public custom5 As Integer
        Public custom6 As Integer
        Public custom7 As Integer
        Public custom8 As Integer
        Public custom9 As Integer
        Public custom10 As Integer
        Public custom11 As Integer
        Public custom12 As Integer
        Public custom13 As Integer
        Public custom14 As Integer
        Public custom15 As Integer
        Public custom16 As Integer
        Public custom17 As Integer
        Public custom18 As Integer
        Public custom19 As Integer
        Public custom20 As Integer
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBTLMNG 
        Public data1 As IODBTLMNG_data
        Public data2 As IODBTLMNG_data
        Public data3 As IODBTLMNG_data
        Public data4 As IODBTLMNG_data
        Public data5 As IODBTLMNG_data
    End Structure ' In case that the number of group is 5 

    ' cnc_regtool_f2:new registration of tool management data 
    ' cnc_rdtool_f2:lead of tool management data 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBTLMNG_F2_data
        Public T_code As Integer
        Public life_count As Integer
        Public max_life As Integer
        Public rest_life As Integer
        Public life_stat As Byte
        Public cust_bits As Byte
        Public tool_info As Short ' C# ushort
        Public H_code As Short
        Public D_code As Short
        Public spindle_speed As Integer
        Public feedrate As Integer
        Public magazine As Short
        Public pot As   Short
        Public G_code   As Short
        Public W_code   As Short
        Public gno      As Short
        public m_ofs    As Short
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=3)> _
        Public reserved As Integer()
        Public custom1 As Integer
        Public custom2 As Integer
        Public custom3 As Integer
        Public custom4 As Integer
        Public custom5 As Integer
        Public custom6 As Integer
        Public custom7 As Integer
        Public custom8 As Integer
        Public custom9 As Integer
        Public custom10 As Integer
        Public custom11 As Integer
        Public custom12 As Integer
        Public custom13 As Integer
        Public custom14 As Integer
        Public custom15 As Integer
        Public custom16 As Integer
        Public custom17 As Integer
        Public custom18 As Integer
        Public custom19 As Integer
        Public custom20 As Integer
        Public custom21 As Integer
        Public custom22 As Integer
        Public custom23 As Integer
        Public custom24 As Integer
        Public custom25 As Integer
        Public custom26 As Integer
        Public custom27 As Integer
        Public custom28 As Integer
        Public custom29 As Integer
        Public custom30 As Integer
        Public custom31 As Integer
        Public custom32 As Integer
        Public custom33 As Integer
        Public custom34 As Integer
        Public custom35 As Integer
        Public custom36 As Integer
        Public custom37 As Integer
        Public custom38 As Integer
        Public custom39 As Integer
        Public custom40 As Integer
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBTLMNG_F2
        Public data1 As IODBTLMNG_F2_data
        Public data2 As IODBTLMNG_F2_data
        Public data3 As IODBTLMNG_F2_data
        Public data4 As IODBTLMNG_F2_data
        Public data5 As IODBTLMNG_F2_data
    End Structure ' In case that the number of group is 5 

    ' cnc_wrtool2:write of individual data of tool management data 
    <StructLayout(LayoutKind.Explicit)> _
    Public Structure IDBTLM_item
        <FieldOffset( 0 )> _
        Public data1 As SByte
        <FieldOffset( 0 )> _
        Public data2 As Short
        <FieldOffset( 0 )> _
        Public data4 As Integer
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IDBTLM
        Public data_id As Short
        Public item As IDBTLM_item
    End Structure

    ' cnc_regmagazine:new registration of magazine management data 
    ' cnc_rdmagazine:lead of magazine management data 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBTLMAG_data
        Public magazine As Short
        Public pot As Short
        Public tool_index As Short
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBTLMAG
        Public data1 As IODBTLMAG_data
        Public data2 As IODBTLMAG_data
        Public data3 As IODBTLMAG_data
        Public data4 As IODBTLMAG_data
        Public data5 As IODBTLMAG_data
    End Structure ' In case that the number of group is 5 

    ' cnc_delmagazine:deletion of magazine management data 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBTLMAG2_data
        Public magazine As Short
        Public pot As Short
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBTLMAG2
        Public data1 As IODBTLMAG2_data
        Public data2 As IODBTLMAG2_data
        Public data3 As IODBTLMAG2_data
        Public data4 As IODBTLMAG2_data
        Public data5 As IODBTLMAG2_data
    End Structure ' In case that the number of group is 5 


'-------------------------------------
' CNC: Operation history data related 
'-------------------------------------

    ' cnc_rdophistry:read operation history data 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure REC_ALM
        Public rec_type As Short  ' record type 
        Public alm_grp As Short   ' alarm group 
        Public alm_no As Short    ' alarm number 
        Public axis_no As SByte   ' axis number 
        Public dummy As SByte
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure REC_MDI
        Public rec_type As Short  ' record type 
        Public key_code As Byte   ' key code 
        Public pw_flag As Byte    ' power on flag 
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=4)> _
        Public dummy As SByte()
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure REC_SGN
        Public rec_type As Short  ' record type 
        Public sig_name As SByte  ' signal name 
        Public sig_old As Byte    ' old signal bit pattern 
        Public sig_new As Byte    ' new signal bit pattern 
        Public dummy As SByte
        Public sig_no As Short    ' signal number 
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure REC_DATE
        Public rec_type As Short  ' record type 
        Public year As SByte       ' year 
        Public month As SByte      ' month 
        Public day As SByte        ' day 
        Public pw_flag As SByte    ' power on flag 
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=2)> _
        Public dummy As SByte()
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure REC_TIME
        Public rec_type As Short  ' record flag 
        Public hour As SByte       ' hour 
        Public minute As SByte     ' minute 
        Public second As SByte     ' second 
        Public pw_flag As SByte    ' power on flag 
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=2)> _
        Public dummy As SByte()
    End Structure
    <StructLayout(LayoutKind.Explicit,Size:=8)> _
    Public Structure ODBHIS_data
        ' record type
        <FieldOffset( 0 )> _
        Public rec_type As Short  ' record type 

        ' alarm record
        <FieldOffset( 0 )> _
        Public alm_rec_type As Short  ' record type 
        <FieldOffset( 2 )> _
        Public alm_alm_grp As Short   ' alarm group 
        <FieldOffset( 4 )> _
        Public alm_alm_no As Short    ' alarm number 
        <FieldOffset( 6 )> _
        Public alm_axis_no As SByte    ' axis number 
        <FieldOffset( 7 )> _
        Public alm_dummy As SByte

        ' mdi record
        <FieldOffset( 0 )> _
        Public mdi_rec_type As Short  ' record type 
        <FieldOffset( 2 )> _
        Public mdi_key_code As Byte   ' key code 
        <FieldOffset( 3 )> _
        Public mdi_pw_flag As Byte    ' power on flag 
        <FieldOffset( 4 )> _
        Public mdi_dummy1 As SByte
        <FieldOffset( 5 )> _
        Public mdi_dummy2 As SByte
        <FieldOffset( 6 )> _
        Public mdi_dummy3 As SByte
        <FieldOffset( 7 )> _
        Public mdi_dummy4 As SByte

        ' sign record
        <FieldOffset( 0 )> _
        Public sgn_rec_type As Short  ' record type 
        <FieldOffset( 2 )> _
        Public sgn_sig_name As SByte  ' signal name 
        <FieldOffset( 3 )> _
        Public sgn_sig_old As Byte   ' old signal bit pattern 
        <FieldOffset( 4 )> _
        Public sgn_sig_new As Byte    ' new signal bit pattern 
        <FieldOffset( 5 )> _
        Public sgn_dummy As SByte
        <FieldOffset( 6 )> _
        Public sgn_sig_no As Short    ' signal number 

        ' date record
        <FieldOffset( 0 )> _
        Public date_rec_type As Short  ' record type 
        <FieldOffset( 2 )> _
        Public date_year As SByte       ' year 
        <FieldOffset( 3 )> _
        Public date_month As SByte      ' month 
        <FieldOffset( 4 )> _
        Public date_day As SByte        ' day 
        <FieldOffset( 5 )> _
        Public date_pw_flag As SByte    ' power on flag 
        <FieldOffset( 6 )> _
        Public date_dummy1 As SByte
        <FieldOffset( 7 )> _
        Public date_dummy2 As SByte

        ' time record
        <FieldOffset( 0 )> _
        Public time_rec_type As Short  ' record flag 
        <FieldOffset( 2 )> _
        Public time_hour As SByte       ' hour 
        <FieldOffset( 3 )> _
        Public time_minute As SByte     ' minute 
        <FieldOffset( 4 )> _
        Public time_second As SByte     ' second 
        <FieldOffset( 5 )> _
        Public time_pw_flag As SByte    ' power on flag 
        <FieldOffset( 6 )> _
        Public time_dummy1 As SByte
        <FieldOffset( 7 )> _
        Public time_dummy2 As SByte
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBHIS1
        Public data1 As ODBHIS_data
        Public data2 As ODBHIS_data
        Public data3 As ODBHIS_data
        Public data4 As ODBHIS_data
        Public data5 As ODBHIS_data
        Public data6 As ODBHIS_data
        Public data7 As ODBHIS_data
        Public data8 As ODBHIS_data
        Public data9 As ODBHIS_data
        Public data10 As ODBHIS_data
    End Structure ' In case that the number of data is 10 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBHIS
        Public s_no As Short  ' start number  C# ushort
        Public type As Short  ' dummy 
        Public e_no As Short  ' end number  C# ushort
        Public data As ODBHIS1
    End Structure

    ' cnc_rdophistry2:read operation history data 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure REC_MDI2
        Public key_code As Byte   ' key code 
        Public pw_flag As Byte    ' power on flag 
        Public dummy As Short
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure REC_MDI2_data
        Public rec_len As Short   ' length 
        Public rec_type As Short  ' record type 
        Public data As REC_MDI2
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure REC_SGN2
        Public sig_name As Short  ' signal name 
        Public sig_no As Short    ' signal number 
        Public sig_old As Byte   ' old signal bit pattern 
        Public sig_new As Byte   ' new signal bit pattern 
        Public dummy As Short
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure REC_SGN2_data
        Public rec_len As Short   ' length 
        Public rec_type As Short  ' record type 
        Public data As REC_SGN2
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure REC_ALM2
        Public alm_grp As Short   ' alarm group 
        Public alm_no As Short    ' alarm number 
        Public axis_no As Short   ' axis number 
        Public year As Short      ' year 
        Public month As Short     ' month 
        Public day As Short       ' day 
        Public hour As Short      ' hour 
        Public minute As Short    ' minute 
        Public second As Short    ' second 
        Public dummy As Short
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure REC_ALM2_data
        Public rec_len As Short   ' length 
        Public rec_type As Short  ' record type 
        Public data As REC_ALM2
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure REC_DATE2
        Public evnt_type As Short ' event type 
        Public year As Short      ' year 
        Public month As Short     ' month 
        Public day As Short       ' day 
        Public hour As Short      ' hour 
        Public minute As Short    ' minute 
        Public second As Short    ' second 
        Public dummy As Short
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure REC_DATE2_data
        Public rec_len As Short   ' length 
        Public rec_type As Short  ' record type 
        Public data As REC_DATE2
    End Structure
    <StructLayout(LayoutKind.Explicit)> _
    Public Structure ODBOPHIS
        <FieldOffset( 0 )> _
        Public rec_mdi As REC_MDI2_data
        <FieldOffset( 0 )> _
        Public rec_sgn As REC_SGN2_data
        <FieldOffset( 0 )> _
        Public rec_alm As REC_ALM2_data
        <FieldOffset( 0 )> _
        Public rec_date As REC_DATE2_data
    End Structure

    ' cnc_rdophistry4:read operation history data
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure REC_MDI4
        Public key_code As Char     ' key code
        Public pw_flag  As Char     ' power on flag
        Public pth_no   As Short    ' path index
        Public ex_flag  As Short    ' kxternal key flag
        Public hour     As Short    ' hour
        Public minute   As Short    ' minute
        Public second   As Short    ' second
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure REC_MDI4_data
        Public rec_len  As Short   ' length 
        Public rec_type As Short   ' record type 
        Public data     As REC_MDI4
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure REC_SGN4
        Public sig_name As Short    ' signal name
        Public sig_no   As Short    ' signal number
        Public sig_old  As Char     ' old signal bit pattern
        Public sig_new  As Char     ' new signal bit pattern
        Public pmc_no   As Short    ' pmc index
        Public hour     As Short    ' hour
        Public minute   As Short    ' minute
        Public second   As Short    ' second
        Public dummy    As Short
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure REC_SGN4_data
        Public rec_len  As Short   ' length 
        Public rec_type As Short   ' record type 
        Public data     As REC_SGN4
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure REC_ALM4
        Public alm_grp  As Short    ' alarm group
        Public alm_no   As Short    ' alarm number
        Public axis_no  As Short    ' axis number
        Public year     As Short    ' year
        Public month    As Short    ' month
        Public day      As Short    ' day
        Public hour     As Short    ' hour
        Public minute   As Short    ' minute
        Public second   As Short    ' second
        Public pth_no   As Short    ' path index
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure REC_ALM4_data
        Public rec_len  As Short   ' length 
        Public rec_type As Short   ' record type 
        Public data     As REC_ALM4
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure REC_DATE4
        Public evnt_type As Short    ' event type
        Public year      As Short    ' year
        Public month     As Short    ' month
        Public day       As Short    ' day
        Public hour      As Short    ' hour
        Public minute    As Short    ' minute
        Public second    As Short    ' second
        Public dummy     As Short
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure REC_DATE4_data
        Public rec_len  As Short   ' length 
        Public rec_type As Short   ' record type 
        Public data     As REC_DATE4
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure REC_IAL4
        Public alm_grp  As Short    ' alarm group
        Public alm_no   As Short    ' alarm number
        Public axis_no  As Short    ' axis number
        Public year     As Short    ' year
        Public month    As Short    ' month
        Public day      As Short    ' day
        Public hour     As Short    ' hour
        Public minute   As Short    ' minute
        Public second   As Short    ' second
        Public pth_no   As Short    ' path index
        Public sys_alm  As Short    ' sys alarm
        Public dsp_flg  As Short    ' message dsp flag
        Public axis_num As Short    ' axis num
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=10)> _
        Public g_modal  As Integer() ' G code Modal
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=10)> _
        Public g_dp     As Char()    ' #7:1 Block  #6Å`#0 dp
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=10)> _
        Public a_modal  As Integer() ' B,D,E,F,H,M,N,O,S,T code Modal
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=10)> _
        Public a_dp     As Char()    ' #7:1 Block  #6Å`#0 dp
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=32)> _
        Public abs_pos  As Integer() ' Abs pos
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=32)> _
        Public abs_dp   As Char()    ' Abs dp
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=32)> _
        Public mcn_pos  As Integer() ' Mcn pos
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=32)> _
        Public mcn_dp   As Char()    ' Mcn dp
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure REC_IAL4_data
        Public rec_len  As Short   ' length 
        Public rec_type As Short   ' record type 
        Public data     As REC_IAL4
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure REC_MAL4
        Public alm_grp  As Short     ' alarm group
        Public alm_no   As Short     ' alarm number
        Public axis_no  As Short     ' axis number
        Public year     As Short     ' year
        Public month    As Short     ' month
        Public day      As Short     ' day
        Public hour     As Short     ' hour
        Public minute   As Short     ' minute
        Public second   As Short     ' second
        Public pth_no   As Short     ' path index
        Public sys_alm  As Short     ' sys alarm
        Public dsp_flg  As Short     ' message dsp flag
        Public axis_num As Short     ' axis num
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=64)> _
        Public alm_msg  As Char()    ' alarm message 
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=10)> _
        Public g_modal  As Integer() ' G code Modal
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=10)> _
        Public g_dp     As Char()    ' #7:1 Block  #6Å`#0 dp
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=10)> _
        Public a_modal  As Integer() ' B,D,E,F,H,M,N,O,S,T code Modal
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=10)> _
        Public a_dp     As Char()    ' #7:1 Block  #6Å`#0 dp
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=32)> _
        Public abs_pos  As Integer() ' Abs pos
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=32)> _
        Public abs_dp   As Char()    ' Abs dp
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=32)> _
        Public mcn_pos  As Integer() ' Mcn pos
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=32)> _
        Public mcn_dp   As Char()    ' Mcn dp
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure REC_MAL4_data
        Public rec_len  As Short   ' length 
        Public rec_type As Short   ' record type 
        Public data     As REC_MAL4
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure REC_OPM4
        Public dsp_flg  As Short     ' Dysplay flag(ON/OFF)
        Public om_no    As Short     ' message number
        Public year     As Short     ' year
        Public month    As Short     ' month
        Public day      As Short     ' day
        Public hour     As Short     ' hour
        Public minute   As Short     ' minute
        Public second   As Short     ' second
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=256)> _
        Public ope_msg  As Char()    ' alarm message 
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure REC_OPM4_data
        Public rec_len  As Short   ' length 
        Public rec_type As Short   ' record type 
        Public data     As REC_OPM4
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure REC_OFS4
        Public ofs_grp  As Short     ' Tool offset group
        Public ofs_no   As Short     ' Tool offset number
        Public hour     As Short     ' hour
        Public minute   As Short     ' minute
        Public second   As Short     ' second
        Public pth_no   As Short     ' path index
        Public ofs_old  As Integer   ' old data
        Public ofs_new  As Integer   ' new data
        Public old_dp   As Short     ' old data decimal point
        Public new_dp   As Short     ' new data decimal point
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure REC_OFS4_data
        Public rec_len  As Short   ' length 
        Public rec_type As Short   ' record type 
        Public data     As REC_OFS4
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure REC_PRM4
        Public prm_grp  As Short     ' paramater group
        Public prm_num  As Short     ' paramater number
        Public hour     As Short     ' hour
        Public minute   As Short     ' minute
        Public second   As Short     ' second
        Public prm_len  As Short     ' paramater data length
        Public prm_no   As Integer   ' paramater no
        Public prm_old  As Integer   ' old data
        Public prm_new  As Integer   ' new data
        Public old_dp   As Short     ' old data decimal point
        Public new_dp   As Short     ' new data decimal point
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure REC_PRM4_data
        Public rec_len  As Short   ' length 
        Public rec_type As Short   ' record type 
        Public data     As REC_PRM4
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure REC_WOF4
        Public ofs_grp  As Short     ' Work offset group
        Public ofs_no   As Short     ' Work offset number
        Public hour     As Short     ' hour
        Public minute   As Short     ' minute
        Public second   As Short     ' second
        Public pth_no   As Short     ' path index
        Public axis_no  As Short     ' path axis num
        Public dummy    As Short
        Public ofs_old  As Integer   ' old data
        Public ofs_new  As Integer   ' new data
        Public old_dp   As Short     ' old data decimal point
        Public new_dp   As Short     ' new data decimal point
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure REC_WOF4_data
        Public rec_len  As Short   ' length 
        Public rec_type As Short   ' record type 
        Public data     As REC_WOF4
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure REC_MAC4
        Public mac_no   As Short     ' macro val number
        Public hour     As Short     ' hour
        Public minute   As Short     ' minute
        Public second   As Short     ' second
        Public pth_no   As Short     ' path index
        Public mac_old  As Integer   ' old data
        Public mac_new  As Integer   ' new data
        Public old_dp   As Short     ' old data decimal point
        Public new_dp   As Short     ' new data decimal point
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure REC_MAC4_data
        Public rec_len  As Short   ' length 
        Public rec_type As Short   ' record type 
        Public data     As REC_MAC4
    End Structure

    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBOPHIS4_1
        Public rec_mdi1  As REC_MDI4_data
        Public rec_mdi2  As REC_MDI4_data
        Public rec_mdi3  As REC_MDI4_data
        Public rec_mdi4  As REC_MDI4_data
        Public rec_mdi5  As REC_MDI4_data
        Public rec_mdi6  As REC_MDI4_data
        Public rec_mdi7  As REC_MDI4_data
        Public rec_mdi8  As REC_MDI4_data
        Public rec_mdi9  As REC_MDI4_data
        Public rec_mdi10 As REC_MDI4_data
    End Structure ' In case that the number of data is 10

    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBOPHIS4_2
        Public rec_sgn1  As REC_SGN4_data
        Public rec_sgn2  As REC_SGN4_data
        Public rec_sgn3  As REC_SGN4_data
        Public rec_sgn4  As REC_SGN4_data
        Public rec_sgn5  As REC_SGN4_data
        Public rec_sgn6  As REC_SGN4_data
        Public rec_sgn7  As REC_SGN4_data
        Public rec_sgn8  As REC_SGN4_data
        Public rec_sgn9  As REC_SGN4_data
        Public rec_sgn10 As REC_SGN4_data
    End Structure ' In case that the number of data is 10

    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBOPHIS4_3
        Public rec_alm1  As REC_ALM4_data
        Public rec_alm2  As REC_ALM4_data
        Public rec_alm3  As REC_ALM4_data
        Public rec_alm4  As REC_ALM4_data
        Public rec_alm5  As REC_ALM4_data
        Public rec_alm6  As REC_ALM4_data
        Public rec_alm7  As REC_ALM4_data
        Public rec_alm8  As REC_ALM4_data
        Public rec_alm9  As REC_ALM4_data
        Public rec_alm10 As REC_ALM4_data
    End Structure ' In case that the number of data is 10

    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBOPHIS4_4
        Public rec_date1  As REC_DATE4_data
        Public rec_date2  As REC_DATE4_data
        Public rec_date3  As REC_DATE4_data
        Public rec_date4  As REC_DATE4_data
        Public rec_date5  As REC_DATE4_data
        Public rec_date6  As REC_DATE4_data
        Public rec_date7  As REC_DATE4_data
        Public rec_date8  As REC_DATE4_data
        Public rec_date9  As REC_DATE4_data
        Public rec_date10 As REC_DATE4_data
    End Structure ' In case that the number of data is 10

    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBOPHIS4_5
        Public rec_ial1  As REC_IAL4_data
        Public rec_ial2  As REC_IAL4_data
        Public rec_ial3  As REC_IAL4_data
        Public rec_ial4  As REC_IAL4_data
        Public rec_ial5  As REC_IAL4_data
        Public rec_ial6  As REC_IAL4_data
        Public rec_ial7  As REC_IAL4_data
        Public rec_ial8  As REC_IAL4_data
        Public rec_ial9  As REC_IAL4_data
        Public rec_ial10 As REC_IAL4_data
    End Structure ' In case that the number of data is 10

    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBOPHIS4_6
        Public rec_mal1  As REC_MAL4_data
        Public rec_mal2  As REC_MAL4_data
        Public rec_mal3  As REC_MAL4_data
        Public rec_mal4  As REC_MAL4_data
        Public rec_mal5  As REC_MAL4_data
        Public rec_mal6  As REC_MAL4_data
        Public rec_mal7  As REC_MAL4_data
        Public rec_mal8  As REC_MAL4_data
        Public rec_mal9  As REC_MAL4_data
        Public rec_mal10 As REC_MAL4_data
    End Structure ' In case that the number of data is 10

    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBOPHIS4_7
        Public rec_opm1  As REC_OPM4_data
        Public rec_opm2  As REC_OPM4_data
        Public rec_opm3  As REC_OPM4_data
        Public rec_opm4  As REC_OPM4_data
        Public rec_opm5  As REC_OPM4_data
        Public rec_opm6  As REC_OPM4_data
        Public rec_opm7  As REC_OPM4_data
        Public rec_opm8  As REC_OPM4_data
        Public rec_opm9  As REC_OPM4_data
        Public rec_opm10 As REC_OPM4_data
    End Structure ' In case that the number of data is 10

    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBOPHIS4_8
        Public rec_ofs1  As REC_OFS4_data
        Public rec_ofs2  As REC_OFS4_data
        Public rec_ofs3  As REC_OFS4_data
        Public rec_ofs4  As REC_OFS4_data
        Public rec_ofs5  As REC_OFS4_data
        Public rec_ofs6  As REC_OFS4_data
        Public rec_ofs7  As REC_OFS4_data
        Public rec_ofs8  As REC_OFS4_data
        Public rec_ofs9  As REC_OFS4_data
        Public rec_ofs10 As REC_OFS4_data
    End Structure ' In case that the number of data is 10

    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBOPHIS4_9
        Public rec_prm1  As REC_PRM4_data
        Public rec_prm2  As REC_PRM4_data
        Public rec_prm3  As REC_PRM4_data
        Public rec_prm4  As REC_PRM4_data
        Public rec_prm5  As REC_PRM4_data
        Public rec_prm6  As REC_PRM4_data
        Public rec_prm7  As REC_PRM4_data
        Public rec_prm8  As REC_PRM4_data
        Public rec_prm9  As REC_PRM4_data
        Public rec_prm10 As REC_PRM4_data
    End Structure ' In case that the number of data is 10

    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBOPHIS4_10
        Public rec_wof1  As REC_WOF4_data
        Public rec_wof2  As REC_WOF4_data
        Public rec_wof3  As REC_WOF4_data
        Public rec_wof4  As REC_WOF4_data
        Public rec_wof5  As REC_WOF4_data
        Public rec_wof6  As REC_WOF4_data
        Public rec_wof7  As REC_WOF4_data
        Public rec_wof8  As REC_WOF4_data
        Public rec_wof9  As REC_WOF4_data
        Public rec_wof10 As REC_WOF4_data
    End Structure ' In case that the number of data is 10

    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBOPHIS4_11
        Public rec_mac1 As REC_MAC4_data
        Public rec_mac2 As REC_MAC4_data
        Public rec_mac3 As REC_MAC4_data
        Public rec_mac4 As REC_MAC4_data
        Public rec_mac5 As REC_MAC4_data
        Public rec_mac6 As REC_MAC4_data
        Public rec_mac7 As REC_MAC4_data
        Public rec_mac8 As REC_MAC4_data
        Public rec_mac9 As REC_MAC4_data
        Public rec_mac10 As REC_MAC4_data
    End Structure ' In case that the number of data is 10

    ' cnc_rdalmhistry:read alarm history data 
    <StructLayout(LayoutKind.Sequential,CharSet:=CharSet.Ansi,Pack:=4)> _
    Public Structure ALM_HIS_data
        Public dummy As Short
        Public alm_grp As Short       ' alarm group 
        Public alm_no As Short        ' alarm number 
        Public axis_no As Byte        ' axis number 
        Public year As Byte           ' year 
        Public month As Byte          ' month 
        Public day As Byte            ' day 
        Public hour As Byte           ' hour 
        Public minute As Byte         ' minute 
        Public second As Byte         ' second 
        Public dummy2 As Byte
        Public len_msg As Short       ' alarm message length 
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=32)> _
        Public alm_msg As String      ' alarm message 
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ALM_HIS1
        Public data1 As ALM_HIS_data
        Public data2 As ALM_HIS_data
        Public data3 As ALM_HIS_data
        Public data4 As ALM_HIS_data
        Public data5 As ALM_HIS_data
        Public data6 As ALM_HIS_data
        Public data7 As ALM_HIS_data
        Public data8 As ALM_HIS_data
        Public data9 As ALM_HIS_data
        Public data10 As ALM_HIS_data
    End Structure ' In case that the number of data is 10 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBAHIS
        Public s_no As Short  ' start number  C# ushort
        Public type As Short  ' dummy 
        Public e_no As Short  ' end number  C# ushort
        Public alm_his As ALM_HIS1
    End Structure

    ' cnc_rdalmhistry2:read alarm history data 
    <StructLayout(LayoutKind.Sequential,CharSet:=CharSet.Ansi,Pack:=4)> _
    Public Structure ALM_HIS2_data
        Public alm_grp As Short       ' alarm group 
        Public alm_no As Short        ' alarm number 
        Public axis_no As Short       ' axis number 
        Public year As Short          ' year 
        Public month As Short         ' month 
        Public day As Short           ' day 
        Public hour As Short          ' hour 
        Public minute As Short        ' minute 
        Public second As Short        ' second 
        Public len_msg As Short       ' alarm message length 
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=32)> _
        Public alm_msg As String      ' alarm message 
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ALM_HIS2
        Public data1 As ALM_HIS2_data
        Public data2 As ALM_HIS2_data
        Public data3 As ALM_HIS2_data
        Public data4 As ALM_HIS2_data
        Public data5 As ALM_HIS2_data
        Public data6 As ALM_HIS2_data
        Public data7 As ALM_HIS2_data
        Public data8 As ALM_HIS2_data
        Public data9 As ALM_HIS2_data
        Public data10 As ALM_HIS2_data
    End Structure ' In case that the number of data is 10 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBAHIS2
        Public s_no As Short  ' start number  C# ushort
        Public e_no As Short  ' end number  C# ushort
        Public alm_his As ALM_HIS2
    End Structure

    ' cnc_rdalmhistry3:read alarm history data 
    <StructLayout(LayoutKind.Sequential,CharSet:=CharSet.Ansi,Pack:=4)> _
    Public Structure ALM_HIS3_data
        Public alm_grp As Short       ' alarm group 
        Public alm_no As Short        ' alarm number 
        Public axis_no As Short       ' axis number 
        Public year As Short          ' year 
        Public month As Short         ' month 
        Public day As Short           ' day 
        Public hour As Short          ' hour 
        Public minute As Short        ' minute 
        Public second As Short        ' second 
        Public len_msg As Short       ' alarm message length 
        Public pth_no As Short        ' path index
        Public dummy As Short
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=32)> _
        Public alm_msg As String      ' alarm message 
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ALM_HIS3
        Public data1 As ALM_HIS3_data
        Public data2 As ALM_HIS3_data
        Public data3 As ALM_HIS3_data
        Public data4 As ALM_HIS3_data
        Public data5 As ALM_HIS3_data
        Public data6 As ALM_HIS3_data
        Public data7 As ALM_HIS3_data
        Public data8 As ALM_HIS3_data
        Public data9 As ALM_HIS3_data
        Public data10 As ALM_HIS3_data
    End Structure ' In case that the number of data is 10 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBAHIS3
        Public s_no As Short  ' start number  C# ushort
        Public e_no As Short  ' end number  C# ushort
        Public alm_his As ALM_HIS3
    End Structure

    ' cnc_rdalmhistry5:read alarm history data 
    <StructLayout(LayoutKind.Sequential,CharSet:=CharSet.Ansi,Pack:=4)> _
    Public Structure ALM_HIS5_data
        Public alm_grp As Short       ' alarm group 
        Public alm_no As Short        ' alarm number 
        Public axis_no As Short       ' axis number 
        Public year As Short          ' year 
        Public month As Short         ' month 
        Public day As Short           ' day 
        Public hour As Short          ' hour 
        Public minute As Short        ' minute 
        Public second As Short        ' second 
        Public len_msg As Short       ' alarm message length 
        Public pth_no As Short        ' path index
        Public sys_alm As Short       ' sys alarm
        Public dsp_flg As Short       ' message dsp flag
        Public axis_num As Short      ' sum axis num
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=64)> _
        Public alm_msg As String      ' alarm message
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=10)> _
        Public g_modal As Integer()   ' G code Modal
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=10)> _
        Public g_dp As Char()         ' #7:1 Block  #6Å`#0 dp
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=10)> _
        Public a_modal As Integer()   ' B,D,E,F,H,M,N,O,S,T code Modal
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=10)> _
        Public a_dp As Char()         ' #7:1 Block  #6Å`#0 dp
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=32)> _
        Public abs_pos As Integer()   ' Abs pos
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=32)> _
        Public abs_dp As Char()       ' Abs dp
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=32)> _
        Public mcn_pos As Integer()   ' Mcn pos
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=32)> _
        Public mcn_dp As Char()       ' Mcn dp
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ALM_HIS5
        Public data1 As ALM_HIS5_data
        Public data2 As ALM_HIS5_data
        Public data3 As ALM_HIS5_data
        Public data4 As ALM_HIS5_data
        Public data5 As ALM_HIS5_data
        Public data6 As ALM_HIS5_data
        Public data7 As ALM_HIS5_data
        Public data8 As ALM_HIS5_data
        Public data9 As ALM_HIS5_data
        Public data10 As ALM_HIS5_data
    End Structure ' In case that the number of data is 10 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBAHIS5
        Public s_no As Short  ' start number  C# ushort
        Public e_no As Short  ' end number  C# ushort
        Public alm_his As ALM_HIS5
    End Structure

    ' cnc_rdomhistry2:read operater message history data
    <StructLayout(LayoutKind.Sequential,CharSet:=CharSet.Ansi,Pack:=4)> _
    Public Structure ODBOMHIS2_data
        Public dsp_flg As Short  ' Dysplay flag(ON/OFF)
        Public om_no As Short    ' operater message number
        Public year As Short     ' year
        Public month As Short    ' month
        Public day As Short      ' day
        Public hour As Short     ' Hour
        Public minute As Short   ' Minute
        Public second As Short   ' Second 
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=256)> _
        Public alm_msg As String ' Messege
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure OPM_HIS
        Public data1  As ODBOMHIS2_data
        Public data2  As ODBOMHIS2_data
        Public data3  As ODBOMHIS2_data
        Public data4  As ODBOMHIS2_data
        Public data5  As ODBOMHIS2_data
        Public data6  As ODBOMHIS2_data
        Public data7  As ODBOMHIS2_data
        Public data8  As ODBOMHIS2_data
        Public data9  As ODBOMHIS2_data
        Public data10 As ODBOMHIS2_data
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBOMHIS2
        Public s_no    As Short   ' start number
        Public e_no    As Short   ' end number
        Public opm_his As OPM_HIS
    End Structure

    ' cnc_rdhissgnl:read signals related operation history 
    ' cnc_wrhissgnl:write signals related operation history 
    <StructLayout(LayoutKind.Sequential,CharSet:=CharSet.Ansi,Pack:=4)> _
    Public Structure IODBSIG_data
        Public ent_no As Short    ' entry number 
        Public sig_no As Short    ' signal number 
        Public sig_name As Byte  ' signal name 
        Public mask_pat As Byte  ' signal mask pattern 
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBSIG1
        Public data1 As IODBSIG_data
        Public data2 As IODBSIG_data
        Public data3 As IODBSIG_data
        Public data4 As IODBSIG_data
        Public data5 As IODBSIG_data
        Public data6 As IODBSIG_data
        Public data7 As IODBSIG_data
        Public data8 As IODBSIG_data
        Public data9 As IODBSIG_data
        Public data10 As IODBSIG_data
        Public data11 As IODBSIG_data
        Public data12 As IODBSIG_data
        Public data13 As IODBSIG_data
        Public data14 As IODBSIG_data
        Public data15 As IODBSIG_data
        Public data16 As IODBSIG_data
        Public data17 As IODBSIG_data
        Public data18 As IODBSIG_data
        Public data19 As IODBSIG_data
        Public data20 As IODBSIG_data
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBSIG
        Public datano As Short' dummy 
        Public type As Short  ' dummy 
        Public data As IODBSIG1
    End Structure

    ' cnc_rdhissgnl2:read signals related operation history 2
    ' cnc_wrhissgnl2:write signals related operation history 2
    <StructLayout(LayoutKind.Sequential,CharSet:=CharSet.Ansi,Pack:=4)> _
    Public Structure _IODBSIG2_data
        Public ent_no As Short    ' entry number 
        Public sig_no As Short    ' signal number 
        Public sig_name As Byte   ' signal name 
        Public mask_pat As Byte   ' signal mask pattern 
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBSIG2_data
        Public data1  As _IODBSIG2_data
        Public data2  As _IODBSIG2_data
        Public data3  As _IODBSIG2_data
        Public data4  As _IODBSIG2_data
        Public data5  As _IODBSIG2_data
        Public data6  As _IODBSIG2_data
        Public data7  As _IODBSIG2_data
        Public data8  As _IODBSIG2_data
        Public data9  As _IODBSIG2_data
        Public data10 As _IODBSIG2_data
        Public data11 As _IODBSIG2_data
        Public data12 As _IODBSIG2_data
        Public data13 As _IODBSIG2_data
        Public data14 As _IODBSIG2_data
        Public data15 As _IODBSIG2_data
        Public data16 As _IODBSIG2_data
        Public data17 As _IODBSIG2_data
        Public data18 As _IODBSIG2_data
        Public data19 As _IODBSIG2_data
        Public data20 As _IODBSIG2_data
        Public data21 As _IODBSIG2_data
        Public data22 As _IODBSIG2_data
        Public data23 As _IODBSIG2_data
        Public data24 As _IODBSIG2_data
        Public data25 As _IODBSIG2_data
        Public data26 As _IODBSIG2_data
        Public data27 As _IODBSIG2_data
        Public data28 As _IODBSIG2_data
        Public data29 As _IODBSIG2_data
        Public data30 As _IODBSIG2_data
        Public data31 As _IODBSIG2_data
        Public data32 As _IODBSIG2_data
        Public data33 As _IODBSIG2_data
        Public data34 As _IODBSIG2_data
        Public data35 As _IODBSIG2_data
        Public data36 As _IODBSIG2_data
        Public data37 As _IODBSIG2_data
        Public data38 As _IODBSIG2_data
        Public data39 As _IODBSIG2_data
        Public data40 As _IODBSIG2_data
        Public data41 As _IODBSIG2_data
        Public data42 As _IODBSIG2_data
        Public data43 As _IODBSIG2_data
        Public data44 As _IODBSIG2_data
        Public data45 As _IODBSIG2_data
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBSIG2
        Public datano As Short    ' dummy 
        Public type As Short      ' dummy 
        Public data As IODBSIG2_data
    End Structure

    ' cnc_rdhissgnl3:read signals related operation history
    ' cnc_wrhissgnl3:write signals related operation history
    <StructLayout(LayoutKind.Sequential,CharSet:=CharSet.Ansi,Pack:=4)> _
    Public Structure _IODBSIG3_data
        Public ent_no As Short    ' entry number 
        Public pmc_no As Short    ' pmc number 
        Public sig_no As Short    ' signal number
        Public sig_name As Byte   ' signal name 
        Public mask_pat As Byte   ' signal mask pattern 
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBSIG3_data
        Public data1  As _IODBSIG3_data
        Public data2  As _IODBSIG3_data
        Public data3  As _IODBSIG3_data
        Public data4  As _IODBSIG3_data
        Public data5  As _IODBSIG3_data
        Public data6  As _IODBSIG3_data
        Public data7  As _IODBSIG3_data
        Public data8  As _IODBSIG3_data
        Public data9  As _IODBSIG3_data
        Public data10 As _IODBSIG3_data
        Public data11 As _IODBSIG3_data
        Public data12 As _IODBSIG3_data
        Public data13 As _IODBSIG3_data
        Public data14 As _IODBSIG3_data
        Public data15 As _IODBSIG3_data
        Public data16 As _IODBSIG3_data
        Public data17 As _IODBSIG3_data
        Public data18 As _IODBSIG3_data
        Public data19 As _IODBSIG3_data
        Public data20 As _IODBSIG3_data
        Public data21 As _IODBSIG3_data
        Public data22 As _IODBSIG3_data
        Public data23 As _IODBSIG3_data
        Public data24 As _IODBSIG3_data
        Public data25 As _IODBSIG3_data
        Public data26 As _IODBSIG3_data
        Public data27 As _IODBSIG3_data
        Public data28 As _IODBSIG3_data
        Public data29 As _IODBSIG3_data
        Public data30 As _IODBSIG3_data
        Public data31 As _IODBSIG3_data
        Public data32 As _IODBSIG3_data
        Public data33 As _IODBSIG3_data
        Public data34 As _IODBSIG3_data
        Public data35 As _IODBSIG3_data
        Public data36 As _IODBSIG3_data
        Public data37 As _IODBSIG3_data
        Public data38 As _IODBSIG3_data
        Public data39 As _IODBSIG3_data
        Public data40 As _IODBSIG3_data
        Public data41 As _IODBSIG3_data
        Public data42 As _IODBSIG3_data
        Public data43 As _IODBSIG3_data
        Public data44 As _IODBSIG3_data
        Public data45 As _IODBSIG3_data
        Public data46 As _IODBSIG3_data
        Public data47 As _IODBSIG3_data
        Public data48 As _IODBSIG3_data
        Public data49 As _IODBSIG3_data
        Public data50 As _IODBSIG3_data
        Public data51 As _IODBSIG3_data
        Public data52 As _IODBSIG3_data
        Public data53 As _IODBSIG3_data
        Public data54 As _IODBSIG3_data
        Public data55 As _IODBSIG3_data
        Public data56 As _IODBSIG3_data
        Public data57 As _IODBSIG3_data
        Public data58 As _IODBSIG3_data
        Public data59 As _IODBSIG3_data
        Public data60 As _IODBSIG3_data
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBSIG3
        Public datano As Short' dummy 
        Public type As Short  ' dummy 
        Public data As IODBSIG3_data
    End Structure

'-------------
' CNC: Others 
'-------------

    ' cnc_sysinfo:read CNC system information 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBSYS
        Public addinfo As Short
        Public max_axis As Short
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=2)> _
        Public cnc_type As Char()
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=2)> _
        Public mt_type As Char()
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=4)> _
        Public series As Char()
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=4)> _
        Public version As Char()
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=2)> _
        Public axes As Char()
    End Structure

#If FS15D Then
    ' cnc_statinfo:read CNC status information 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBST
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=2)> _
        Public dummy As Short()     ' dummy                    
        Public aut As Short       ' selected automatic mode  
        Public manual As Short    ' selected manual mode     
        Public run As Short       ' running status           
        Public edit As Short      ' editting status          
        Public motion As Short    ' axis, dwell status       
        Public mstb As Short      ' m, s, t, b status        
        Public emergency As Short ' emergency stop status    
        Public write As Short     ' writting status          
        Public labelskip As Short ' label skip status        
        Public alarm As Short     ' alarm status             
        Public warning As Short   ' warning status           
        Public battery As Short   ' battery status           
    End Structure
#Else
    ' cnc_statinfo:read CNC status information 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBST
        Public dummy As Short     ' dummy 
        Public tmmode As Short    ' T/M mode 
        Public aut As Short       ' selected automatic mode 
        Public run As Short       ' running status 
        Public motion As Short    ' axis, dwell status 
        Public mstb As Short      ' m, s, t, b status 
        Public emergency As Short ' emergency stop status 
        Public alarm As Short     ' alarm status 
        Public edit As Short      ' editting status 
    End Structure
#End If

    ' cnc_alarm:read alarm status 
    <StructLayout(LayoutKind.Sequential, Pack:=4)> _
    Public Structure ODBALM
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=2)> _
        Public dummy As Short()
        Public data As Short  ' C# ushort
    End Structure

    ' cnc_rdalminfo:read alarm information 
#If M_AXIS2 Then
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ALMINFO1_data
        Public axis As Integer
        Public alm_no As Short
    End Structure
    
    <StructLayout(LayoutKind.Sequential,CharSet:=CharSet.Ansi,Pack:=4)> _
    Public Structure ALMINFO2_data
        Public axis As Integer
        Public alm_no As Short
        Public msg_len As Short
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=32)> _
        Public alm_msg As String   ' alarm message 
    End Structure
#Else
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ALMINFO1_data
        Public axis As Short
        Public alm_no As Short
    End Structure

    <StructLayout(LayoutKind.Sequential,CharSet:=CharSet.Ansi,Pack:=4)> _
    Public Structure ALMINFO2_data
        Public axis As Short
        Public alm_no As Short
        Public msg_len As Short
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=32)> _
        Public alm_msg As String   ' alarm message 
    End Structure
#End If
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ALMINFO_1
        Public msg1 As ALMINFO1_data
        Public msg2 As ALMINFO1_data
        Public msg3 As ALMINFO1_data
        Public msg4 As ALMINFO1_data
        Public msg5 As ALMINFO1_data
        Public data_end As Short
    End Structure ' In case that the number of alarm is 5 

    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ALMINFO_2
        Public msg1 As ALMINFO2_data
        Public msg2 As ALMINFO2_data
        Public msg3 As ALMINFO2_data
        Public msg4 As ALMINFO2_data
        Public msg5 As ALMINFO2_data
        Public dataend As Short
    End Structure ' In case that the number of alarm is 5 

    ' cnc_rdalmmsg:read alarm messages 
    <StructLayout(LayoutKind.Sequential,CharSet:=CharSet.Ansi,Pack:=4)> _
    Public Structure ODBALMMSG_data
        Public alm_no As Integer
        Public type As Short
        Public axis As Short
        Public dummy As Short
        Public msg_len As Short
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=32)> _
        Public alm_msg As String   ' alarm message 
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBALMMSG
        Public msg1 As ODBALMMSG_data
        Public msg2 As ODBALMMSG_data
        Public msg3 As ODBALMMSG_data
        Public msg4 As ODBALMMSG_data
        Public msg5 As ODBALMMSG_data
        Public msg6 As ODBALMMSG_data
        Public msg7 As ODBALMMSG_data
        Public msg8 As ODBALMMSG_data
        Public msg9 As ODBALMMSG_data
        Public msg10 As ODBALMMSG_data
    End Structure ' In case that the number of alarm is 10 

    ' cnc_rdalmmsg2:read alarm messages 
    <StructLayout(LayoutKind.Sequential,CharSet:=CharSet.Ansi,Pack:=4)> _
    Public Structure ODBALMMSG2_data
        Public alm_no As Integer
        Public type As Short
        Public axis As Short
        Public dummy As Short
        Public msg_len As Short
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=64)> _
        Public alm_msg As String   ' alarm message 
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBALMMSG2
        Public msg1 As ODBALMMSG2_data
        Public msg2 As ODBALMMSG2_data
        Public msg3 As ODBALMMSG2_data
        Public msg4 As ODBALMMSG2_data
        Public msg5 As ODBALMMSG2_data
        Public msg6 As ODBALMMSG2_data
        Public msg7 As ODBALMMSG2_data
        Public msg8 As ODBALMMSG2_data
        Public msg9 As ODBALMMSG2_data
        Public msg10 As ODBALMMSG2_data
    End Structure ' In case that the number of alarm is 10 

    ' cnc_modal:read modal data 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure MODAL_AUX_data
        Public aux_data As Integer
        Public flag1 As Byte
        Public flag2 As Byte
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure MODAL_RAUX1_data
        Public data1 As MODAL_AUX_data
        Public data2 As MODAL_AUX_data
        Public data3 As MODAL_AUX_data
        Public data4 As MODAL_AUX_data
        Public data5 As MODAL_AUX_data
        Public data6 As MODAL_AUX_data
        Public data7 As MODAL_AUX_data
        Public data8 As MODAL_AUX_data
        Public data9 As MODAL_AUX_data
        Public data10 As MODAL_AUX_data
        Public data11 As MODAL_AUX_data
        Public data12 As MODAL_AUX_data
        Public data13 As MODAL_AUX_data
        Public data14 As MODAL_AUX_data
        Public data15 As MODAL_AUX_data
        Public data16 As MODAL_AUX_data
        Public data17 As MODAL_AUX_data
        Public data18 As MODAL_AUX_data
        Public data19 As MODAL_AUX_data
        Public data20 As MODAL_AUX_data
        Public data21 As MODAL_AUX_data
        Public data22 As MODAL_AUX_data
        Public data23 As MODAL_AUX_data
        Public data24 As MODAL_AUX_data
        Public data25 As MODAL_AUX_data
        Public data26 As MODAL_AUX_data
        Public data27 As MODAL_AUX_data
    End Structure
#If M_AXIS2 Then
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure MODAL_RAUX2_data
        Public data1 As MODAL_AUX_data
        Public data2 As MODAL_AUX_data
        Public data3 As MODAL_AUX_data
        Public data4 As MODAL_AUX_data
        Public data5 As MODAL_AUX_data
        Public data6 As MODAL_AUX_data
        Public data7 As MODAL_AUX_data
        Public data8 As MODAL_AUX_data
        Public data9 As MODAL_AUX_data
        Public data10 As MODAL_AUX_data
        Public data11 As MODAL_AUX_data
        Public data12 As MODAL_AUX_data
        Public data13 As MODAL_AUX_data
        Public data14 As MODAL_AUX_data
        Public data15 As MODAL_AUX_data
        Public data16 As MODAL_AUX_data
        Public data17 As MODAL_AUX_data
        Public data18 As MODAL_AUX_data
        Public data19 As MODAL_AUX_data
        Public data20 As MODAL_AUX_data
        Public data21 As MODAL_AUX_data
        Public data22 As MODAL_AUX_data
        Public data23 As MODAL_AUX_data
        Public data24 As MODAL_AUX_data
    End Structure
#Else
#If FS15D Then
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure MODAL_RAUX2_data
        Public data1 As MODAL_AUX_data
        Public data2 As MODAL_AUX_data
        Public data3 As MODAL_AUX_data
        Public data4 As MODAL_AUX_data
        Public data5 As MODAL_AUX_data
        Public data6 As MODAL_AUX_data
        Public data7 As MODAL_AUX_data
        Public data8 As MODAL_AUX_data
        Public data9 As MODAL_AUX_data
        Public data10 As MODAL_AUX_data
    End Structure
#Else
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure MODAL_RAUX2_data
        Public data1 As MODAL_AUX_data
        Public data2 As MODAL_AUX_data
        Public data3 As MODAL_AUX_data
        Public data4 As MODAL_AUX_data
        Public data5 As MODAL_AUX_data
        Public data6 As MODAL_AUX_data
        Public data7 As MODAL_AUX_data
        Public data8 As MODAL_AUX_data
    End Structure
#End If
#End If

    <StructLayout(LayoutKind.Explicit)> _
    Public Structure ODBMDL_1
        <FieldOffset( 0 )> _
        Public datano As Short
        <FieldOffset( 2 )> _
        Public type As Short
        <FieldOffset( 4 )> _
        Public g_data As Byte
    End Structure
    <StructLayout(LayoutKind.Explicit)> _
    Public Structure ODBMDL_2
        <FieldOffset( 0 )> _
        Public datano As Short
        <FieldOffset( 2 )> _
        Public type As Short
        <FieldOffset( 4 ), _
        MarshalAs(UnmanagedType.ByValArray,SizeConst:=4)> _
        Public g_1shot As Byte()
        <FieldOffset( 4 ), _
        MarshalAs(UnmanagedType.ByValArray,SizeConst:=35)> _
        Public g_rdata As Byte()
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBMDL_3
        Public datano As Short
        Public type As Short
        Public aux As MODAL_AUX_data
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBMDL_4
        Public datano As Short
        Public type As Short
        Public raux1 As MODAL_RAUX1_data
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBMDL_5
        Public datano As Short
        Public type As Short
        Public raux2 As MODAL_RAUX2_data
    End Structure

    ' cnc_rdgcode: read G code 
    <StructLayout(LayoutKind.Sequential,CharSet:=CharSet.Ansi,Pack:=4)> _
    Public Structure ODBGCD_data
        Public group As Short
        Public flag As Short
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=8)> _
        Public code As String
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBGCD
        Public gcd0  As ODBGCD_data
        Public gcd1  As ODBGCD_data
        Public gcd2  As ODBGCD_data
        Public gcd3  As ODBGCD_data
        Public gcd4  As ODBGCD_data
        Public gcd5  As ODBGCD_data
        Public gcd6  As ODBGCD_data
        Public gcd7  As ODBGCD_data
        Public gcd8  As ODBGCD_data
        Public gcd9  As ODBGCD_data
        Public gcd10 As ODBGCD_data
        Public gcd11 As ODBGCD_data
        Public gcd12 As ODBGCD_data
        Public gcd13 As ODBGCD_data
        Public gcd14 As ODBGCD_data
        Public gcd15 As ODBGCD_data
        Public gcd16 As ODBGCD_data
        Public gcd17 As ODBGCD_data
        Public gcd18 As ODBGCD_data
        Public gcd19 As ODBGCD_data
        Public gcd20 As ODBGCD_data
        Public gcd21 As ODBGCD_data
        Public gcd22 As ODBGCD_data
        Public gcd23 As ODBGCD_data
        Public gcd24 As ODBGCD_data
        Public gcd25 As ODBGCD_data
        Public gcd26 As ODBGCD_data
        Public gcd27 As ODBGCD_data
    End Structure

    ' cnc_rdcommand: read command value 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBCMD_data
        Public adrs As Byte
        Public num As Byte
        Public flag As Short
        Public cmd_val As Integer
        Public dec_val As Integer
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBCMD
        Public cmd0  As ODBCMD_data
        Public cmd1  As ODBCMD_data
        Public cmd2  As ODBCMD_data
        Public cmd3  As ODBCMD_data
        Public cmd4  As ODBCMD_data
        Public cmd5  As ODBCMD_data
        Public cmd6  As ODBCMD_data
        Public cmd7  As ODBCMD_data
        Public cmd8  As ODBCMD_data
        Public cmd9  As ODBCMD_data
        Public cmd10 As ODBCMD_data
        Public cmd11 As ODBCMD_data
        Public cmd12 As ODBCMD_data
        Public cmd13 As ODBCMD_data
        Public cmd14 As ODBCMD_data
        Public cmd15 As ODBCMD_data
        Public cmd16 As ODBCMD_data
        Public cmd17 As ODBCMD_data
        Public cmd18 As ODBCMD_data
        Public cmd19 As ODBCMD_data
        Public cmd20 As ODBCMD_data
        Public cmd21 As ODBCMD_data
        Public cmd22 As ODBCMD_data
        Public cmd23 As ODBCMD_data
        Public cmd24 As ODBCMD_data
        Public cmd25 As ODBCMD_data
        Public cmd26 As ODBCMD_data
        Public cmd27 As ODBCMD_data
        Public cmd28 As ODBCMD_data
        Public cmd29 As ODBCMD_data
    End Structure

    ' cnc_diagnoss:read diagnosis data 
    ' cnc_diagnosr:read diagnosis data(area specified) 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure REALDGN
        Public dgn_val As Integer    ' data of real diagnoss 
        Public dec_val As Integer    ' decimal point of real diagnoss 
    End Structure

#If M_AXIS2 Then
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure REALDGNS
        Public rdata1 As REALDGN
        Public rdata2 As REALDGN
        Public rdata3 As REALDGN
        Public rdata4 As REALDGN
        Public rdata5 As REALDGN
        Public rdata6 As REALDGN
        Public rdata7 As REALDGN
        Public rdata8 As REALDGN
        Public rdata9 As REALDGN
        Public rdata10 As REALDGN
        Public rdata11 As REALDGN
        Public rdata12 As REALDGN
        Public rdata13 As REALDGN
        Public rdata14 As REALDGN
        Public rdata15 As REALDGN
        Public rdata16 As REALDGN
        Public rdata17 As REALDGN
        Public rdata18 As REALDGN
        Public rdata19 As REALDGN
        Public rdata20 As REALDGN
        Public rdata21 As REALDGN
        Public rdata22 As REALDGN
        Public rdata23 As REALDGN
        Public rdata24 As REALDGN
    End Structure ' In case that the number of alarm is 24 
#Else
#If FS15D Then
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure REALDGNS
        Public rdata1 As REALDGN
        Public rdata2 As REALDGN
        Public rdata3 As REALDGN
        Public rdata4 As REALDGN
        Public rdata5 As REALDGN
        Public rdata6 As REALDGN
        Public rdata7 As REALDGN
        Public rdata8 As REALDGN
        Public rdata9 As REALDGN
        Public rdata10 As REALDGN
    End Structure ' In case that the number of alarm is 10 
#Else
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure REALDGNS
        Public rdata1 As REALDGN
        Public rdata2 As REALDGN
        Public rdata3 As REALDGN
        Public rdata4 As REALDGN
        Public rdata5 As REALDGN
        Public rdata6 As REALDGN
        Public rdata7 As REALDGN
        Public rdata8 As REALDGN
    End Structure ' In case that the number of alarm is 8 
#End If
#End If

    <StructLayout(LayoutKind.Explicit)> _
    Public Structure ODBDGN_1
        <FieldOffset( 0 )> _
        Public datano As Short    ' data number 
        <FieldOffset( 2 )> _
        Public type As Short      ' axis number 
        <FieldOffset( 4 )> _
        Public cdata As Byte    ' parameter / setting data 
        <FieldOffset( 4 )> _
        Public idata As Short
        <FieldOffset( 4 )> _
        Public ldata As Integer
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBDGN_2
        Public datano As Short    ' data number 
        Public type As Short      ' axis number 
        Public rdata As REALDGN
    End Structure
    <StructLayout(LayoutKind.Explicit)> _
    Public Structure ODBDGN_3
        <FieldOffset( 0 )> _
        Public datano As Short    ' data number 
        <FieldOffset( 2 )> _
        Public type As Short      ' axis number 
        <FieldOffset( 4 ), _
        MarshalAs(UnmanagedType.ByValArray,SizeConst:=MAX_AXIS)> _
        Public cdatas As Byte()
        <FieldOffset( 4 ), _
        MarshalAs(UnmanagedType.ByValArray,SizeConst:=MAX_AXIS)> _
        Public idatas As Short()
        <FieldOffset( 4 ), _
        MarshalAs(UnmanagedType.ByValArray,SizeConst:=MAX_AXIS)> _
        Public ldatas As Integer()
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBDGN_4
        Public datano As Short    ' data number 
        Public type As Short      ' axis number 
        Public rdatas As REALDGNS
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBDGN_A
        Public data1 As ODBDGN_1
        Public data2 As ODBDGN_1
        Public data3 As ODBDGN_1
        Public data4 As ODBDGN_1
        Public data5 As ODBDGN_1
        Public data6 As ODBDGN_1
        Public data7 As ODBDGN_1
    End Structure ' (sample) must be modified 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBDGN_B
        Public data1 As ODBDGN_2
        Public data2 As ODBDGN_2
        Public data3 As ODBDGN_2
        Public data4 As ODBDGN_2
        Public data5 As ODBDGN_2
        Public data6 As ODBDGN_2
        Public data7 As ODBDGN_2
    End Structure ' (sample) must be modified 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBDGN_C
        Public data1 As ODBDGN_3
        Public data2 As ODBDGN_3
        Public data3 As ODBDGN_3
        Public data4 As ODBDGN_3
        Public data5 As ODBDGN_3
        Public data6 As ODBDGN_3
        Public data7 As ODBDGN_3
    End Structure ' (sample) must be modified 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBDGN_D
        Public data1 As ODBDGN_4
        Public data2 As ODBDGN_4
        Public data3 As ODBDGN_4
        Public data4 As ODBDGN_4
        Public data5 As ODBDGN_4
        Public data6 As ODBDGN_4
        Public data7 As ODBDGN_4
    End Structure ' (sample) must be modified 

    ' cnc_adcnv:read A/D conversion data 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBAD
        Public datano As Short    ' input analog voltage type 
        Public type As Short      ' analog voltage type 
        Public data As Short      ' digital voltage data 
    End Structure

#If FS15D Then
    ' cnc_rdopmsg:read operator's message 
    <StructLayout(LayoutKind.Sequential,CharSet:=CharSet.Ansi,Pack:=4)> _
    Public Structure OPMSG_data
        Public datano As Short     ' operator's message number 
        Public type As Short       ' operator's message type   
        Public char_num As Short   ' message string length   
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=129)> _
        Public data As String      ' operator's message string 
    End Structure ' In case that the data length is 129 
#Else
    <StructLayout(LayoutKind.Sequential,CharSet:=CharSet.Ansi,Pack:=4)> _
    Public Structure OPMSG_data
        Public datano As Short     ' operator's message number 
        Public type As Short       ' operator's message type   
        Public char_num As Short   ' message string length   
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=256)> _
        Public data As String      ' operator's message string 
    End Structure ' In case that the data length is 256 
#End If
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure OPMSG
        Public msg1 As OPMSG_data
        Public msg2 As OPMSG_data
        Public msg3 As OPMSG_data
        Public msg4 As OPMSG_data
        Public msg5 As OPMSG_data
    End Structure

    ' cnc_rdopmsg2:read operator's message 
    <StructLayout(LayoutKind.Sequential,CharSet:=CharSet.Ansi,Pack:=4)> _
    Public Structure OPMSG2_data
        Public datano As Short    ' operator's message number 
        Public type As Short      ' operator's message type 
        Public char_num As Short  ' message string length 
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=64)> _
        Public data As String     ' operator's message string 
    End Structure ' In case that the data length is 64 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure OPMSG2
        Public msg1 As OPMSG2_data
        Public msg2 As OPMSG2_data
        Public msg3 As OPMSG2_data
        Public msg4 As OPMSG2_data
        Public msg5 As OPMSG2_data
    End Structure

    ' cnc_rdopmsg3:read operator's message 
    <StructLayout(LayoutKind.Sequential,CharSet:=CharSet.Ansi,Pack:=4)> _
    Public Structure OPMSG3_data
        Public datano As Short    ' operator's message number 
        Public type As Short      ' operator's message type 
        Public char_num As Short  ' message string length 
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=256)> _
        Public data As String     ' operator's message string 
    End Structure ' In case that the data length is 256 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure OPMSG3
        Public msg1 As OPMSG3_data
        Public msg2 As OPMSG3_data
        Public msg3 As OPMSG3_data
        Public msg4 As OPMSG3_data
        Public msg5 As OPMSG3_data
    End Structure

    ' cnc_sysconfig:read CNC configuration information 
    <StructLayout(LayoutKind.Sequential,CharSet:=CharSet.Ansi,Pack:=4)> _
    Public Structure ODBSYSC
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=16)> _
        Public slot_no_p As Byte()
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=16)> _
        Public slot_no_l As Byte()
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=16)> _
        Public mod_id As Short()
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=16)> _
        Public soft_id As Short()
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public s_series1 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public s_series2 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public s_series3 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public s_series4 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public s_series5 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public s_series6 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public s_series7 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public s_series8 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public s_series9 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public s_series10 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public s_series11 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public s_series12 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public s_series13 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public s_series14 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public s_series15 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public s_series16 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public s_version1 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public s_version2 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public s_version3 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public s_version4 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public s_version5 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public s_version6 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public s_version7 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public s_version8 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public s_version9 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public s_version10 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public s_version11 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public s_version12 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public s_version13 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public s_version14 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public s_version15 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public s_version16 As String
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=16)> _
        Public dummy As Byte()
        Public m_rom As Short
        Public s_rom As Short
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=8)> _
        Public svo_soft As Char()
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=6)> _
        Public pmc_soft As Char()
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=6)> _
        Public lad_soft As Char()
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=8)> _
        Public mcr_soft As Char()
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=6)> _
        Public spl1_soft As Char()
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=6)> _
        Public spl2_soft As Char()
        Public frmmin As Short
        Public drmmin As Short
        Public srmmin As Short
        Public pmcmin As Short
        Public crtmin As Short
        Public sv1min As Short
        Public sv3min As Short
        Public sicmin As Short
        Public posmin As Short
        Public drmmrc As Short
        Public drmarc As Short
        Public pmcmrc As Short
        Public dmaarc As Short
        Public iopt As Short
        Public hdiio As Short
        Public frmsub As Short
        Public drmsub As Short
        Public srmsub As Short
        Public sv5sub As Short
        Public sv7sub As Short
        Public sicsub As Short
        Public possub As Short
        Public hamsub As Short
        Public gm2gr1 As Short
        Public crtgr2 As Short
        Public gm1gr2 As Short
        Public gm2gr2 As Short
        Public cmmrb As Short
        Public sv5axs As Short
        Public sv7axs As Short
        Public sicaxs As Short
        Public posaxs As Short
        Public hanaxs As Short
        Public romr64 As Short
        Public srmr64 As Short
        Public dr1r64 As Short
        Public dr2r64 As Short
        Public iopio2 As Short
        Public hdiio2 As Short
        Public cmmrb2 As Short
        Public romfap As Short
        Public srmfap As Short
        Public drmfap As Short
    End Structure

    ' cnc_rdprstrinfo:read program restart information 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBPRS
        Public datano As Short          ' dummy 
        Public type As Short            ' dummy 
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=5)> _
        Public data_info As Short()     ' data setting information 
        Public rstr_bc As Integer       ' block counter 
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=35)> _
        Public rstr_m As Integer()      ' M code value 
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=2)> _
        Public rstr_t As Integer()      ' T code value 
        Public rstr_s As Integer        ' S code value 
        Public rstr_b As Integer        ' B code value 
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=MAX_AXIS)> _
        Public dest As Integer()        ' program re-start position 
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=MAX_AXIS)> _
        Public dist As Integer()        ' program re-start distance 
    End Structure

#If FS15D Then
    ' cnc_rdopnlsgnl:read output signal image of software operator's panel 
    ' cnc_wropnlsgnl:write output signal of software operator's panel 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBSGNL
        Public datano As Short    ' dummy 
        Public type As Short      ' data select flag 
        Public mode As Short      ' mode signal 
        Public hndl_ax As Short   ' Manual handle feed axis selection signal 
        Public hndl_mv As Short   ' Manual handle feed travel distance selection signal 
        Public rpd_ovrd As Short  ' rapid traverse override signal 
        Public jog_ovrd As Short  ' manual feedrate override signal 
        Public feed_ovrd As Short ' feedrate override signal 
        Public spdl_ovrd As Short ' spindle override signal 
        Public blck_del As Short  ' optional block skip signal 
        Public sngl_blck As Short ' single block signal 
        Public machn_lock As Short' machine lock signal 
        Public dry_run As Short   ' dry run signal 
        Public mem_prtct As Short ' memory protection signal 
        Public feed_hold As Short ' automatic operation halt signal 
        Public manual_rpd As Short' (not used) 
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=2)> _
        Public dummy As Short()     ' (not used) 
    End Structure
#Else
    ' cnc_rdopnlsgnl:read output signal image of software operator's panel 
    ' cnc_wropnlsgnl:write output signal of software operator's panel 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBSGNL
        Public datano As Short    ' dummy 
        Public type As Short      ' data select flag 
        Public mode As Short      ' mode signal 
        Public hndl_ax As Short   ' Manual handle feed axis selection signal 
        Public hndl_mv As Short   ' Manual handle feed travel distance selection signal 
        Public rpd_ovrd As Short  ' rapid traverse override signal 
        Public jog_ovrd As Short  ' manual feedrate override signal 
        Public feed_ovrd As Short ' feedrate override signal 
        Public spdl_ovrd As Short ' (not used) 
        Public blck_del As Short  ' optional block skip signal 
        Public sngl_blck As Short ' single block signal 
        Public machn_lock As Short' machine lock signal 
        Public dry_run As Short   ' dry run signal 
        Public mem_prtct As Short ' memory protection signal 
        Public feed_hold As Short ' automatic operation halt signal 
    End Structure
#End If

    ' cnc_rdopnlgnrl:read general signal image of software operator's panel 
    ' cnc_wropnlgnrl:write general signal image of software operator's panel 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBGNRL
        Public datano As Short' dummy 
        Public type   As Short' data select flag 
        Public sgnal As Byte ' general signal 
    End Structure

    ' cnc_rdopnlgsname:read general signal name of software operator's panel 
    ' cnc_wropnlgsname:write general signal name of software operator's panel
    <StructLayout(LayoutKind.Sequential,CharSet:=CharSet.Ansi,Pack:=4)> _
    Public Structure IODBRDNA
        Public datano As Short        ' dummy 
        Public type As Short          ' data select flag 
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=9)> _
        Public sgnl1_name As String   ' general signal 1 name 
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=9)> _
        Public sgnl2_name As String   ' general signal 2 name 
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=9)> _
        Public sgnl3_name As String   ' general signal 3 name 
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=9)> _
        Public sgnl4_name As String   ' general signal 4 name 
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=9)> _
        Public sgnl5_name As String   ' general signal 5 name 
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=9)> _
        Public sgnl6_name As String   ' general signal 6 name 
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=9)> _
        Public sgnl7_name As String   ' general signal 7 name 
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=9)> _
        Public sgnl8_name As String   ' general signal 8 name 
    End Structure

    ' cnc_getdtailerr:get detail error 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBERR
        Public err_no As Short
        Public err_dtno As Short
    End Structure


    ' cnc_rdparainfo:read informations of CNC parameter 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBPARAIF_info
        Public prm_no As Short
        Public prm_type As Short
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBPARAIF1
        Public info1 As ODBPARAIF_info
        Public info2 As ODBPARAIF_info
        Public info3 As ODBPARAIF_info
        Public info4 As ODBPARAIF_info
        Public info5 As ODBPARAIF_info
        Public info6 As ODBPARAIF_info
        Public info7 As ODBPARAIF_info
        Public info8 As ODBPARAIF_info
        Public info9 As ODBPARAIF_info
        Public info10 As ODBPARAIF_info
    End Structure  ' In case that the number of data is 10 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBPARAIF
        Public info_no As Short ' C# ushort
        Public prev_no As Short
        Public next_no As Short
        Public info As ODBPARAIF1
    End Structure

    ' cnc_rdsetinfo:read informations of CNC setting data 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBSETIF_info
        Public set_no As Short
        Public set_type As Short
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBSETIF1
        Public info1 As ODBSETIF_info
        Public info2 As ODBSETIF_info
        Public info3 As ODBSETIF_info
        Public info4 As ODBSETIF_info
        Public info5 As ODBSETIF_info
        Public info6 As ODBSETIF_info
        Public info7 As ODBSETIF_info
        Public info8 As ODBSETIF_info
        Public info9 As ODBSETIF_info
        Public info10 As ODBSETIF_info
    End Structure ' In case that the number of data is 10 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBSETIF
        Public info_no As Short 'C# ushort
        Public prev_no As Short
        Public next_no As Short
        Public info As ODBSETIF1
    End Structure

    ' cnc_rddiaginfo:read informations of CNC diagnose data 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBDIAGIF_info
        Public diag_no As Short
        Public diag_type As Short
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBDIAGIF1
        Public info1 As ODBDIAGIF_info
        Public info2 As ODBDIAGIF_info
        Public info3 As ODBDIAGIF_info
        Public info4 As ODBDIAGIF_info
        Public info5 As ODBDIAGIF_info
        Public info6 As ODBDIAGIF_info
        Public info7 As ODBDIAGIF_info
        Public info8 As ODBDIAGIF_info
        Public info9 As ODBDIAGIF_info
        Public info10 As ODBDIAGIF_info
    End Structure ' In case that the number of data is 10 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBDIAGIF
        Public info_no As Short ' C# ushort
        Public prev_no As Short
        Public next_no As Short
        Public info As ODBDIAGIF1
    End Structure

    ' cnc_rdparanum:read maximum, minimum and total number of CNC parameter 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBPARANUM
        Public para_min As Short ' C# ushort
        Public para_max As Short ' C# ushort
        Public total_no As Short ' C# ushort
    End Structure

    ' cnc_rdsetnum:read maximum, minimum and total number of CNC setting data 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBSETNUM
        Public set_min As Short ' C# ushort
        Public set_max As Short ' C# ushort
        Public total_no As Short ' C# ushort
    End Structure

    ' cnc_rddiagnum:read maximum, minimum and total number of CNC diagnose data 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBDIAGNUM
        Public diag_min As Short ' C# ushort
        Public diag_max As Short ' C# ushort
        Public total_no As Short ' C# ushort
    End Structure

    ' cnc_rdfrominfo:read F-ROM information on CNC  
    <StructLayout(LayoutKind.Sequential,CharSet:=CharSet.Ansi,Pack:=4)> _
    Public Structure ODBFINFO_info
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=12)> _
        Public sysname As String       ' F-ROM SYSTEM data Name 
        Public fromsize As Integer     ' F-ROM Size 
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBFINFO1
        Public info1 As ODBFINFO_info
        Public info2 As ODBFINFO_info
        Public info3 As ODBFINFO_info
        Public info4 As ODBFINFO_info
        Public info5 As ODBFINFO_info
        Public info6 As ODBFINFO_info
        Public info7 As ODBFINFO_info
        Public info8 As ODBFINFO_info
        Public info9 As ODBFINFO_info
        Public info10 As ODBFINFO_info
        Public info11 As ODBFINFO_info
        Public info12 As ODBFINFO_info
        Public info13 As ODBFINFO_info
        Public info14 As ODBFINFO_info
        Public info15 As ODBFINFO_info
        Public info16 As ODBFINFO_info
        Public info17 As ODBFINFO_info
        Public info18 As ODBFINFO_info
        Public info19 As ODBFINFO_info
        Public info20 As ODBFINFO_info
        Public info21 As ODBFINFO_info
        Public info22 As ODBFINFO_info
        Public info23 As ODBFINFO_info
        Public info24 As ODBFINFO_info
        Public info25 As ODBFINFO_info
        Public info26 As ODBFINFO_info
        Public info27 As ODBFINFO_info
        Public info28 As ODBFINFO_info
        Public info29 As ODBFINFO_info
        Public info30 As ODBFINFO_info
        Public info31 As ODBFINFO_info
        Public info32 As ODBFINFO_info
    End Structure
    <StructLayout(LayoutKind.Sequential,CharSet:=CharSet.Ansi,Pack:=4)> _
    Public Structure ODBFINFO
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=12)> _
        Public slotname As String          ' Slot Name 
        Public fromnum As Integer          ' Number of F-ROM SYSTEM data 
        Public info As ODBFINFO1
    End Structure

    ' cnc_getfrominfo:read F-ROM information on CNC  
    <StructLayout(LayoutKind.Sequential,CharSet:=CharSet.Ansi,Pack:=4)> _
    Public Structure ODBFINFORM_info
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=12)> _
        Public sysname As String       ' F-ROM SYSTEM data Name 
        Public fromsize As Integer     ' F-ROM Size 
        Public fromattrib As Integer   ' F-ROM data attribute 
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBFINFORM1
        Public info1 As ODBFINFORM_info
        Public info2 As ODBFINFORM_info
        Public info3 As ODBFINFORM_info
        Public info4 As ODBFINFORM_info
        Public info5 As ODBFINFORM_info
        Public info6 As ODBFINFORM_info
        Public info7 As ODBFINFORM_info
        Public info8 As ODBFINFORM_info
        Public info9 As ODBFINFORM_info
        Public info10 As ODBFINFORM_info
        Public info11 As ODBFINFORM_info
        Public info12 As ODBFINFORM_info
        Public info13 As ODBFINFORM_info
        Public info14 As ODBFINFORM_info
        Public info15 As ODBFINFORM_info
        Public info16 As ODBFINFORM_info
        Public info17 As ODBFINFORM_info
        Public info18 As ODBFINFORM_info
        Public info19 As ODBFINFORM_info
        Public info20 As ODBFINFORM_info
        Public info21 As ODBFINFORM_info
        Public info22 As ODBFINFORM_info
        Public info23 As ODBFINFORM_info
        Public info24 As ODBFINFORM_info
        Public info25 As ODBFINFORM_info
        Public info26 As ODBFINFORM_info
        Public info27 As ODBFINFORM_info
        Public info28 As ODBFINFORM_info
        Public info29 As ODBFINFORM_info
        Public info30 As ODBFINFORM_info
        Public info31 As ODBFINFORM_info
        Public info32 As ODBFINFORM_info
    End Structure
    <StructLayout(LayoutKind.Sequential,CharSet:=CharSet.Ansi,Pack:=4)> _
    Public Structure ODBFINFORM
        Public slotno As Integer           ' Slot Number 
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=12)> _
        Public slotname As String          ' Slot Name 
        Public fromnum As Integer          ' Number of F-ROM SYSTEM data 
        Public info As ODBFINFORM1
    End Structure

    ' cnc_rdsraminfo:read S-RAM information on CNC 
    ' cnc_getsraminfo:read S-RAM information on CNC 
    <StructLayout(LayoutKind.Sequential,CharSet:=CharSet.Ansi,Pack:=4)> _
    Public Structure ODBSINFO_info
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=12)> _
        Public sramname As String     ' S-RAM data Name 
        Public sramsize As Integer    ' S-RAM data Size 
        Public divnumber As Short     ' Division number of S-RAM file 
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=16)> _
        Public fname1 As String       ' S-RAM data Name1 
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=16)> _
        Public fname2 As String       ' S-RAM data Name2 
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=16)> _
        Public fname3 As String       ' S-RAM data Name3 
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=16)> _
        Public fname4 As String       ' S-RAM data Name4 
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=16)> _
        Public fname5 As String       ' S-RAM data Name5 
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=16)> _
        Public fname6 As String       ' S-RAM data Name6 
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBSINFO1
        Public info1 As ODBSINFO_info
        Public info2 As ODBSINFO_info
        Public info3 As ODBSINFO_info
        Public info4 As ODBSINFO_info
        Public info5 As ODBSINFO_info
        Public info6 As ODBSINFO_info
        Public info7 As ODBSINFO_info
        Public info8 As ODBSINFO_info
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBSINFO
        Public sramnum As Integer         ' Number of S-RAM data 
        Public info As ODBSINFO1
    End Structure

    ' cnc_rdsramaddr:read S-RAM address on CNC 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure SRAMADDR
        Public type As Short            ' SRAM data type 
        Public size As Integer          ' SRAM data size 
        Public offset As Integer        ' offset from top address of SRAM 
    End Structure

    ' cnc_dtsvrdpgdir:read file directory in Data Server 
    <StructLayout(LayoutKind.Sequential,CharSet:=CharSet.Ansi,Pack:=4)> _
    Public Structure ODBDSDIR_data
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=16)> _
        Public file_name As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=64)> _
        Public comment As String
        Public size As Integer
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=16)> _
        Public sDate As String
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBDSDIR1
        Public data1 As ODBDSDIR_data
        Public data2 As ODBDSDIR_data
        Public data3 As ODBDSDIR_data
        Public data4 As ODBDSDIR_data
        Public data5 As ODBDSDIR_data
        Public data6 As ODBDSDIR_data
        Public data7 As ODBDSDIR_data
        Public data8 As ODBDSDIR_data
        Public data9 As ODBDSDIR_data
        Public data10 As ODBDSDIR_data
        Public data11 As ODBDSDIR_data
        Public data12 As ODBDSDIR_data
        Public data13 As ODBDSDIR_data
        Public data14 As ODBDSDIR_data
        Public data15 As ODBDSDIR_data
        Public data16 As ODBDSDIR_data
        Public data17 As ODBDSDIR_data
        Public data18 As ODBDSDIR_data
        Public data19 As ODBDSDIR_data
        Public data20 As ODBDSDIR_data
        Public data21 As ODBDSDIR_data
        Public data22 As ODBDSDIR_data
        Public data23 As ODBDSDIR_data
        Public data24 As ODBDSDIR_data
        Public data25 As ODBDSDIR_data
        Public data26 As ODBDSDIR_data
        Public data27 As ODBDSDIR_data
        Public data28 As ODBDSDIR_data
        Public data29 As ODBDSDIR_data
        Public data30 As ODBDSDIR_data
        Public data31 As ODBDSDIR_data
        Public data32 As ODBDSDIR_data
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBDSDIR
        Public file_num As Integer
        Public remainder As Integer
        Public data_num As Short
        Public data As ODBDSDIR1
    End Structure

    ' cnc_dtsvrdset:read setting data for Data Server 
    ' cnc_dtsvwrset:write setting data for Data Server 
    <StructLayout(LayoutKind.Sequential,CharSet:=CharSet.Ansi,Pack:=4)> _
    Public Structure IODBDSSET
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=16)> _
        Public host_ip As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=32)> _
        Public host_uname As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=32)> _
        Public host_passwd As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=128)> _
        Public host_dir As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=13)> _
        Public dtsv_mac As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=16)> _
        Public dtsv_ip As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=16)> _
        Public dtsv_mask As String
    End Structure

    ' cnc_dtsvmntinfo:read maintenance information for Data Server 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBDSMNT
        Public empty_cnt As Integer
        Public total_size As Integer
        Public read_ptr As Integer
        Public write_ptr As Integer
    End Structure

    ' cnc_rdposerrs2:read the position deviation S1 and S2 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBPSER
        Public poserr1 As Integer
        Public poserr2 As Integer
    End Structure

    ' cnc_rdctrldi:read the control input signal 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBSPDI_data
        Public sgnl1 As Byte
        Public sgnl2 As Byte
        Public sgnl3 As Byte
        Public sgnl4 As Byte
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBSPDI
        Public di1 As ODBSPDI_data
        Public di2 As ODBSPDI_data
        Public di3 As ODBSPDI_data
        Public di4 As ODBSPDI_data
    End Structure

    ' cnc_rdctrldo:read the control output signal 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBSPDO_data
        Public sgnl1 As Byte
        Public sgnl2 As Byte
        Public sgnl3 As Byte
        Public sgnl4 As Byte
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBSPDO
        Public do1 As ODBSPDO_data
        Public do2 As ODBSPDO_data
        Public do3 As ODBSPDO_data
        Public do4 As ODBSPDO_data
    End Structure

    ' cnc_rdwaveprm:read the parameter of wave diagnosis 
    ' cnc_wrwaveprm:write the parameter of wave diagnosis 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBWAVE_io
        Public adr As Byte
        Public bit As Byte
        Public no As Short
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBWAVE_axis
        Public axis As Short
    End Structure
    <StructLayout(LayoutKind.Explicit)> _
    Public Structure IODBWAVE_u
        <FieldOffset( 0 )> _
        Public io As IODBWAVE_io
        <FieldOffset( 0 )> _
        Public axis As IODBWAVE_axis
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBWAVE_ch_data
        Public kind As Short
        Public u As IODBWAVE_u
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBWAVE_ch
        Public ch1 As IODBWAVE_ch_data
        Public ch2 As IODBWAVE_ch_data
        Public ch3 As IODBWAVE_ch_data
        Public ch4 As IODBWAVE_ch_data
        Public ch5 As IODBWAVE_ch_data
        Public ch6 As IODBWAVE_ch_data
        Public ch7 As IODBWAVE_ch_data
        Public ch8 As IODBWAVE_ch_data
        Public ch9 As IODBWAVE_ch_data
        Public ch10 As IODBWAVE_ch_data
        Public ch11 As IODBWAVE_ch_data
        Public ch12 As IODBWAVE_ch_data
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBWAVE
        Public condition As Short
        Public trg_adr As Char
        Public trg_bit As Byte
        Public trg_no As Short
        Public delay As Short
        Public t_range As Short
        Public ch As IODBWAVE_ch
    End Structure

    ' cnc_rdwaveprm2:read the parameter of wave diagnosis 2 
    ' cnc_wrwaveprm2:write the parameter of wave diagnosis 2 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBWVPRM_io
        Public adr As Byte
        Public bit As Byte
        Public no As Short
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBWVPRM_axis
        Public axis As Short
    End Structure
    <StructLayout(LayoutKind.Explicit)> _
    Public Structure IODBWVPRM_u
        <FieldOffset( 0 )> _
        Public io As IODBWVPRM_io
        <FieldOffset( 0 )> _
        Public axis As IODBWVPRM_axis
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBWVPRM_ch_data
        Public kind As Short
        Public u As IODBWVPRM_u
        Public reserve2 As Integer
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBWVPRM_ch
        Public ch1 As IODBWVPRM_ch_data
        Public ch2 As IODBWVPRM_ch_data
        Public ch3 As IODBWVPRM_ch_data
        Public ch4 As IODBWVPRM_ch_data
        Public ch5 As IODBWVPRM_ch_data
        Public ch6 As IODBWVPRM_ch_data
        Public ch7 As IODBWVPRM_ch_data
        Public ch8 As IODBWVPRM_ch_data
        Public ch9 As IODBWVPRM_ch_data
        Public ch10 As IODBWVPRM_ch_data
        Public ch11 As IODBWVPRM_ch_data
        Public ch12 As IODBWVPRM_ch_data
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBWVPRM
        Public condition As Short
        Public trg_adr As Byte
        Public trg_bit As Byte
        Public trg_no As Short
        Public reserve1 As Short
        Public delay As Integer
        Public t_range As Integer
        Public ch As IODBWVPRM_ch
    End Structure

    ' cnc_rdwavedata:read the data of wave diagnosis 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBWVDT_io
        Public adr As Byte
        Public bit As Byte
        Public no As Short
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBWVDT_axis
        Public axis As Short
    End Structure
    <StructLayout(LayoutKind.Explicit)> _
    Public Structure ODBWVDT_u
        <FieldOffset( 0 )> _
        Public io As ODBWVDT_io
        <FieldOffset( 0 )> _
        Public axis As ODBWVDT_axis
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBWVDT
        Public channel As Short
        Public kind As Short
        Public u As ODBWVDT_u
        Public year As Byte
        Public month As Byte
        Public day As Byte
        Public hour As Byte
        Public minute As Byte
        Public second As Byte
        Public t_cycle As Short
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=8192)> _
        Public data As Short()
    End Structure

    ' cnc_rdrmtwaveprm:read the parameter of wave diagnosis for remort diagnosis 
    ' cnc_wrrmtwaveprm:write the parameter of wave diagnosis for remort diagnosis 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBRMTPRM_alm
        Public no As Short
        Public axis As SByte
        Public type As Byte
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBRMTPRM_io
        Public adr As Char
        Public bit As Byte
        Public no As Short
    End Structure
    <StructLayout(LayoutKind.Explicit)> _
    Public Structure IODBRMTPRM_trg
        <FieldOffset( 0 )> _
        Public alm As IODBRMTPRM_alm
        <FieldOffset( 0 )> _
        Public io As IODBRMTPRM_alm
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBRMTPRM_smpl
         Public adr As Char
         Public bit As Byte
         Public no As Short
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBRMTPRM1
        Public ampl1 As IODBRMTPRM_smpl
        Public ampl2 As IODBRMTPRM_smpl
        Public ampl3 As IODBRMTPRM_smpl
        Public ampl4 As IODBRMTPRM_smpl
        Public ampl5 As IODBRMTPRM_smpl
        Public ampl6 As IODBRMTPRM_smpl
        Public ampl7 As IODBRMTPRM_smpl
        Public ampl8 As IODBRMTPRM_smpl
        Public ampl9 As IODBRMTPRM_smpl
        Public ampl10 As IODBRMTPRM_smpl
        Public ampl11 As IODBRMTPRM_smpl
        Public ampl12 As IODBRMTPRM_smpl
        Public ampl13 As IODBRMTPRM_smpl
        Public ampl14 As IODBRMTPRM_smpl
        Public ampl15 As IODBRMTPRM_smpl
        Public ampl16 As IODBRMTPRM_smpl
        Public ampl17 As IODBRMTPRM_smpl
        Public ampl18 As IODBRMTPRM_smpl
        Public ampl19 As IODBRMTPRM_smpl
        Public ampl20 As IODBRMTPRM_smpl
        Public ampl21 As IODBRMTPRM_smpl
        Public ampl22 As IODBRMTPRM_smpl
        Public ampl23 As IODBRMTPRM_smpl
        Public ampl24 As IODBRMTPRM_smpl
        Public ampl25 As IODBRMTPRM_smpl
        Public ampl26 As IODBRMTPRM_smpl
        Public ampl27 As IODBRMTPRM_smpl
        Public ampl28 As IODBRMTPRM_smpl
        Public ampl29 As IODBRMTPRM_smpl
        Public ampl30 As IODBRMTPRM_smpl
        Public ampl31 As IODBRMTPRM_smpl
        Public ampl32 As IODBRMTPRM_smpl
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBRMTPRM
        Public condition As Short
        Public reserve As Short
        Public trg As IODBRMTPRM_trg
        Public delay As Integer
        Public wv_intrvl As Short
        Public io_intrvl As Short
        Public kind1 As Short
        Public kind2 As Short
        Public ampl As IODBRMTPRM1
    End Structure

    ' cnc_rdrmtwavedt:read the data of wave diagnosis for remort diagnosis 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBRMTDT
        Public channel As Short
        Public kind As Short
        Public year As Byte
        Public month As Byte
        Public day As Byte
        Public hour As Byte
        Public minute As Byte
        Public second As Byte
        Public t_intrvl As Short
        Public trg_data As Short
        Public ins_ptr As Integer
        Public t_delta As Short
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=1917)> _
        Public data As Short()
    End Structure

    ' cnc_rdsavsigadr:read of address for PMC signal batch save 
    ' cnc_wrsavsigadr:write of address for PMC signal batch save 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBSIGAD
        Public adr As Byte
        Public reserve As Byte
        Public no As Short
        Public size As Short
    End Structure

    ' cnc_rdmgrpdata:read M-code group data 
    <StructLayout(LayoutKind.Sequential,CharSet:=CharSet.Ansi,Pack:=4)> _
    Public Structure ODBMGRP_data
        Public m_code As Integer
        Public grp_no As Short
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=21)> _
        Public m_name As String
        Public dummy As Byte
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBMGRP
        Public mgrp1 As ODBMGRP_data
        Public mgrp2 As ODBMGRP_data
        Public mgrp3 As ODBMGRP_data
        Public mgrp4 As ODBMGRP_data
        Public mgrp5 As ODBMGRP_data
        Public mgrp6 As ODBMGRP_data
        Public mgrp7 As ODBMGRP_data
        Public mgrp8 As ODBMGRP_data
        Public mgrp9 As ODBMGRP_data
        Public mgrp10 As ODBMGRP_data
    End Structure

    ' cnc_wrmgrpdata:write M-code group data 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IDBMGRP
        Public s_no As Short
        Public dummy As Short
        Public num As Short
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=500)> _
        Public group As Short()
    End Structure

    ' cnc_rdexecmcode:read executing M-code group data 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBEXEM_data
        Public no As Integer
        Public flag As Short
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBEXEM1
        Public m_code1 As ODBEXEM_data
        Public m_code2 As ODBEXEM_data
        Public m_code3 As ODBEXEM_data
        Public m_code4 As ODBEXEM_data
        Public m_code5 As ODBEXEM_data
    End Structure
    <StructLayout(LayoutKind.Sequential,CharSet:=CharSet.Ansi,Pack:=4)> _
    Public Structure ODBEXEM
        Public grp_no As Short
        Public mem_no As Short
        Public m_code As ODBEXEM1
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=21)> _
        Public m_name As String
        Public dummy As Byte
    End Structure

    ' cnc_rdrstrmcode:read program restart M-code group data 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure M_CODE_data
        Public no As Integer
        Public flag As Short
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure M_CODE1
        Public m_code1 As M_CODE_data
        Public m_code2 As M_CODE_data
        Public m_code3 As M_CODE_data
        Public m_code4 As M_CODE_data
        Public m_code5 As M_CODE_data
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBRSTRM
        Public grp_no As Short
        Public mem_no As Short
        Public m_code As M_CODE1
    End Structure

    ' cnc_rdproctime:read processing time stamp data 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBPTIME_data
        Public prg_no As Integer
        Public hour As Short
        Public minute As Byte
        Public second As Byte
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBPTIME1
        Public data1 As ODBPTIME_data
        Public data2 As ODBPTIME_data
        Public data3 As ODBPTIME_data
        Public data4 As ODBPTIME_data
        Public data5 As ODBPTIME_data
        Public data6 As ODBPTIME_data
        Public data7 As ODBPTIME_data
        Public data8 As ODBPTIME_data
        Public data9 As ODBPTIME_data
        Public data10 As ODBPTIME_data
    End Structure ' In case that the number of data is 10 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBPTIME
        Public num As Short
        Public data As ODBPTIME1
    End Structure

    ' cnc_rdprgdirtime:read program directory for processing time data 
    <StructLayout(LayoutKind.Sequential,CharSet:=CharSet.Ansi,Pack:=4)> _
    Public Structure PRGDIRTM_data
        Public prg_no As Integer
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=51)> _
        Public m_name As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=13)> _
        Public cuttime As String
    End Structure
    <StructLayout(LayoutKind.Sequential,CharSet:=CharSet.Ansi,Pack:=4)> _
    Public Structure PRGDIRTM
        Public data1 As PRGDIRTM_data
        Public data2 As PRGDIRTM_data
        Public data3 As PRGDIRTM_data
        Public data4 As PRGDIRTM_data
        Public data5 As PRGDIRTM_data
        Public data6 As PRGDIRTM_data
        Public data7 As PRGDIRTM_data
        Public data8 As PRGDIRTM_data
        Public data9 As PRGDIRTM_data
        Public data10 As PRGDIRTM_data
    End Structure ' In case that the number of data is 10 

    ' cnc_rdprogdir2:read program directory 2 
#If ONO8D = Nothing Then
    <StructLayout(LayoutKind.Sequential,CharSet:=CharSet.Ansi,Pack:=4)> _
    Public Structure PRGDIR2_data
        Public number As Short
        Public length As Integer
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=51)> _
        Public comment As String
        Public dummy As Byte
    End Structure
#Else
    <StructLayout(LayoutKind.Sequential,CharSet:=CharSet.Ansi,Pack:=4)> _
    Public Structure PRGDIR2_data
        Public number As Integer
        Public length As Integer
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=51)> _
        Public comment As String
        Public dummy As Byte
    End Structure
#End If
    <StructLayout(LayoutKind.Sequential,CharSet:=CharSet.Ansi,Pack:=4)> _
    Public Structure PRGDIR2
        Public dir1 As PRGDIR2_data
        Public dir2 As PRGDIR2_data
        Public dir3 As PRGDIR2_data
        Public dir4 As PRGDIR2_data
        Public dir5 As PRGDIR2_data
        Public dir6 As PRGDIR2_data
        Public dir7 As PRGDIR2_data
        Public dir8 As PRGDIR2_data
        Public dir9 As PRGDIR2_data
        Public dir10 As PRGDIR2_data
    End Structure ' In case that the number of data is 10 

    ' cnc_rdprogdir3:read program directory 3 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure DIR3_MDATE
      Public year As Short
      Public month As Short
      Public day As Short
      Public hour As Short
      Public minute As Short
      Public dummy As Short
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure DIR3_CDATE
      Public year As Short
      Public month As Short
      Public day As Short
      Public hour As Short
      Public minute As Short
      Public dummy As Short
    End Structure
    <StructLayout(LayoutKind.Sequential,CharSet:=CharSet.Ansi,Pack:=4)> _
    Public Structure PRGDIR3_data
        Public number As Integer
        Public length As Integer
        Public page As Integer
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=52)> _
        Public comment As String
        Public mdate As DIR3_MDATE
        Public cdate1 As DIR3_CDATE
    End Structure
    <StructLayout(LayoutKind.Sequential,CharSet:=CharSet.Ansi,Pack:=4)> _
    Public Structure PRGDIR3
        Public dir1 As PRGDIR3_data
        Public dir2 As PRGDIR3_data
        Public dir3 As PRGDIR3_data
        Public dir4 As PRGDIR3_data
        Public dir5 As PRGDIR3_data
        Public dir6 As PRGDIR3_data
        Public dir7 As PRGDIR3_data
        Public dir8 As PRGDIR3_data
        Public dir9 As PRGDIR3_data
        Public dir10 As PRGDIR3_data
    End Structure ' In case that the number of data is 10 

    ' cnc_rdprogdir4:read program directory 4 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure DIR4_MDATE
      Public year As Short
      Public month As Short
      Public day As Short
      Public hour As Short
      Public minute As Short
      Public dummy As Short
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure DIR4_CDATE
      Public year As Short
      Public month As Short
      Public day As Short
      Public hour As Short
      Public minute As Short
      Public dummy As Short
    End Structure
    <StructLayout(LayoutKind.Sequential,CharSet:=CharSet.Ansi,Pack:=4)> _
    Public Structure PRGDIR4_data
        Public number As Integer
        Public length As Integer
        Public page As Integer
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=52)> _
        Public comment As String
        Public mdate As DIR3_MDATE
        Public cdate1 As DIR3_CDATE
    End Structure
    <StructLayout(LayoutKind.Sequential,CharSet:=CharSet.Ansi,Pack:=4)> _
    Public Structure PRGDIR4
        Public dir1 As PRGDIR4_data
        Public dir2 As PRGDIR4_data
        Public dir3 As PRGDIR4_data
        Public dir4 As PRGDIR4_data
        Public dir5 As PRGDIR4_data
        Public dir6 As PRGDIR4_data
        Public dir7 As PRGDIR4_data
        Public dir8 As PRGDIR4_data
        Public dir9 As PRGDIR4_data
        Public dir10 As PRGDIR4_data
    End Structure ' In case that the number of data is 10 

    ' cnc_rdcomparam:read communication parameter for DNC1, DNC2, OSI-Ethernet 
    ' cnc_wrcomparam:write communication parameter for DNC1, DNC2, OSI-Ethernet 
    <StructLayout(LayoutKind.Sequential,CharSet:=CharSet.Ansi,Pack:=4)> _
    Public Structure IODBCPRM
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=65)> _
        Public NcApli As String
        Public Dummy1 As Byte
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=65)> _
        Public HostApli As String
        Public Dummy2 As Byte
        Public StatPstv As Integer  ' C# uint
        Public StatNgtv As Integer  ' C# uint
        Public Statmask As Integer  ' C# uint
        Public AlarmStat As Integer ' C# uint
        Public PsclHaddr As Integer ' C# uint
        Public PsclLaddr As Integer ' C# uint
        Public SvcMode1 As Short ' C# ushort
        Public SvcMode2 As Short ' C# ushort
        Public FileTout As Integer
        Public RemTout As Integer
    End Structure

    ' cnc_rdintchk:read interference check 
    ' cnc_wrintchk:write interference check 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBINT
        Public datano_s As Short  ' start offset No. 
        Public type As Short      ' kind of position 
        Public datano_e As Short  ' end offset No. 
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=8*3)> _
        Public data As Integer()  ' position value of area for not attach 
    End Structure

    ' cnc_rdwkcdshft:read work coordinate shift 
    ' cnc_wrwkcdshft:write work coordinate shift 
    ' cnc_rdwkcdsfms:read work coordinate shift measure 
    ' cnc_wrwkcdsfms:write work coordinate shift measure 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBWCSF
        Public datano As Short   ' datano           
        Public type As Short     ' axis number      
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=MAX_AXIS)> _
        Public data As Integer() ' data             
    End Structure

    ' cnc_rdomhisinfo:read operator message history information 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBOMIF
        Public om_max As Short   ' maximum operator message history  C# ushort
        Public om_sum As Short   ' actually operator message history C# ushort 
        Public om_char As Short  ' maximum character (include NULL)  C# ushort
    End Structure

    ' cnc_rdomhistry:read operator message history 
    <StructLayout(LayoutKind.Sequential,CharSet:=CharSet.Ansi,Pack:=4)> _
    Public Structure ODBOMHIS_data
        Public om_no As Short ' operator message number 
        Public year As Short  ' year 
        Public month As Short ' month 
        Public day As Short   ' day 
        Public hour As Short  ' hour 
        Public minute As Short' mimute 
        Public second As Short' second 
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=256)> _
        Public om_msg As String
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBOMHIS
        Public omhis1 As ODBOMHIS_data
        Public omhis2 As ODBOMHIS_data
        Public omhis3 As ODBOMHIS_data
        Public omhis4 As ODBOMHIS_data
        Public omhis5 As ODBOMHIS_data
        Public omhis6 As ODBOMHIS_data
        Public omhis7 As ODBOMHIS_data
        Public omhis8 As ODBOMHIS_data
        Public omhis9 As ODBOMHIS_data
        Public omhis10 As ODBOMHIS_data
    End Structure ' In case that the number of data is 10 

    ' cnc_rdbtofsr:read b-axis tool offset value(area specified) 
    ' cnc_wrbtofsr:write b-axis tool offset value(area specified) 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBBTO
        Public datano_s  As Short   ' start offset number 
        Public type  As Short       ' offset type 
        Public datano_e As Short    ' end offset number 
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=18)> _
        Public ofs As Integer()     ' offset 
    End Structure ' In case that the number of data is 9 (B type) 

    ' cnc_rdbtofsinfo:read b-axis tool offset information 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBBTLINF
        Public ofs_type As Short  ' memory type 
        Public use_no As Short    ' sum of b-axis offset 
        Public sub_no As Short    ' sub function number of offset cancel 
    End Structure

    ' cnc_rdbaxis:read b-axis command 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBBAXIS
        Public flag As Short        ' b-axis command exist or not 
        Public command As Short     ' b-axis command 
        Public speed As Short       ' b-axis speed  C# ushort
        Public sub_data As Integer    ' b-axis sub data 
    End Structure

    ' cnc_rdsyssoft:read CNC system soft series and version 
    <StructLayout(LayoutKind.Sequential,CharSet:=CharSet.Ansi,Pack:=4)> _
    Public Structure ODBSYSS
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=16)> _
        Public slot_no_p As Byte()
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=16)> _
        Public slot_no_l As Byte()
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=16)> _
        Public module_id As Short()
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=16)> _
        Public soft_id As Short()
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public soft_series1 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public soft_series2 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public soft_series3 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public soft_series4 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public soft_series5 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public soft_series6 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public soft_series7 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public soft_series8 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public soft_series9 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public soft_series10 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public soft_series11 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public soft_series12 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public soft_series13 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public soft_series14 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public soft_series15 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public soft_series16 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public soft_version1 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public soft_version2 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public soft_version3 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public soft_version4 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public soft_version5 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public soft_version6 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public soft_version7 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public soft_version8 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public soft_version9 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public soft_version10 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public soft_version11 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public soft_version12 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public soft_version13 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public soft_version14 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public soft_version15 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public soft_version16 As String
        Public soft_inst As Short
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public boot_ser As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public boot_ver As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public servo_ser As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public servo_ver As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public pmc_ser As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public pmc_ver As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public ladder_ser As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public ladder_ver As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public mcrlib_ser As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public mcrlib_ver As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public mcrapl_ser As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public mcrapl_ver As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public spl1_ser As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public spl1_ver As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public spl2_ser As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public spl2_ver As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public spl3_ser As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public spl3_ver As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public c_exelib_ser As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public c_exelib_ver As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public c_exeapl_ser As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public c_exeapl_ver As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public int_vga_ser As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public int_vga_ver As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public out_vga_ser As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public out_vga_ver As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public pmm_ser As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public pmm_ver As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public pmc_mng_ser As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public pmc_mng_ver As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public pmc_shin_ser As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public pmc_shin_ver As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public pmc_shout_ser As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public pmc_shout_ver As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public pmc_c_ser As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public pmc_c_ver As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public pmc_edit_ser As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public pmc_edit_ver As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public lddr_mng_ser As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public lddr_mng_ver As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public lddr_apl_ser As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public lddr_apl_ver As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public spl4_ser As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public spl4_ver As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public mcr2_ser As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public mcr2_ver As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public mcr3_ser As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public mcr3_ver As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public eth_boot_ser As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public eth_boot_ver As String
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=8*5)> _
        Public reserve As Byte()
    End Structure

    ' cnc_rdsyssoft2:read CNC system soft series and version (2) 
    <StructLayout(LayoutKind.Sequential,CharSet:=CharSet.Ansi,Pack:=4)> _
    Public Structure ODBSYSS2
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=16)> _
        Public slot_no_p As Byte()
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=16)> _
        Public slot_no_l As Byte()
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=16)> _
        Public module_id As Short()
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=16)> _
        Public soft_id As Short()
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public soft_series1 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public soft_series2 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public soft_series3 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public soft_series4 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public soft_series5 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public soft_series6 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public soft_series7 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public soft_series8 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public soft_series9 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public soft_series10 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public soft_series11 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public soft_series12 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public soft_series13 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public soft_series14 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public soft_series15 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public soft_series16 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public soft_version1 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public soft_version2 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public soft_version3 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public soft_version4 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public soft_version5 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public soft_version6 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public soft_version7 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public soft_version8 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public soft_version9 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public soft_version10 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public soft_version11 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public soft_version12 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public soft_version13 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public soft_version14 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public soft_version15 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public soft_version16 As String
        Public soft_inst As Short
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public boot_ser As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public boot_ver As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public servo_ser As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public servo_ver As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public pmc_ser As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public pmc_ver As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public ladder_ser As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public ladder_ver As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public mcrlib_ser As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public mcrlib_ver As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public mcrapl_ser As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public mcrapl_ver As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public spl1_ser As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public spl1_ver As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public spl2_ser As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public spl2_ver As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public spl3_ser As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public spl3_ver As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public c_exelib_ser As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public c_exelib_ver As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public c_exeapl_ser As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public c_exeapl_ver As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public int_vga_ser As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public int_vga_ver As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public out_vga_ser As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public out_vga_ver As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public pmm_ser As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public pmm_ver As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public pmc_mng_ser As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public pmc_mng_ver As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public pmc_shin_ser As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public pmc_shin_ver As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public pmc_shout_ser As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public pmc_shout_ver As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public pmc_c_ser As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public pmc_c_ver As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public pmc_edit_ser As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public pmc_edit_ver As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public lddr_mng_ser As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public lddr_mng_ver As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public lddr_apl_ser As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public lddr_apl_ver As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public spl4_ser As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public spl4_ver As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public mcr2_ser As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public mcr2_ver As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public mcr3_ser As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public mcr3_ver As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public eth_boot_ser As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public eth_boot_ver As String
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=8*5)> _
        Public reserve As Byte()
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public embEthe_ser As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public embEthe_ver As String
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=38*5)> _
        Public reserve2 As Byte()
    End Structure

    'Å@cnc_rdsyssoft3:read CNC system soft series and version (3)
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBSYSS3_data
        Public soft_id As Short
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=5)> _
        Public soft_series As Char()
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=5)> _
        Public soft_edition As Char()
    End Structure

    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBSYSS3
        Public p1  As ODBSYSS3_data
        Public p2  As ODBSYSS3_data
        Public p3  As ODBSYSS3_data
        Public p4  As ODBSYSS3_data
        Public p5  As ODBSYSS3_data
        Public p6  As ODBSYSS3_data
        Public p7  As ODBSYSS3_data
        Public p8  As ODBSYSS3_data
        Public p9  As ODBSYSS3_data
        Public p10 As ODBSYSS3_data
        Public p11 As ODBSYSS3_data
        Public p12 As ODBSYSS3_data
        Public p13 As ODBSYSS3_data
        Public p14 As ODBSYSS3_data
        Public p15 As ODBSYSS3_data
        Public p16 As ODBSYSS3_data
        Public p17 As ODBSYSS3_data
        Public p18 As ODBSYSS3_data
        Public p19 As ODBSYSS3_data
        Public p20 As ODBSYSS3_data
        Public p21 As ODBSYSS3_data
        Public p22 As ODBSYSS3_data
        Public p23 As ODBSYSS3_data
        Public p24 As ODBSYSS3_data
        Public p25 As ODBSYSS3_data
        Public p26 As ODBSYSS3_data
        Public p27 As ODBSYSS3_data
        Public p28 As ODBSYSS3_data
        Public p29 As ODBSYSS3_data
        Public p30 As ODBSYSS3_data
        Public p31 As ODBSYSS3_data
        Public p32 As ODBSYSS3_data
        Public p33 As ODBSYSS3_data
        Public p34 As ODBSYSS3_data
        Public p35 As ODBSYSS3_data
        Public p36 As ODBSYSS3_data
        Public p37 As ODBSYSS3_data
        Public p38 As ODBSYSS3_data
        Public p39 As ODBSYSS3_data
        Public p40 As ODBSYSS3_data
    End Structure

    ' cnc_rdsyshard:read CNC system hard info
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBSYSH_data
        Public id1        As Integer
        Public id2        As Integer
        Public group_id   As Short
        Public hard_id    As Short
        Public hard_num   As Short
        Public slot_no    As Short
        Public id1_format As Short
        Public id2_format As Short
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBSYSH
        Public data1  As ODBSYSH_data
        Public data2  As ODBSYSH_data
        Public data3  As ODBSYSH_data
        Public data4  As ODBSYSH_data
        Public data5  As ODBSYSH_data
        Public data6  As ODBSYSH_data
        Public data7  As ODBSYSH_data
        Public data8  As ODBSYSH_data
        Public data9  As ODBSYSH_data
        Public data10 As ODBSYSH_data
        Public data11 As ODBSYSH_data
        Public data12 As ODBSYSH_data
        Public data13 As ODBSYSH_data
        Public data14 As ODBSYSH_data
        Public data15 As ODBSYSH_data
        Public data16 As ODBSYSH_data
        Public data17 As ODBSYSH_data
        Public data18 As ODBSYSH_data
        Public data19 As ODBSYSH_data
        Public data20 As ODBSYSH_data
        Public data21 As ODBSYSH_data
        Public data22 As ODBSYSH_data
        Public data23 As ODBSYSH_data
        Public data24 As ODBSYSH_data
        Public data25 As ODBSYSH_data
    End Structure

    ' cnc_rdmdlconfig:read CNC module configuration information 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBMDLC
        Public from As Short
        Public dram As Short
        Public sram As Short
        Public pmc As Short
        Public crtc As Short
        Public servo12 As Short
        Public servo34 As Short
        Public servo56 As Short
        Public servo78 As Short
        Public sic As Short
        Public pos_lsi As Short
        Public hi_aio As Short
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=12)> _
        Public reserve As Short()
        Public drmmrc As Short
        Public drmarc As Short
        Public pmcmrc As Short
        Public dmaarc As Short
        Public iopt As Short
        Public hdiio As Short
        Public gm2gr1 As Short
        Public crtgr2 As Short
        Public gm1gr2 As Short
        Public gm2gr2 As Short
        Public cmmrb As Short
        Public sv5axs As Short
        Public sv7axs As Short
        Public sicaxs As Short
        Public posaxs As Short
        Public hamaxs As Short
        Public romr64 As Short
        Public srmr64 As Short
        Public dr1r64 As Short
        Public dr2r64 As Short
        Public iopio2 As Short
        Public hdiio2 As Short
        Public cmmrb2 As Short
        Public romfap As Short
        Public srmfap As Short
        Public drmfap As Short
        Public drmare As Short
        Public pmcmre As Short
        Public dmaare As Short
        Public frmbgg As Short
        Public drmbgg As Short
        Public asrbgg As Short
        Public edtpsc As Short
        Public slcpsc As Short
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=34)> _
        Public reserve2 As Short()
    End Structure

    ' cnc_rdpscdproc:read processing condition file (processing data) 
    ' cnc_wrpscdproc:write processing condition file (processing data) 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBPSCD_data
        Public slct As Short
        Public feed As Integer
        Public power As Short
        Public freq As Short
        Public duty As Short
        Public g_press As Short
        Public g_kind As Short
        Public g_ready_t As Short
        Public displace As Short
        Public supple As Integer
        Public edge_slt As Short
        Public appr_slt As Short
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=5)> _
        Public reserve As Short()
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBPSCD
        Public data1 As IODBPSCD_data
        Public data2 As IODBPSCD_data
        Public data3 As IODBPSCD_data
        Public data4 As IODBPSCD_data
        Public data5 As IODBPSCD_data
        Public data6 As IODBPSCD_data
        Public data7 As IODBPSCD_data
        Public data8 As IODBPSCD_data
        Public data9 As IODBPSCD_data
        Public data10 As IODBPSCD_data
    End Structure ' In case that the number of data is 10 

    ' cnc_rdpscdpirc:read processing condition file (piercing data) 
    ' cnc_wrpscdpirc:write processing condition file (piercing data) 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBPIRC_data
        Public slct As Short
        Public power As Short
        Public freq As Short
        Public duty As Short
        Public i_freq As Short
        Public i_duty As Short
        Public step_t As Short
        Public step_sum As Short
        Public pier_t As Integer
        Public g_press As Short
        Public g_kind As Short
        Public g_time As Short
        Public def_pos As Short
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=4)> _
        Public reserve As Short()
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBPIRC
        Public data1 As IODBPIRC_data
        Public data2 As IODBPIRC_data
        Public data3 As IODBPIRC_data
    End Structure

    ' cnc_rdpscdedge:read processing condition file (edging data) 
    ' cnc_wrpscdedge:write processing condition file (edging data) 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBEDGE_data
        Public slct As Short
        Public angle As Short
        Public power As Short
        Public freq As Short
        Public duty As Short
        Public pier_t As Integer
        Public g_press As Short
        Public g_kind As Short
        Public r_len As Integer
        Public r_feed As Short
        Public r_freq As Short
        Public r_duty As Short
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=5)> _
        Public reserve As Short()
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBEDGE
        Public data1 As IODBEDGE_data
        Public data2 As IODBEDGE_data
        Public data3 As IODBEDGE_data
        Public data4 As IODBEDGE_data
        Public data5 As IODBEDGE_data
    End Structure

    ' cnc_rdpscdslop:read processing condition file (slope data) 
    ' cnc_wrpscdslop:write processing condition file (slope data) 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBSLOP_data
        Public slct As Integer
        Public upleng As Integer
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=10)> _
        Public upsp As Short()
        Public dwleng As Integer
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=10)> _
        Public dwsp As Short()
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=10)> _
        Public reserve As Short()
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBSLOP
        Public data1 As IODBSLOP_data
        Public data2 As IODBSLOP_data
        Public data3 As IODBSLOP_data
        Public data4 As IODBSLOP_data
        Public data5 As IODBSLOP_data
    End Structure

    ' cnc_rdlpwrdty:read power controll duty data 
    ' cnc_wrlpwrdty:write power controll duty data 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBLPWDT
        Public slct As Short
        Public dty_const As Short
        Public dty_min As Short
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=6)> _
        Public reserve As Short()
    End Structure

    ' cnc_rdlpwrdat:read laser power data 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBLOPDT
        Public slct As Short
        Public pwr_mon As Short
        Public pwr_ofs As Short
        Public pwr_act As Short
        Public feed_act As Integer
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=4)> _
        Public reserve As Short()
    End Structure

    ' cnc_rdlagslt:read laser assist gas selection 
    ' cnc_wrlagslt:write laser assist gas selection 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBLAGSL
        Public slct As Short
        Public ag_slt As Short
        Public agflow_slt As Short
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=6)> _
        Public reserve As Short()
    End Structure

    ' cnc_rdlagst:read laser assist gas flow 
    ' cnc_wrlagst:write laser assist gas flow 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure GASFLOW
        Public slct As Short
        Public pre_time As Short
        Public pre_press As Short
        Public proc_press As Short
        Public end_time As Short
        Public end_press As Short
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=3)> _
        Public reserve As Short()
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBLAGST
        Public data1 As GASFLOW
        Public data2 As GASFLOW
        Public data3 As GASFLOW
    End Structure

    ' cnc_rdledgprc:read laser power for edge processing 
    ' cnc_wrledgprc:write laser power for edge processing 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBLEGPR
        Public slct As Short
        Public power As Short
        Public freq As Short
        Public duty As Short
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=5)> _
        Public reserve As Short()
    End Structure

    ' cnc_rdlprcprc:read laser power for piercing 
    ' cnc_wrlprcprc:write laser power for piercing 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBLPCPR
        Public slct As Short
        Public power As Short
        Public freq As Short
        Public duty As Short
        Public time As Integer
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=4)> _
        Public reserve As Short()
    End Structure

    ' cnc_rdlcmddat:read laser command data 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBLCMDT
        Public slct As Short
        Public feed As Integer
        Public power As Short
        Public freq As Short
        Public duty As Short
        Public g_kind As Short
        Public g_ready_t As Short
        Public g_press As Short
        Public error1 As Short
        Public dsplc As Integer
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=7)> _
        Public reserve As Short()
    End Structure

    ' cnc_rdlactnum:read active number 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBLACTN
        Public slct As Short
        Public act_proc As Short
        Public act_pirce As Short
        Public act_slop As Short
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=5)> _
        Public reserve As Short()
    End Structure

    ' cnc_rdlcmmt:read laser comment 
    <StructLayout(LayoutKind.Sequential,CharSet:=CharSet.Ansi,Pack:=4)> _
    Public Structure ODBLCMMT
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=25)> _
        Public comment As String
    End Structure

    ' cnc_rdpwofsthis:read power correction factor history data 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBPWOFST_data
        Public pwratio As Integer
        Public rfvolt As Integer
        Public year As Short ' C# ushort
        Public month As Short ' C# ushort
        Public day As Short ' C# ushort
        Public hour As Short ' C# ushort
        Public minute As Short ' C# ushort
        Public second As Short ' C# ushort
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBPWOFST
        Public data1 As ODBPWOFST_data
        Public data2 As ODBPWOFST_data
        Public data3 As ODBPWOFST_data
        Public data4 As ODBPWOFST_data
        Public data5 As ODBPWOFST_data
        Public data6 As ODBPWOFST_data
        Public data7 As ODBPWOFST_data
        Public data8 As ODBPWOFST_data
        Public data9 As ODBPWOFST_data
        Public data10 As ODBPWOFST_data
        Public data11 As ODBPWOFST_data
        Public data12 As ODBPWOFST_data
        Public data13 As ODBPWOFST_data
        Public data14 As ODBPWOFST_data
        Public data15 As ODBPWOFST_data
        Public data16 As ODBPWOFST_data
        Public data17 As ODBPWOFST_data
        Public data18 As ODBPWOFST_data
        Public data19 As ODBPWOFST_data
        Public data20 As ODBPWOFST_data
        Public data21 As ODBPWOFST_data
        Public data22 As ODBPWOFST_data
        Public data23 As ODBPWOFST_data
        Public data24 As ODBPWOFST_data
        Public data25 As ODBPWOFST_data
        Public data26 As ODBPWOFST_data
        Public data27 As ODBPWOFST_data
        Public data28 As ODBPWOFST_data
        Public data29 As ODBPWOFST_data
        Public data30 As ODBPWOFST_data
    End Structure

    ' cnc_rdmngtime:read management time 
    ' cnc_wrmngtime:write management time 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBMNGTIME_data
        Public life As Integer  ' C# uint
        Public tota As Integer  ' C# uint
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBMNGTIME
        Public data1 As IODBMNGTIME_data
        Public data2 As IODBMNGTIME_data
        Public data3 As IODBMNGTIME_data
        Public data4 As IODBMNGTIME_data
        Public data5 As IODBMNGTIME_data
        Public data6 As IODBMNGTIME_data
        Public data7 As IODBMNGTIME_data
        Public data8 As IODBMNGTIME_data
        Public data9 As IODBMNGTIME_data
        Public data10 As IODBMNGTIME_data
    End Structure ' In case that the number of data is 10 

    ' cnc_rddischarge:read data related to electrical discharge at power correction ends 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBDISCHRG
        Public aps As Short  ' C# ushort
        Public year As Short  ' C# ushort
        Public month As Short  ' C# ushort
        Public day As Short  ' C# ushort
        Public hour As Short  ' C# ushort
        Public minute As Short  ' C# ushort
        Public second As Short  ' C# ushort
        Public hpc As Short
        Public hfq As Short
        Public hdt As Short
        Public hpa As Short
        Public hce As Integer
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=8)> _
        Public rfi As Integer()
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=8)> _
        Public rfv As Integer()
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=8)> _
        Public dci As Integer()
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=8)> _
        Public dcv As Integer()
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=8)> _
        Public dcw As Integer()
    End Structure

    ' cnc_rddischrgalm:read alarm history data related to electrical discharg 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBDISCHRGALM_data
        Public year As Short  ' C# ushort
        Public month As Short  ' C# ushort
        Public day As Short  ' C# ushort
        Public hour As Short  ' C# ushort
        Public minute As Short  ' C# ushort
        Public second As Short  ' C# ushort
        Public almnum As Integer
        Public psec As Integer  ' C# uint
        Public hpc As Short
        Public hfq As Short
        Public hdt As Short
        Public hpa As Short
        Public hce As Integer
        Public asq As Short  ' C# ushort
        Public psu As Short  ' C# ushort
        Public aps As Short  ' C# ushort
        Public dummy  As Short
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=8)> _
        Public rfi As Integer()
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=8)> _
        Public rfv As Integer()
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=8)> _
        Public dci As Integer()
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=8)> _
        Public dcv As Integer()
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=8)> _
        Public dcw As Integer()
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=8)> _
        Public almcd As Short()
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBDISCHRGALM
        Public data1 As ODBDISCHRGALM_data
        Public data2 As ODBDISCHRGALM_data
        Public data3 As ODBDISCHRGALM_data
        Public data4 As ODBDISCHRGALM_data
        Public data5 As ODBDISCHRGALM_data
    End Structure

    ' cnc_gettimer:get date and time from cnc 
    ' cnc_settimer:set date and time for cnc 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure TIMER_DATE
        Public year As Short
        Public month As Short
        Public date1 As Short
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure TIMER_TIME
        Public hour As Short
        Public minute As Short
        Public second As Short
    End Structure
    <StructLayout(LayoutKind.Explicit)> _
    Public Structure IODBTIMER
        <FieldOffset(0)> _
        Public type As Short
        <FieldOffset(2)> _
        Public dummy As Short
        <FieldOffset(4)> _
        Public date1 As TIMER_DATE
        <FieldOffset(4)> _
        Public time As TIMER_TIME
    End Structure

    ' cnc_rdtimer:read timer data from cnc 
    ' cnc_wrtimer:write timer data for cnc 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBTIME
        Public minute As Integer
        Public msec As Integer
    End Structure

    ' cnc_rdtlctldata: read tool controll data 
    ' cnc_wrtlctldata: write tool controll data 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBTLCTL
        Public slct As Short
        Public used_tool As Short
        Public turret_indx As Short
        Public zero_tl_no As Integer
        Public t_axis_move As Integer
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=2)> _
        Public total_punch As Integer()
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=11)> _
        Public reserve As Short()
    End Structure

    ' cnc_rdtooldata: read tool data 
    ' cnc_wrtooldata: read tool data 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBTLDT_data
        Public slct As Short
        Public tool_no As Integer
        Public x_axis_ofs As Integer
        Public y_axis_ofs As Integer
        Public turret_pos As Integer
        Public chg_tl_no As Integer
        Public punch_count As Integer
        Public tool_life As Integer
        Public m_tl_radius As Integer
        Public m_tl_angle As Integer
        Public tl_shape As Byte
        Public tl_size_i As Integer
        Public tl_size_j As Integer
        Public tl_angle As Integer
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=3)> _
        Public reserve As Integer()
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBTLDT
        Public data1 As IODBTLDT_data
        Public data2 As IODBTLDT_data
        Public data3 As IODBTLDT_data
        Public data4 As IODBTLDT_data
        Public data5 As IODBTLDT_data
        Public data6 As IODBTLDT_data
        Public data7 As IODBTLDT_data
        Public data8 As IODBTLDT_data
        Public data9 As IODBTLDT_data
        Public data10 As IODBTLDT_data
    End Structure ' In case that the number of data is 10 

    ' cnc_rdmultitldt: read multi tool data 
    ' cnc_wrmultitldt: write multi tool data 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBMLTTL_data
        Public slct As Short
        Public m_tl_no As Short
        Public m_tl_radius As Integer
        Public m_tl_angle As Integer
        Public x_axis_ofs As Integer
        Public y_axis_ofs As Integer
        Public tl_shape As Byte
        Public tl_size_i As Integer
        Public tl_size_j As Integer
        Public tl_angle As Integer
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=7)> _
        Public reserve As Integer()
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBMLTTL
        Public data1 As IODBMLTTL_data
        Public data2 As IODBMLTTL_data
        Public data3 As IODBMLTTL_data
        Public data4 As IODBMLTTL_data
        Public data5 As IODBMLTTL_data
        Public data6 As IODBMLTTL_data
        Public data7 As IODBMLTTL_data
        Public data8 As IODBMLTTL_data
        Public data9 As IODBMLTTL_data
        Public data10 As IODBMLTTL_data
    End Structure ' In case that the number of data is 10 

    ' cnc_rdmtapdata: read multi tap data 
    ' cnc_wrmtapdata: write multi tap data 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBMTAP_data
        Public slct As Short
        Public tool_no As Integer
        Public x_axis_ofs As Integer
        Public y_axis_ofs As Integer
        Public punch_count As Integer
        Public tool_life As Integer
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=11)> _
        Public reserve As Integer()
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBMTAP
        Public data1 As IODBMTAP_data
        Public data2 As IODBMTAP_data
        Public data3 As IODBMTAP_data
        Public data4 As IODBMTAP_data
        Public data5 As IODBMTAP_data
        Public data6 As IODBMTAP_data
        Public data7 As IODBMTAP_data
        Public data8 As IODBMTAP_data
        Public data9 As IODBMTAP_data
        Public data10 As IODBMTAP_data
    End Structure

    ' cnc_rdtoolinfo: read tool information 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBPTLINF
        Public tld_max As Short
        Public mlt_max As Short
        Public reserve As Short
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=16)> _
        Public tld_size As Short()
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=16)> _
        Public mlt_size As Short()
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=16)> _
        Public reserves As Short()
    End Structure

    ' cnc_rdsafetyzone: read safetyzone data 
    ' cnc_wrsafetyzone: write safetyzone data 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBSAFE_data
        Public slct As Short
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=3)> _
        Public data As Integer()
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBSAFE
        Public data1 As IODBSAFE_data
        Public data2 As IODBSAFE_data
        Public data3 As IODBSAFE_data
        Public data4 As IODBSAFE_data
    End Structure ' In case that the number of data is 4 

    ' cnc_rdtoolzone: read toolzone data 
    ' cnc_wrtoolzone: write toolzone data 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBTLZN_data
        Public slct As Short
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=2)> _
        Public data As Integer()
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBTLZN
        Public data1 As IODBTLZN_data
        Public data2 As IODBTLZN_data
        Public data3 As IODBTLZN_data
        Public data4 As IODBTLZN_data
        Public data5 As IODBTLZN_data
        Public data6 As IODBTLZN_data
        Public data7 As IODBTLZN_data
        Public data8 As IODBTLZN_data
        Public data9 As IODBTLZN_data
        Public data10 As IODBTLZN_data
        Public data11 As IODBTLZN_data
        Public data12 As IODBTLZN_data
    End Structure ' In case that the number of data is 12 

    ' cnc_rdacttlzone: read active toolzone data 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBACTTLZN
        Public act_no As Short
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=2)> _
        Public data As Integer()
    End Structure

    ' cnc_rdbrstrinfo:read block restart information 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBBRS
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=MAX_AXIS)> _
        Public dest As Integer()
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=MAX_AXIS)> _
        Public dist As Integer()
    End Structure '  In case that the number of axes is MAX_AXIS 

    ' cnc_rdradofs:read tool radius offset for position data 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBROFS
        Public mode As Short
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=2)> _
        Public pln_axes As Short()
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=2)> _
        Public ofsvct As Integer()
    End Structure

    ' cnc_rdlenofs:read tool length offset for position data 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBLOFS
        Public mode As Short
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=MAX_AXIS)> _
        Public ofsvct As Integer()
    End Structure '  In case that the number of axes is MAX_AXIS 

    ' cnc_rdfixcycle:read fixed cycle for position data 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBFIX
        Public mode As Short
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=2)> _
        Public pln_axes As Short()
        Public drl_axes As Short
        Public i_pos As Integer
        Public r_pos As Integer
        Public z_pos As Integer
        Public cmd_cnt As Integer
        Public act_cnt As Integer
        Public cut As Integer
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=2)> _
        Public shift As Integer()
    End Structure

    ' cnc_rdcdrotate:read coordinate rotate for position data 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBROT
        Public mode As Short
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=2)> _
        Public pln_axes As Short()
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=2)> _
        Public center As Integer()
        Public angle As Integer
    End Structure

    ' cnc_rd3dcdcnv:read 3D coordinate convert for position data 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODB3DCD
        Public mode As Short
        Public dno As Short
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=3)> _
        Public cd_axes As Short()
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=2*3)> _
        Public center As Integer()
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=2*3)> _
        Public direct As Integer()
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=2)> _
        Public angle As Integer()
    End Structure

    ' cnc_rdmirimage:read programable mirror image for position data 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBMIR
        Public mode As Short
        Public mir_flag As Integer
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=MAX_AXIS)> _
        Public mir_pos As Integer()
    End Structure '  In case that the number of axes is MAX_AXIS 

    ' cnc_rdscaling:read scaling data for position data 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBSCL
        Public mode As Short
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=MAX_AXIS)> _
        Public center As Integer()
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=MAX_AXIS)> _
        Public magnif As Integer()
    End Structure '  In case that the number of axes is MAX_AXIS 

    ' cnc_rd3dtofs:read 3D tool offset for position data 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODB3DTO
        Public mode As Short
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=3)> _
        Public ofs_axes As Short()
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=3)> _
        Public ofsvct As Integer()
    End Structure

    ' cnc_rdposofs:read tool position offset for position data 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBPOFS
        Public mode As Short
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=MAX_AXIS)> _
        Public ofsvct As Integer()
    End Structure '  In case that the number of axes is MAX_AXIS 

    ' cnc_rdhpccset:read hpcc setting data 
    ' cnc_wrhpccset:write hpcc setting data 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBHPST
        Public slct As Short
        Public hpcc As Short
        Public multi As Short
        Public ovr1 As Short
        Public ign_f As Short
        Public foward As Short
        Public max_f As Integer
        Public ovr2 As Short
        Public ovr3 As Short
        Public ovr4 As Short
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=7)> _
        Public reserve As Integer()
    End Structure

    ' cnc_rdhpcctupr:read hpcc tuning data ( parameter input ) 
    ' cnc_wrhpcctupr:write hpcc tuning data ( parameter input ) 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBHPPR_tune
        Public slct As Short
        Public diff As Short
        Public fine As Short
        Public acc_lv As Short
        Public max_f As Integer
        Public bipl As Short
        Public aipl As Short
        Public corner As Integer
        Public clamp As Short
        Public radius As Integer
        Public max_cf As Integer
        Public min_cf As Integer
        Public foward As Integer
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=5)> _
        Public reserve As Integer()
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBHPPR
        Public tune1 As IODBHPPR_tune
        Public tune2 As IODBHPPR_tune
        Public tune3 As IODBHPPR_tune
    End Structure

    ' cnc_rdhpcctuac:read hpcc tuning data ( acc input ) 
    ' cnc_wrhpcctuac:write hpcc tuning data ( acc input ) 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBHPAC_tune
        Public slct As Short
        Public diff As Short
        Public fine As Short
        Public acc_lv As Short
        Public bipl As Integer
        Public aipl As Short
        Public corner As Integer
        Public clamp As Integer
        Public c_acc As Integer
        Public foward As Integer
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=8)> _
        Public reserve As Integer()
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBHPAC
        Public tune1 As IODBHPAC_tune
        Public tune2 As IODBHPAC_tune
        Public tune3 As IODBHPAC_tune
    End Structure

    ' cnc_rd3dtooltip:read tip of tool for 3D handle 
    ' cnc_rd3dmovrlap:read move overrlap of tool for 3D handle 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODB3DHDL_data
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=5)> _
        Public axes As Short()
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=5)> _
        Public data As Integer()
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODB3DHDL
        Public data1 As ODB3DHDL_data
        Public data2 As ODB3DHDL_data
    End Structure

    ' cnc_rd3dpulse:read pulse for 3D handle 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODB3DPLS_data
        Public right_angle_x As Integer
        Public right_angle_y As Integer
        Public tool_axis As Integer    
        Public tool_tip_a_b As Integer 
        Public tool_tip_c As Integer   
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODB3DPLS
        Public pls1 As ODB3DPLS_data
        Public pls2 As ODB3DPLS_data
    End Structure

    ' cnc_rdaxisname: read axis name 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBAXISNAME_data
        Public name As Byte         ' axis name 
        Public suff As Byte         ' suffix 
    End Structure
#If M_AXIS2 Then
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBAXISNAME
        Public data1 As ODBAXISNAME_data
        Public data2 As ODBAXISNAME_data
        Public data3 As ODBAXISNAME_data
        Public data4 As ODBAXISNAME_data
        Public data5 As ODBAXISNAME_data
        Public data6 As ODBAXISNAME_data
        Public data7 As ODBAXISNAME_data
        Public data8 As ODBAXISNAME_data
        Public data9 As ODBAXISNAME_data
        Public data10 As ODBAXISNAME_data
        Public data11 As ODBAXISNAME_data
        Public data12 As ODBAXISNAME_data
        Public data13 As ODBAXISNAME_data
        Public data14 As ODBAXISNAME_data
        Public data15 As ODBAXISNAME_data
        Public data16 As ODBAXISNAME_data
        Public data17 As ODBAXISNAME_data
        Public data18 As ODBAXISNAME_data
        Public data19 As ODBAXISNAME_data
        Public data20 As ODBAXISNAME_data
        Public data21 As ODBAXISNAME_data
        Public data22 As ODBAXISNAME_data
        Public data23 As ODBAXISNAME_data
        Public data24 As ODBAXISNAME_data
    End Structure
#Else
#If FS15D Then
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBAXISNAME
        Public data1 As ODBAXISNAME_data
        Public data2 As ODBAXISNAME_data
        Public data3 As ODBAXISNAME_data
        Public data4 As ODBAXISNAME_data
        Public data5 As ODBAXISNAME_data
        Public data6 As ODBAXISNAME_data
        Public data7 As ODBAXISNAME_data
        Public data8 As ODBAXISNAME_data
        Public data9 As ODBAXISNAME_data
        Public data10 As ODBAXISNAME_data
    End Structure
#Else
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBAXISNAME
        Public data1 As ODBAXISNAME_data
        Public data2 As ODBAXISNAME_data
        Public data3 As ODBAXISNAME_data
        Public data4 As ODBAXISNAME_data
        Public data5 As ODBAXISNAME_data
        Public data6 As ODBAXISNAME_data
        Public data7 As ODBAXISNAME_data
        Public data8 As ODBAXISNAME_data
    End Structure
#End If
#End If

    ' cnc_rdspdlname: read spindle name 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBSPDLNAME_data
        Public name As Byte         ' spindle name 
        Public suff1 As Byte        ' suffix 
        Public suff2 As Byte        ' suffix 
        Public suff3 As Byte        ' suffix 
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBSPDLNAME
        Public data1 As ODBSPDLNAME_data
        Public data2 As ODBSPDLNAME_data
        Public data3 As ODBSPDLNAME_data
        Public data4 As ODBSPDLNAME_data
    End Structure

    ' cnc_exaxisname: read spindle name
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBEXAXISNAME
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=4)> _
        Public axname1   As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=4)> _
        Public axname2   As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=4)> _
        Public axname3   As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=4)> _
        Public axname4   As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=4)> _
        Public axname5   As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=4)> _
        Public axname6   As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=4)> _
        Public axname7   As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=4)> _
        Public axname8   As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=4)> _
        Public axname9   As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=4)> _
        Public axname10  As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=4)> _
        Public axname11  As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=4)> _
        Public axname12  As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=4)> _
        Public axname13  As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=4)> _
        Public axname14  As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=4)> _
        Public axname15  As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=4)> _
        Public axname16  As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=4)> _
        Public axname17  As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=4)> _
        Public axname18  As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=4)> _
        Public axname19  As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=4)> _
        Public axname20 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=4)> _
        Public axname21  As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=4)> _
        Public axname22  As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=4)> _
        Public axname23  As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=4)> _
        Public axname24  As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=4)> _
        Public axname25  As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=4)> _
        Public axname26  As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=4)> _
        Public axname27  As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=4)> _
        Public axname28  As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=4)> _
        Public axname29  As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=4)> _
        Public axname30 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=4)> _
        Public axname31  As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=4)> _
        Public axname32 As String
    End Structure

    ' cnc_wrunsolicprm: Set the unsolicited message parameters 
    ' cnc_rdunsolicprm: Get the unsolicited message parameters 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBUNSOLIC_pmc
        Public type As Short
        Public rdaddr As Short
        Public rdno As Short
        Public rdsize As Short
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBUNSOLIC_dmy
        Public type As Short
        Public dummy1 As Integer
        Public dummy2 As Short
    End Structure
    <StructLayout(LayoutKind.Explicit)> _
    Public Structure IODBUNSOLIC_rddata
        <FieldOffset( 0 )> _
        Public pmc As IODBUNSOLIC_pmc
        <FieldOffset( 0 )> _
        Public dmy As IODBUNSOLIC_dmy
    End Structure

    <StructLayout(LayoutKind.Sequential,CharSet:=CharSet.Ansi,Pack:=4)> _
    Public Structure IODBUNSOLIC
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=16)> _
        Public ipaddr As String
        Public port As Short  ' C# ushort
        Public reqaddr As Short
        Public pmcno As Short
        Public retry As Short
        Public timeout As Short
        Public alivetime As Short
        Public setno As Short
        Public rddata1 As IODBUNSOLIC_rddata
        Public rddata2 As IODBUNSOLIC_rddata
        Public rddata3 As IODBUNSOLIC_rddata
    End Structure

    ' cnc_rdunsolicmsg: Reads the unsolicited message data 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IDBUNSOLICMSG_msg
        Public rdsize As Short
        <MarshalAs(UnmanagedType.AsAny)> _
        Public data As Object
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IDBUNSOLICMSG1
        Public msg1 As IDBUNSOLICMSG_msg
        Public msg2 As IDBUNSOLICMSG_msg
        Public msg3 As IDBUNSOLICMSG_msg
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IDBUNSOLICMSG
        Public getno As Short
        Public msg As IDBUNSOLICMSG1
    End Structure

    ' cnc_rdpm_cncitem: read cnc maintenance item
    ' cnc_rdpm_mcnitem: read machine specific maintenance item
    ' cnc_wrpm_mcnitem: write machine specific maintenance item
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBITEM
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=62)> _
        Public name1  As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=62)> _
        Public name2  As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=62)> _
        Public name3  As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=62)> _
        Public name4  As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=62)> _
        Public name5  As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=62)> _
        Public name6  As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=62)> _
        Public name7  As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=62)> _
        Public name8  As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=62)> _
        Public name9  As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=62)> _
        Public name10 As String
    End Structure

    ' cnc_rdpm_item:read maintenance item status
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBPMAINTE_data
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=62)> _
        Public name     As String   ' name
        Public type     As Integer  ' life count type
        Public total    As Integer  ' total life time (minite basis)
        Public remain   As Integer  ' life rest time
        Public stat     As Integer  ' life state
    End Structure

    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBPMAINTE
        Public data1  As IODBPMAINTE_data
        Public data2  As IODBPMAINTE_data
        Public data3  As IODBPMAINTE_data
        Public data4  As IODBPMAINTE_data
        Public data5  As IODBPMAINTE_data
        Public data6  As IODBPMAINTE_data
        Public data7  As IODBPMAINTE_data
        Public data8  As IODBPMAINTE_data
        Public data9  As IODBPMAINTE_data
        Public data10 As IODBPMAINTE_data
    End Structure

    ' cnc_sysinfo_ex:read CNC system path information
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBSYSEX_path
        Public system    As Short
        Public group     As Short
        Public attrib    As Short
        Public ctrl_axis As Short
        Public ctrl_srvo As Short
        Public ctrl_spdl As Short
        Public mchn_no   As Short
        Public reserved  As Short
    End Structure

    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBSYSEX_data
        Public data1  As ODBSYSEX_path
        Public data2  As ODBSYSEX_path
        Public data3  As ODBSYSEX_path
        Public data4  As ODBSYSEX_path
        Public data5  As ODBSYSEX_path
        Public data6  As ODBSYSEX_path
        Public data7  As ODBSYSEX_path
        Public data8  As ODBSYSEX_path
        Public data9  As ODBSYSEX_path
        Public data10 As ODBSYSEX_path
    End Structure

    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBSYSEX
        Public max_axis  As Short
        Public max_spdl  As Short
        Public max_path  As Short
        Public max_mchn  As Short
        Public ctrl_axis As Short
        Public ctrl_srvo As Short
        Public ctrl_spdl As Short
        Public ctrl_path As Short
        Public ctrl_mchn As Short
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=3)> _
        Public reserved As Short()
        Public path As ODBSYSEX_data
    End Structure

'------------------
' CNC : SERCOS I/F 
'------------------

    ' cnc_srcsrdidinfo:Read ID information of SERCOS I/F 
    ' cnc_srcswridinfo:Write ID information of SERCOS I/F 
    <StructLayout(LayoutKind.Sequential,CharSet:=CharSet.Ansi,Pack:=4)> _
    Public Structure IODBIDINF
        Public id_no As Integer
        Public drv_no As Short
        Public acc_element As Short
        Public err_general As Short
        Public err_id_no As Short
        Public err_id_name As Short
        Public err_attr As Short
        Public err_unit As Short
        Public err_min_val As Short
        Public err_max_val As Short
        Public id_name_len As Short
        Public id_name_max As Short
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=60)> _
        Public id_name As String
        Public attr As Integer
        Public unit_len As Short
        Public unit_max As Short
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=12)> _
        Public unit As Byte()
        Public min_val As Integer
        Public max_val As Integer
    End Structure

    ' cnc_srcsrdexstat:Get execution status of reading/writing operation data of SERCOS I/F 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBSRCSST
        Public acc_element As Short
        Public err_general As Short
        Public err_id_no As Short
        Public err_attr As Short
        Public err_op_data As Short
    End Structure

    ' cnc_srcsrdlayout:Read drive assign of SERCOS I/F 
    <StructLayout(LayoutKind.Sequential,CharSet:=CharSet.Ansi,Pack:=4)> _
    Public Structure ODBSRCSLYT
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=4)> _
        Public spndl As Short()
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=8)> _
        Public servo As Short()
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=8)> _
        Public axis_name As String
    End Structure

'----------------------------
' CNC : Servo Guide          
'----------------------------
    ' cnc_sdsetchnl:Servo Guide (Channel data set) 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IDBCHAN_data
        Public chno As Byte
        Public axis As SByte
        Public datanum As Integer
        Public datainf As Short  ' C# ushort
        Public dataadr As Short
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IDBCHAN
        Public data1 As IDBCHAN_data
        Public data2 As IDBCHAN_data
        Public data3 As IDBCHAN_data
        Public data4 As IDBCHAN_data
        Public data5 As IDBCHAN_data
        Public data6 As IDBCHAN_data
        Public data7 As IDBCHAN_data
        Public data8 As IDBCHAN_data
    End Structure

    ' cnc_sdsetchnl:Servo Guide (read Sampling data) 
    ' cnc_sfbreadsmpl:Servo feedback data (read Sampling data) 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBSD
        Public chadata As IntPtr
        Public count As IntPtr
    End Structure

    ' cnc_sfbsetchnl:Servo feedback data (Channel data set) 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IDBSFBCHAN
        Public chno As Byte
        Public axis As SByte
        Public shift As Short  ' C# ushort
    End Structure

    ' cnc_sdtsetchnl:Servo Guide (Channel data set) 
    <StructLayout(LayoutKind.Sequential, Pack:=4)> _ 
    Public Structure IDBSDTCHAN 
        Public type As Short 
        Public chno As Byte 
        Public axis As SByte 
        Public shift As UShort 
    End Structure 

'-------------------------
' CNC : FS18-LN function  
'-------------------------

    ' cnc_allowcnd:read allowanced state 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBCAXIS
        Public dummy As Short             ' dummy 
        Public type As Short              ' axis number 
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=MAX_AXIS)> _
        Public data As SByte()              ' data value 
    End Structure


'---------------------------------
' CNC : C-EXE SRAM file function  
'---------------------------------

    ' read C-EXE SRAM disk directory 
    <StructLayout(LayoutKind.Sequential,CharSet:=CharSet.Ansi,Pack:=4)> _
    Public Structure CFILEINFO_data
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=12)> _
        Public fname As String      ' file name 
        Public file_size As Integer ' file size (bytes) 
        Public file_attr As Integer ' attribute 
        Public year As Short        ' year 
        Public month As Short       ' month 
        Public day As Short         ' day 
        Public hour As Short        ' hour 
        Public minute As Short      ' mimute 
        Public second As Short      ' second 
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure CFILEINFO
        Public data1 As CFILEINFO_data
        Public data2 As CFILEINFO_data
        Public data3 As CFILEINFO_data
        Public data4 As CFILEINFO_data
        Public data5 As CFILEINFO_data
        Public data6 As CFILEINFO_data
        Public data7 As CFILEINFO_data
        Public data8 As CFILEINFO_data
        Public data9 As CFILEINFO_data
        Public data10 As CFILEINFO_data
    End Structure

'-----
' PMC 
'-----

    ' pmc_rdpmcrng:read PMC data(area specified) 
    ' pmc_wrpmcrng:write PMC data(area specified) 
    <StructLayout(LayoutKind.Explicit)> _
    Public Structure IODBPMC0
        <FieldOffset(0)> _
        Public type_a As Short    ' PMC address type 
        <FieldOffset(2)> _
        Public type_d As Short    ' PMC data type 
        <FieldOffset(4)> _
        Public datano_s As Short  ' start PMC address 
        <FieldOffset(6)> _
        Public datano_e As Short  ' end PMC address 
        <FieldOffset(8), _
        MarshalAs(UnmanagedType.ByValArray, SizeConst:=8)> _
        Public cdata As Byte()      ' PMC data 
    End Structure ' In case that the number of data is 8 
    <StructLayout(LayoutKind.Explicit)> _
    Public Structure IODBPMC1
        <FieldOffset(0)> _
        Public type_a As Short    ' PMC address type 
        <FieldOffset(2)> _
        Public type_d As Short    ' PMC data type 
        <FieldOffset(4)> _
        Public datano_s As Short  ' start PMC address 
        <FieldOffset(6)> _
        Public datano_e As Short  ' end PMC address 
        <FieldOffset(8), _
        MarshalAs(UnmanagedType.ByValArray, SizeConst:=8)> _
        Public idata As Short()
    End Structure ' In case that the number of data is 8 
    <StructLayout(LayoutKind.Explicit)> _
    Public Structure IODBPMC2
        <FieldOffset(0)> _
        Public type_a As Short    ' PMC address type 
        <FieldOffset(2)> _
        Public type_d As Short    ' PMC data type 
        <FieldOffset(4)> _
        Public datano_s As Short  ' start PMC address 
        <FieldOffset(6)> _
        Public datano_e As Short  ' end PMC address 
        <FieldOffset(8), _
        MarshalAs(UnmanagedType.ByValArray, SizeConst:=8)> _
        Public ldata As Integer()
    End Structure ' In case that the number of data is 8 

    ' pmc_rdpmcinfo:read informations of PMC data 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBPMCINF_info
        Public pmc_adr As Char
        Public adr_attr As Byte
        Public top_num As Short  ' C# ushort
        Public last_num As Short  ' C# ushort
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBPMCINF1
        Public info1 As ODBPMCINF_info
        Public info2 As ODBPMCINF_info
        Public info3 As ODBPMCINF_info
        Public info4 As ODBPMCINF_info
        Public info5 As ODBPMCINF_info
        Public info6 As ODBPMCINF_info
        Public info7 As ODBPMCINF_info
        Public info8 As ODBPMCINF_info
        Public info9 As ODBPMCINF_info
        Public info10 As ODBPMCINF_info
        Public info11 As ODBPMCINF_info
        Public info12 As ODBPMCINF_info
        Public info13 As ODBPMCINF_info
        Public info14 As ODBPMCINF_info
        Public info15 As ODBPMCINF_info
        Public info16 As ODBPMCINF_info
        Public info17 As ODBPMCINF_info
        Public info18 As ODBPMCINF_info
        Public info19 As ODBPMCINF_info
        Public info20 As ODBPMCINF_info
        Public info21 As ODBPMCINF_info
        Public info22 As ODBPMCINF_info
        Public info23 As ODBPMCINF_info
        Public info24 As ODBPMCINF_info
        Public info25 As ODBPMCINF_info
        Public info26 As ODBPMCINF_info
        Public info27 As ODBPMCINF_info
        Public info28 As ODBPMCINF_info
        Public info29 As ODBPMCINF_info
        Public info30 As ODBPMCINF_info
        Public info31 As ODBPMCINF_info
        Public info32 As ODBPMCINF_info
        Public info33 As ODBPMCINF_info
        Public info34 As ODBPMCINF_info
        Public info35 As ODBPMCINF_info
        Public info36 As ODBPMCINF_info
        Public info37 As ODBPMCINF_info
        Public info38 As ODBPMCINF_info
        Public info39 As ODBPMCINF_info
        Public info40 As ODBPMCINF_info
        Public info41 As ODBPMCINF_info
        Public info42 As ODBPMCINF_info
        Public info43 As ODBPMCINF_info
        Public info44 As ODBPMCINF_info
        Public info45 As ODBPMCINF_info
        Public info46 As ODBPMCINF_info
        Public info47 As ODBPMCINF_info
        Public info48 As ODBPMCINF_info
        Public info49 As ODBPMCINF_info
        Public info50 As ODBPMCINF_info
        Public info51 As ODBPMCINF_info
        Public info52 As ODBPMCINF_info
        Public info53 As ODBPMCINF_info
        Public info54 As ODBPMCINF_info
        Public info55 As ODBPMCINF_info
        Public info56 As ODBPMCINF_info
        Public info57 As ODBPMCINF_info
        Public info58 As ODBPMCINF_info
        Public info59 As ODBPMCINF_info
        Public info60 As ODBPMCINF_info
        Public info61 As ODBPMCINF_info
        Public info62 As ODBPMCINF_info
        Public info63 As ODBPMCINF_info
        Public info64 As ODBPMCINF_info
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBPMCINF
        Public datano As Short
        Public info As ODBPMCINF1
    End Structure

    ' pmc_rdcntldata:read PMC parameter data table control data 
    ' pmc_wrcntldata:write PMC parameter data table control data 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBPMCCNTL_info
        Public tbl_prm As Byte
        Public data_type As Byte
        Public data_size As Short  ' C# ushort
        Public data_dsp As Short  ' C# ushort
        Public dummy As Short
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBPMCCNTL1
        Public info1 As IODBPMCCNTL_info
        Public info2 As IODBPMCCNTL_info
        Public info3 As IODBPMCCNTL_info
        Public info4 As IODBPMCCNTL_info
        Public info5 As IODBPMCCNTL_info
        Public info6 As IODBPMCCNTL_info
        Public info7 As IODBPMCCNTL_info
        Public info8 As IODBPMCCNTL_info
        Public info9 As IODBPMCCNTL_info
        Public info10 As IODBPMCCNTL_info
        Public info11 As IODBPMCCNTL_info
        Public info12 As IODBPMCCNTL_info
        Public info13 As IODBPMCCNTL_info
        Public info14 As IODBPMCCNTL_info
        Public info15 As IODBPMCCNTL_info
        Public info16 As IODBPMCCNTL_info
        Public info17 As IODBPMCCNTL_info
        Public info18 As IODBPMCCNTL_info
        Public info19 As IODBPMCCNTL_info
        Public info20 As IODBPMCCNTL_info
        Public info21 As IODBPMCCNTL_info
        Public info22 As IODBPMCCNTL_info
        Public info23 As IODBPMCCNTL_info
        Public info24 As IODBPMCCNTL_info
        Public info25 As IODBPMCCNTL_info
        Public info26 As IODBPMCCNTL_info
        Public info27 As IODBPMCCNTL_info
        Public info28 As IODBPMCCNTL_info
        Public info29 As IODBPMCCNTL_info
        Public info30 As IODBPMCCNTL_info
        Public info31 As IODBPMCCNTL_info
        Public info32 As IODBPMCCNTL_info
        Public info33 As IODBPMCCNTL_info
        Public info34 As IODBPMCCNTL_info
        Public info35 As IODBPMCCNTL_info
        Public info36 As IODBPMCCNTL_info
        Public info37 As IODBPMCCNTL_info
        Public info38 As IODBPMCCNTL_info
        Public info39 As IODBPMCCNTL_info
        Public info40 As IODBPMCCNTL_info
        Public info41 As IODBPMCCNTL_info
        Public info42 As IODBPMCCNTL_info
        Public info43 As IODBPMCCNTL_info
        Public info44 As IODBPMCCNTL_info
        Public info45 As IODBPMCCNTL_info
        Public info46 As IODBPMCCNTL_info
        Public info47 As IODBPMCCNTL_info
        Public info48 As IODBPMCCNTL_info
        Public info49 As IODBPMCCNTL_info
        Public info50 As IODBPMCCNTL_info
        Public info51 As IODBPMCCNTL_info
        Public info52 As IODBPMCCNTL_info
        Public info53 As IODBPMCCNTL_info
        Public info54 As IODBPMCCNTL_info
        Public info55 As IODBPMCCNTL_info
        Public info56 As IODBPMCCNTL_info
        Public info57 As IODBPMCCNTL_info
        Public info58 As IODBPMCCNTL_info
        Public info59 As IODBPMCCNTL_info
        Public info60 As IODBPMCCNTL_info
        Public info61 As IODBPMCCNTL_info
        Public info62 As IODBPMCCNTL_info
        Public info63 As IODBPMCCNTL_info
        Public info64 As IODBPMCCNTL_info
        Public info65 As IODBPMCCNTL_info
        Public info66 As IODBPMCCNTL_info
        Public info67 As IODBPMCCNTL_info
        Public info68 As IODBPMCCNTL_info
        Public info69 As IODBPMCCNTL_info
        Public info70 As IODBPMCCNTL_info
        Public info71 As IODBPMCCNTL_info
        Public info72 As IODBPMCCNTL_info
        Public info73 As IODBPMCCNTL_info
        Public info74 As IODBPMCCNTL_info
        Public info75 As IODBPMCCNTL_info
        Public info76 As IODBPMCCNTL_info
        Public info77 As IODBPMCCNTL_info
        Public info78 As IODBPMCCNTL_info
        Public info79 As IODBPMCCNTL_info
        Public info80 As IODBPMCCNTL_info
        Public info81 As IODBPMCCNTL_info
        Public info82 As IODBPMCCNTL_info
        Public info83 As IODBPMCCNTL_info
        Public info84 As IODBPMCCNTL_info
        Public info85 As IODBPMCCNTL_info
        Public info86 As IODBPMCCNTL_info
        Public info87 As IODBPMCCNTL_info
        Public info88 As IODBPMCCNTL_info
        Public info89 As IODBPMCCNTL_info
        Public info90 As IODBPMCCNTL_info
        Public info91 As IODBPMCCNTL_info
        Public info92 As IODBPMCCNTL_info
        Public info93 As IODBPMCCNTL_info
        Public info94 As IODBPMCCNTL_info
        Public info95 As IODBPMCCNTL_info
        Public info96 As IODBPMCCNTL_info
        Public info97 As IODBPMCCNTL_info
        Public info98 As IODBPMCCNTL_info
        Public info99 As IODBPMCCNTL_info
        Public info100 As IODBPMCCNTL_info
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBPMCCNTL
        Public datano_s As Short
        Public dummy As Short
        Public datano_e As Short
        Public info As IODBPMCCNTL1
    End Structure

    ' pmc_rdalmmsg:read PMC alarm message 
    <StructLayout(LayoutKind.Sequential,CharSet:=CharSet.Ansi,Pack:=4)> _
    Public Structure ODBPMCALM_data
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=128)> _
        Public almmsg As String  ' alarm message 
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBPMCALM
        Public msg1 As ODBPMCALM_data
        Public msg2 As ODBPMCALM_data
        Public msg3 As ODBPMCALM_data
        Public msg4 As ODBPMCALM_data
        Public msg5 As ODBPMCALM_data
        Public msg6 As ODBPMCALM_data
        Public msg7 As ODBPMCALM_data
        Public msg8 As ODBPMCALM_data
        Public msg9 As ODBPMCALM_data
        Public msg10 As ODBPMCALM_data
    End Structure ' In case that the number of data is 10 

    ' pmc_getdtailerr:get detail error for pmc 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBPMCERR
        Public err_no As Short
        Public err_dtno As Short
    End Structure

    ' pmc_rdpmctitle:read pmc title data 
    <StructLayout(LayoutKind.Sequential,CharSet:=CharSet.Ansi,Pack:=4)> _
    Public Structure ODBPMCTITLE
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=48)> _
        Public mtb As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=48)> _
        Public machine As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=48)> _
        Public type As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=8)> _
        Public prgno As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=4)> _
        Public prgvers As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=48)> _
        Public prgdraw As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=32)> _
        Public date1 As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=48)> _
        Public design As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=48)> _
        Public written As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=48)> _
        Public remarks As String
    End Structure

    ' pmc_rdpmcrng_ext:read PMC data 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBPMCEXT
        Public type_a As Short    ' PMC address type 
        Public type_d As Short    ' PMC data type 
        Public datano_s As Short  ' start PMC address 
        Public datano_e As Short  ' end PMC address 
        Public err_code As Short  ' error code 
        Public reserved As Short  ' reserved 
        <MarshalAs(UnmanagedType.AsAny)> _
        Public data As Object     ' pointer to buffer 
    End Structure

    ' pmc_rdpmcaddr:read PMC address information 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBPMCADR_info
        Public pmc_adr As Byte
        Public adr_attr As Byte
        Public offset As Short  ' C# ushort
        Public top As Short  ' C# ushort
        Public num As Short  ' C# ushort
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBPMCADR1
        Public info1 As ODBPMCADR_info
        Public info2 As ODBPMCADR_info
        Public info3 As ODBPMCADR_info
        Public info4 As ODBPMCADR_info
        Public info5 As ODBPMCADR_info
        Public info6 As ODBPMCADR_info
        Public info7 As ODBPMCADR_info
        Public info8 As ODBPMCADR_info
        Public info9 As ODBPMCADR_info
        Public info10 As ODBPMCADR_info
        Public info11 As ODBPMCADR_info
        Public info12 As ODBPMCADR_info
        Public info13 As ODBPMCADR_info
        Public info14 As ODBPMCADR_info
        Public info15 As ODBPMCADR_info
        Public info16 As ODBPMCADR_info
        Public info17 As ODBPMCADR_info
        Public info18 As ODBPMCADR_info
        Public info19 As ODBPMCADR_info
        Public info20 As ODBPMCADR_info
        Public info21 As ODBPMCADR_info
        Public info22 As ODBPMCADR_info
        Public info23 As ODBPMCADR_info
        Public info24 As ODBPMCADR_info
        Public info25 As ODBPMCADR_info
        Public info26 As ODBPMCADR_info
        Public info27 As ODBPMCADR_info
        Public info28 As ODBPMCADR_info
        Public info29 As ODBPMCADR_info
        Public info30 As ODBPMCADR_info
        Public info31 As ODBPMCADR_info
        Public info32 As ODBPMCADR_info
        Public info33 As ODBPMCADR_info
        Public info34 As ODBPMCADR_info
        Public info35 As ODBPMCADR_info
        Public info36 As ODBPMCADR_info
        Public info37 As ODBPMCADR_info
        Public info38 As ODBPMCADR_info
        Public info39 As ODBPMCADR_info
        Public info40 As ODBPMCADR_info
        Public info41 As ODBPMCADR_info
        Public info42 As ODBPMCADR_info
        Public info43 As ODBPMCADR_info
        Public info44 As ODBPMCADR_info
        Public info45 As ODBPMCADR_info
        Public info46 As ODBPMCADR_info
        Public info47 As ODBPMCADR_info
        Public info48 As ODBPMCADR_info
        Public info49 As ODBPMCADR_info
        Public info50 As ODBPMCADR_info
        Public info51 As ODBPMCADR_info
        Public info52 As ODBPMCADR_info
        Public info53 As ODBPMCADR_info
        Public info54 As ODBPMCADR_info
        Public info55 As ODBPMCADR_info
        Public info56 As ODBPMCADR_info
        Public info57 As ODBPMCADR_info
        Public info58 As ODBPMCADR_info
        Public info59 As ODBPMCADR_info
        Public info60 As ODBPMCADR_info
        Public info61 As ODBPMCADR_info
        Public info62 As ODBPMCADR_info
        Public info63 As ODBPMCADR_info
        Public info64 As ODBPMCADR_info
    End Structure
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBPMCADR
        Public io_adr As Integer  ' C# uint
        Public datano As Short
        Public info As ODBPMCADR1
    End Structure


'--------------------------
' PROFIBUS function        
'--------------------------

    ' pmc_prfrdconfig:read PROFIBUS configration data 
    <StructLayout(LayoutKind.Sequential,CharSet:=CharSet.Ansi,Pack:=4)> _
    Public Structure ODBPRFCNF
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public master_ser As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=3)> _
        Public master_ver As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public slave_ser As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=3)> _
        Public slave_ver As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=5)> _
        Public cntl_ser As String
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=3)> _
        Public cntl_ver As String
    End Structure

    ' pmc_prfrdbusprm:read bus parameter for master function 
    ' pmc_prfwrbusprm:write bus parameter for master function 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBBUSPRM
        Public fdl_add As SByte
        Public baudrate As SByte
        Public tsl As Short  ' C# ushort
        Public min_tsdr As Short  ' C# ushort
        Public max_tsdr As Short  ' C# ushort
        Public tqui As Byte
        Public tset As Byte
        Public ttr As Integer
        Public gap As SByte
        Public hsa As SByte
        Public max_retry As SByte
        Public bp_flag As Byte
        Public min_slv_int As Short  ' C# ushort
        Public poll_tout As Short  ' C# ushort
        Public data_cntl As Short  ' C# ushort
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=6)> _
        Public reserve1 As Byte()
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=32)> _
        Public cls2_name As Byte()
        Public user_dlen As Short
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=62)> _
        Public user_data As Byte()
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=96)> _
        Public reserve2 As Byte()
    End Structure

    ' pmc_prfrdslvprm:read slave parameter for master function 
    ' pmc_prfwrslvprm:write slave parameter for master function 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBSLVPRM
        Public dis_enb As Short
        Public ident_no As Short  ' C# ushort
        Public slv_flag As Byte
        Public slv_type As Byte
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=12)> _
        Public reserve1 As Byte()
        Public slv_stat As Byte
        Public wd_fact1 As Byte
        Public wd_fact2 As Byte
        Public min_tsdr As Byte
        Public reserve2 As Char
        Public grp_ident As Byte
        Public user_plen As Short
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=32)> _
        Public user_pdata As Byte()
        Public cnfg_dlen As Short
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=126)> _
        Public cnfg_data As Byte()
        Public slv_ulen As Short
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=30)> _
        Public slv_udata As Byte()
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=8)> _
        Public reserve3 As Byte()
    End Structure

    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBSLVPRM2
        Public dis_enb As Short
        Public ident_no As Short  ' C# ushort
        Public slv_flag As Byte
        Public slv_type As Byte
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=12)> _
        Public reserve1 As Byte()
        Public slv_stat As Byte
        Public wd_fact1 As Byte
        Public wd_fact2 As Byte
        Public min_tsdr As Byte
        Public reserve2 As SByte
        Public grp_ident As Byte
        Public user_plen As Short
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=206)> _
        Public user_pdata As Byte()
        Public cnfg_dlen As Short
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=126)> _
        Public cnfg_data As Byte()
        Public slv_ulen As Short
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=30)> _
        Public slv_udata As Byte()
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=8)> _
        Public reserve3 As Byte()
    End Structure

    ' pmc_prfrdallcadr:read allocation address for master function 
    ' pmc_prfwrallcadr:set allocation address for master function 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBPRFADR
        Public di_size As Byte
        Public di_type As Byte
        Public di_addr As Short  ' C# ushort
        Public reserve1 As Short
        Public do_size As Byte
        Public do_type As Byte
        Public do_addr As Short  ' C# ushort
        Public reserve2 As Short
        Public dgn_size As Byte
        Public dgn_type As Byte
        Public dgn_addr As Short  ' C# ushort
    End Structure

    ' pmc_prfrdslvaddr:read allocation address for slave function 
    ' pmc_prfwrslvaddr:set allocation address for slave function 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBSLVADR
        Public slave_no As Byte
        Public di_size As Byte
        Public di_type As Byte
        Public di_addr As Short  ' C# ushort
        Public do_size As Byte
        Public do_type As Byte
        Public do_addr As Short  ' C# ushort
        <MarshalAs(UnmanagedType.ByValArray,SizeConst:=7)> _
        Public reserve As Byte()
    End Structure

    ' pmc_prfrdslvstat:read status for slave function 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBSLVST
        Public cnfg_stat As Byte
        Public prm_stat  As Byte
        Public wdg_stat  As SByte
        Public live_stat As Byte
        Public ident_no  As Short
    End Structure

    ' pmc_prfwrslvid:Writes slave index data of master function
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBSLVID
        Public dis_enb  As Short
        Public slave_no As Short
        Public nsl      As Short
        Public dgn_size As Byte
        Public dgn_type As Char
        Public dgn_addr As Short
    End Structure

    ' pmc_prfrdslvprm2:Reads slave parameter of master function(2)
    ' pmc_prfwrslvprm2:Writes slave parameter of master function(2)
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBSLVPRM3
        Public ident_no As Short
        Public slv_flag As Byte
        Public slv_type As Byte
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=12)> _
        Public reserve1 As Byte()
        Public slv_stat As Byte
        Public wd_fact1 As Byte
        Public wd_fact2 As Byte
        Public min_tsdr As Byte
        Public reserve2 As Char
        Public grp_ident As Byte
        Public user_plen As Short
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=206)> _
        Public user_pdata As Byte()
        Public slv_ulen As Short
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=30)> _
        Public slv_udata As Byte()
    End Structure

    ' pmc_prfrddido:Reads  DI/DO parameter of master function
    ' pmc_prfwrdido:Writes DI/DO parameter of master function
    <StructLayout(LayoutKind.Sequential, Pack:=4)> _
    Public Structure IODBDIDO
        Public slave_no As Short
        Public slot_no As Short
        Public di_size As Byte
        Public di_type As Char
        Public di_addr As Short
        Public do_size As Byte
        Public do_type As Char
        Public do_addr As Short
        Public shift As Short
        Public module_dlen As Byte
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=128)> _
        Public module_data As Byte()
    End Structure

    ' pmc_prfrdindiadr:Reads  indication address of master function
    ' pmc_prfwrindiadr:Writes indication address of master function
    <StructLayout(LayoutKind.Sequential, Pack:=4)> _
    Public Structure IODBINDEADR
        Public dummy As Byte
        Public indi_type As Char
        'Public indi_type As Byte
        Public indi_addr As Short
    End Structure

'-----------------------------------------------
' DS : Data server & Ethernet board function    
'-----------------------------------------------

    ' etb_rdparam : readÅ@the parameter of the Ethernet board 
    ' etb_wrparam : write the parameter of the Ethernet board 
    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure TCPPRM
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=16)> _
        Public OwnIPAddress As String
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=16)> _
        Public SubNetMask As String
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=16)> _
        Public RouterIPAddress As String
    End Structure

    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure HOSTPRM
        Public DataServerPort As Short
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=16)> _
        Public DataServerIPAddress As String
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=32)> _
        Public DataServerUserName As String
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=32)> _
        Public DataServerPassword As String
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=128)> _
        Public DataServerLoginDirectory As String
    End Structure

    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure FTPPRM
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=32)> _
        Public FTPServerUserName As String
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=32)> _
        Public FTPServerPassword As String
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=128)> _
        Public FTPServerLoginDirectory As String
    End Structure

    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ETBPRM
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=13)> _
        Public OwnMACAddress As String
        Public MaximumChannel As Short
        Public HDDExistence As Short
        Public NumberOfScreens As Short
    End Structure


    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBETP_TCP
        Public ParameterType As Short
        Public tcp As TCPPRM
    End Structure

    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBETP_HOST
        Public ParameterType As Short
        Public host As HOSTPRM
    End Structure

    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBETP_FTP
        Public ParameterType As Short
        Public ftp As FTPPRM
    End Structure

    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure IODBETP_ETB
        Public ParameterType As Short
        Public etb As ETBPRM
    End Structure

    <StructLayout(LayoutKind.Sequential,Pack:=4)> _
    Public Structure ODBETMSG
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=33)> _
        Public title As String
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=390)> _
        Public message As String
    End Structure

    ' ds_rdhddinfo : read information of the Data Server's HDD
    <StructLayout(LayoutKind.Sequential, Pack:=4)> _
    Public Structure ODBHDDINF
        Public file_num As Integer
        Public remainder_l As Integer
        Public remainder_h As Integer
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=32)> _
        Public current_dir As Byte()
    End Structure

    <StructLayout(LayoutKind.Sequential, Pack:=4)> _
    Public Structure ODBHDDDIR_data
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=64)> _
        Public file_name As String
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=80)> _
        Public comment As String
        Public attribute As Short
        Public reserved As Short
        Public size As Integer
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=16)> _
        Public dates As String
    End Structure
    <StructLayout(LayoutKind.Sequential, Pack:=4)> _
    Public Structure ODBHDDDIR
        Public data1 As ODBHDDDIR_data
        Public data2 As ODBHDDDIR_data
        Public data3 As ODBHDDDIR_data
        Public data4 As ODBHDDDIR_data
        Public data5 As ODBHDDDIR_data
        Public data6 As ODBHDDDIR_data
        Public data7 As ODBHDDDIR_data
        Public data8 As ODBHDDDIR_data
        Public data9 As ODBHDDDIR_data
        Public data10 As ODBHDDDIR_data
        Public data11 As ODBHDDDIR_data
        Public data12 As ODBHDDDIR_data
        Public data13 As ODBHDDDIR_data
        Public data14 As ODBHDDDIR_data
        Public data15 As ODBHDDDIR_data
        Public data16 As ODBHDDDIR_data
        Public data17 As ODBHDDDIR_data
        Public data18 As ODBHDDDIR_data
        Public data19 As ODBHDDDIR_data
        Public data20 As ODBHDDDIR_data
        Public data21 As ODBHDDDIR_data
        Public data22 As ODBHDDDIR_data
        Public data23 As ODBHDDDIR_data
        Public data24 As ODBHDDDIR_data
        Public data25 As ODBHDDDIR_data
        Public data26 As ODBHDDDIR_data
        Public data27 As ODBHDDDIR_data
        Public data28 As ODBHDDDIR_data
        Public data29 As ODBHDDDIR_data
        Public data30 As ODBHDDDIR_data
        Public data31 As ODBHDDDIR_data
        Public data32 As ODBHDDDIR_data
    End Structure

    ' ds_rdhostdir : read the file list of the host
    <StructLayout(LayoutKind.Sequential, Pack:=4)> _
    Public Structure ODBHOSTDIR_data
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=128)> _
        Public host_file As String
        'Public host_file As Char()
    End Structure
    <StructLayout(LayoutKind.Sequential, Pack:=4)> _
    Public Structure ODBHOSTDIR
        Public data1 As ODBHOSTDIR_data
        Public data2 As ODBHOSTDIR_data
        Public data3 As ODBHOSTDIR_data
        Public data4 As ODBHOSTDIR_data
        Public data5 As ODBHOSTDIR_data
        Public data6 As ODBHOSTDIR_data
        Public data7 As ODBHOSTDIR_data
        Public data8 As ODBHOSTDIR_data
        Public data9 As ODBHOSTDIR_data
        Public data10 As ODBHOSTDIR_data
        Public data11 As ODBHOSTDIR_data
        Public data12 As ODBHOSTDIR_data
        Public data13 As ODBHOSTDIR_data
        Public data14 As ODBHOSTDIR_data
        Public data15 As ODBHOSTDIR_data
        Public data16 As ODBHOSTDIR_data
        Public data17 As ODBHOSTDIR_data
        Public data18 As ODBHOSTDIR_data
        Public data19 As ODBHOSTDIR_data
        Public data20 As ODBHOSTDIR_data
        Public data21 As ODBHOSTDIR_data
        Public data22 As ODBHOSTDIR_data
        Public data23 As ODBHOSTDIR_data
        Public data24 As ODBHOSTDIR_data
        Public data25 As ODBHOSTDIR_data
        Public data26 As ODBHOSTDIR_data
        Public data27 As ODBHOSTDIR_data
        Public data28 As ODBHOSTDIR_data
        Public data29 As ODBHOSTDIR_data
        Public data30 As ODBHOSTDIR_data
        Public data31 As ODBHOSTDIR_data
        Public data32 As ODBHOSTDIR_data
    End Structure

    ' ds_rdmntinfo : read maintenance information
    <StructLayout(LayoutKind.Sequential, Pack:=4)> _
    Public Structure DSMNTINFO
        Public empty_cnt As Short
        Public total_size As Integer
        Public ReadPtr As Short
        Public WritePtr As Short
    End Structure


'--------------------------
' HSSB multiple connection 
'--------------------------

    ' cnc_rdnodeinfo:read node informations 
    <StructLayout(LayoutKind.Sequential,CharSet:=CharSet.Ansi,Pack:=4)> _
    Public Structure ODBNODE
        Public node_no As Integer
        Public io_base As Integer
        Public status As Integer
        Public cnc_type As Integer
        <MarshalAs(UnmanagedType.ByValTStr,SizeConst:=20)> _
        Public node_name As String
    End Structure


'-------------------------------------
' CNC: Control axis / spindle related 
'-------------------------------------

    ' read actual axis feedrate(F) 
    Declare Function cnc_actf Lib "FWLIB32.DLL" _
       (ByVal FlibHndl As Integer, ByRef a As ODBACT) As Short

    ' read absolute axis position 
    Declare Function cnc_absolute Lib "FWLIB32.DLL" _
       (ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Short, ByRef c As ODBAXIS) As Short

    ' read machine axis position 
    Declare Function cnc_machine Lib "FWLIB32.DLL" _
       (ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Short, ByRef c As ODBAXIS) As Short

    ' read relative axis position 
    Declare Function cnc_relative Lib "FWLIB32.DLL" _
       (ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Short, ByRef c As ODBAXIS) As Short

    ' read distance to go 
    Declare Function cnc_distance Lib "FWLIB32.DLL" _
       (ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Short, ByRef c As ODBAXIS) As Short

    ' read skip position 
    Declare Function cnc_skip Lib "FWLIB32.DLL" _
       (ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Short, ByRef c As ODBAXIS) As Short

    ' read servo delay value 
    Declare Function cnc_srvdelay Lib "FWLIB32.DLL" _
       (ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Short, ByRef c As ODBAXIS) As Short

    ' read acceleration/deceleration delay value 
    Declare Function cnc_accdecdly Lib "FWLIB32.DLL" _
       (ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Short, ByRef c As ODBAXIS) As Short

    ' read all dynamic data 
#If ONO8D = Nothing Then
    Declare Function cnc_rddynamic Lib "FWLIB32.DLL" _
       (ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Short, ByRef c As ODBDY_1) As Short
    Declare Function cnc_rddynamic Lib "FWLIB32.DLL" _
       (ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Short, ByRef c As ODBDY_2) As Short
#Else
    Declare Function cnc_rddynamico8 Lib "FWLIB32.DLL" _
       (ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Short, ByRef c As ODBDY_1) As Short
    Declare Function cnc_rddynamico8 Lib "FWLIB32.DLL" _
       (ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Short, ByRef c As ODBDY_2) As Short
#End If

    ' read all dynamic data 
    Declare Function cnc_rddynamic2 Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Short, ByRef c As ODBDY2_1) As Short
    Declare Function cnc_rddynamic2 Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Short, ByRef c As ODBDY2_2) As Short

    ' read actual spindle speed(S) 
    Declare Function cnc_acts Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As ODBACT) As Short

    ' read actual spindle speed(S) (All or spesified) 
    Declare Function cnc_acts2 Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As ODBACT2) As Short

    ' set origin / preset relative axis position 
    Declare Function cnc_wrrelpos Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As IDBWRR ) As Short

    ' preset work coordinate 
    Declare Function cnc_prstwkcd Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As IDBWRA ) As Short

    ' read manual overlapped motion value 
    Declare Function cnc_rdmovrlap Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Short, ByRef c As IODBOVL ) As Short

    ' cancel manual overlapped motion value 
    Declare Function cnc_canmovrlap Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short ) As Short

    ' read load information of serial spindle 
    Declare Function cnc_rdspload Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As ODBSPN ) As Short

    ' read maximum r.p.m. ratio of serial spindle 
    Declare Function cnc_rdspmaxrpm Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As ODBSPN ) As Short

    ' read gear ratio of serial spindle 
    Declare Function cnc_rdspgear Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As ODBSPN ) As Short

    ' read absolute axis position 2 
    Declare Function cnc_absolute2 Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Short, ByRef c As ODBAXIS) As Short

    ' read relative axis position 2 
    Declare Function cnc_relative2 Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Short, ByRef c As ODBAXIS) As Short

    ' set wire vertival position 
    Declare Function cnc_setvrtclpos Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short ) As Short

    ' set wire threading position 
    Declare Function cnc_setthrdngpos Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer ) As Short

    ' read tool position 
    Declare Function cnc_rdposition Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As Short, ByRef c As ODBPOS) As Short

    ' read current speed 
    Declare Function cnc_rdspeed Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As ODBSPEED ) As Short

    ' read servo load meter 
    Declare Function cnc_rdsvmeter Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As Short, ByRef b As ODBSVLOAD ) As Short

    ' read spindle load meter 
    Declare Function cnc_rdspmeter Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As Short, ByRef c As ODBSPLOAD ) As Short

    ' read handle interruption 
    Declare Function cnc_rdhndintrpt Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As Short, ByRef c As ODBHND ) As Short

    ' read manual feed for 5-axis machining
    Declare Function cnc_rd5axmandt Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As ODB5AXMAN ) As Short

    ' read amount of machine axes movement of manual feed for 5-axis machining
    Declare Function cnc_rd5axovrlap Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Short, ByRef c As ODBAXIS ) As Short

    ' clear pulse values of manual feed for 5-axis machining
    Declare Function cnc_clr5axpls Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short ) As Short

    ' read constant surface speed 
    Declare Function cnc_rdspcss Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As ODBCSS ) As Short

    ' read execution program pointer
    Declare Function cnc_rdexecpt Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As PRGPNT, ByRef b As PRGPNT ) As Short

    ' read various axis data
    Declare Function cnc_rdaxisdata Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByVal b() As Short, ByVal c As Short, ByRef d As Short, ByRef c As ODBAXDT ) As Short

'----------------------
' CNC: Program related 
'----------------------

    ' start downloading NC program 
    Declare Function cnc_dwnstart Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer) As Short

    ' download NC program 
    Declare Function cnc_download Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As String, ByVal b As Short) As Short

    ' download NC program(conditional) 
    Declare Function cnc_cdownload Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As String, ByVal b As Short) As Short

    ' end of downloading NC program 
    Declare Function cnc_dwnend Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer) As Short

    ' end of downloading NC program 2 
    Declare Function cnc_dwnend2 Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As String) As Short

    ' start downloading NC program 3 
    Declare Function cnc_dwnstart3 Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short) As Short

    ' start downloading NC program 3 special 
    Declare Function cnc_dwnstart3_f Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, _
        ByVal a As Short, ByVal b As String, ByVal c As String) As Short

    ' download NC program 3 
    Declare Function cnc_download3 Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As Integer, ByVal b As String) As Short

    ' end of downloading NC program 3 
    Declare Function cnc_dwnend3 Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer) As Short

    ' start downloading NC program 4
    Declare Function cnc_dwnstart4 Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As String) As Short

    ' download NC program 4
    Declare Function cnc_download4 Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As Integer, ByVal b As String) As Short

    ' end of downloading NC program 4
    Declare Function cnc_dwnend4 Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer) As Short

    ' start verification of NC program 
    Declare Function cnc_vrfstart Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer) As Short

    ' verify NC program 
    Declare Function cnc_verify Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As String, ByVal b As Short) As Short

    ' verify NC program(conditional) 
    Declare Function cnc_cverify Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As String, ByVal b As Short) As Short

    ' end of verification 
    Declare Function cnc_vrfend Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer) As Short

    ' start verification of NC program
    Declare Function cnc_vrfstart4 Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As String) As Short

    ' verify NC program
    Declare Function cnc_verify4 Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As Integer, <[In], Out> ByVal a() As Char) As Short
    
    ' end of verification
    Declare Function cnc_vrfend4 Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer) As Short

    ' start downloading DNC program 
    Declare Function cnc_dncstart Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer) As Short

    ' download DNC program 
    Declare Function cnc_dnc Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As String, ByVal b As Integer) As Short

    ' download DNC program(conditional) 
    Declare Function cnc_cdnc Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As String, ByVal b As Integer) As Short

    ' end of downloading DNC program 
    Declare Function cnc_dncend Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer ) As Short

    ' start downloading DNC program 2 
    Declare Function cnc_dncstart2 Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, <[In], Out> ByVal a() As Char) As Short

    ' download DNC program 2 
    Declare Function cnc_dnc2 Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As Integer, ByVal b As String) As Short

    ' end of downloading DNC program 2 
    Declare Function cnc_dncend2 Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short ) As Short

    ' read the diagnosis data of DNC operation 
    Declare Function cnc_rddncdgndt Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As ODBDNCDGN ) As Short

    ' start uploading NC program 
#If ONO8D = Nothing Then
    Declare Function cnc_upstart Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short ) As Short
#Else
    Declare Function cnc_upstart Lib "FWLIB32.DLL" Alias "cnc_upstarto8" _
        ( ByVal FlibHndl As Integer, ByVal a As Integer ) As Short
#End If

    ' upload NC program 
    Declare Function cnc_upload Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As ODBUP, ByRef b As Integer) As Short

    ' upload NC program(conditional) 
    Declare Function cnc_cupload Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As ODBUP, ByRef b As Integer) As Short

    ' end of uploading NC program 
    Declare Function cnc_upend Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer ) As Short

    ' start uploading NC program 3 
    Declare Function cnc_upstart3 Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Integer, ByVal c As Integer ) As Short

    ' start uploading NC program special 3 
    Declare Function cnc_upstart3_f Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, _
        ByVal a As Short, ByVal b As String, ByVal c As String) As Short

    ' upload NC program 3 
    Declare Function cnc_upload3 Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As Integer, <[In], Out> ByVal c() As Char) As Short

    ' end of uploading NC program 3 
    Declare Function cnc_upend3 Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer ) As Short

    ' start uploading NC program 4
    Declare Function cnc_upstart4 Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As String) As Short

    ' upload NC program 4
    Declare Function cnc_upload4 Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As Integer, <[In], Out> ByVal b() As Char) As Short

    ' end of uploading NC program 4
    Declare Function cnc_upend4 Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer ) As Short

    ' read buffer status for downloading/verification NC program 
    Declare Function cnc_buff Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As ODBBUF ) As Short

    ' search specified program 
#If ONO8D = Nothing Then
    Declare Function cnc_search Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short ) As Short
#Else
    Declare Function cnc_search Lib "FWLIB32.DLL" Alias "cnc_searcho8" _
        ( ByVal FlibHndl As Integer, ByVal a As Integer ) As Short
#End If

    ' search specified program 
    Declare Function cnc_search2 Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Integer ) As Short

    ' delete all programs 
    Declare Function cnc_delall Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer ) As Short

    ' delete specified program 
#If ONO8D = Nothing Then
    Declare Function cnc_delete Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short ) As Short
#Else
    Declare Function cnc_delete Lib "FWLIB32.DLL" Alias "cnc_deleteo8" _
        ( ByVal FlibHndl As Integer, ByVal a As Integer ) As Short
#End If

    ' delete program (area specified)
    Declare Function cnc_delrange Lib "FWLIB32.DLL" Alias "cnc_delrange" _
        ( ByVal FlibHndl As Integer, ByVal a As Integer , ByVal b As Integer) As Short

' read program directory 
#If ONO8D = Nothing Then
    Declare Function cnc_rdprogdir Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Short, ByVal c As Short, ByVal d As Integer, ByRef e As PRGDIR) As Short
#Else
    Declare Function cnc_rdprogdir Lib "FWLIB32.DLL" Alias "cnc_rdprogdiro8" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Short, ByVal c As Short, ByVal d As Integer, ByRef e As PRGDIR) As Short
#End If

    ' read program information 
    Declare Function cnc_rdproginfo Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Short, ByRef c As ODBNC_1) As Short
    Declare Function cnc_rdproginfo Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Short, ByRef c As ODBNC_2) As Short

    ' read program number under execution 
#If ONO8D = Nothing Then
    Declare Function cnc_rdprgnum Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As ODBPRO ) As Short
#Else
    Declare Function cnc_rdprgnum Lib "FWLIB32.DLL" Alias "cnc_rdprgnumo8" _
        ( ByVal FlibHndl As Integer, ByRef a As ODBPRO ) As Short
#End If

    ' read program name under execution
    Declare Function cnc_exeprgname Lib "FWLIB32.DLL" Alias "cnc_exeprgname" _
        ( ByVal FlibHndl As Integer, ByRef a As ODBEXEPRG ) As Short

    ' read sequence number under execution 
    Declare Function cnc_rdseqnum Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As ODBSEQ ) As Short

    ' search specified sequence number 
    Declare Function cnc_seqsrch Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Integer ) As Short

    ' search specified sequence number (2)
    Declare Function cnc_seqsrch2 Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Integer ) As Short

    ' rewind cursor of NC program 
    Declare Function cnc_rewind Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer ) As Short

    ' read block counter 
    Declare Function cnc_rdblkcount Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As Integer ) As Short

    ' read program under execution 
    Declare Function cnc_rdexecprog Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As Integer, ByRef b As Short, <[In], Out> ByVal c() As Char) As Short

    ' read program for MDI operation 
    Declare Function cnc_rdmdiprog Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As Short, <[In], Out> ByVal c() As Char) As Short

    ' write program for MDI operation 
    Declare Function cnc_wrmdiprog Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As String) As Short

' read execution pointer for MDI operation 
#If ONO8D = Nothing Then
    Declare Function cnc_rdmdipntr Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As ODBMDIP ) As Short
#Else
    Declare Function cnc_rdmdipntr Lib "FWLIB32.DLL" Alias "cnc_rdmdipntro8" _
        ( ByVal FlibHndl As Integer, ByRef a As ODBMDIP ) As Short
#End If

    ' write execution pointer for MDI operation 
    Declare Function cnc_wrmdipntr Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Integer ) As Short

    ' register new program 
    Declare Function cnc_newprog Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Integer ) As Short

    ' copy program 
    Declare Function cnc_copyprog Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer ) As Short

    ' rename program 
    Declare Function cnc_renameprog Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer ) As Short

    ' condense program 
    Declare Function cnc_condense Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Integer ) As Short

    ' merge program
    Declare Function cnc_mergeprog Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Integer, ByVal c As Integer, ByVal d As Integer ) As Short

    ' read current program and its pointer 
    Declare Function cnc_rdactpt Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As Integer, ByRef b As Integer ) As Short

    ' read current program and its pointer and UV macro pointer 
    Declare Function cnc_rduvactpt Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As Integer, ByRef b As Integer, ByRef c As Integer ) As Short

    ' set current program and its pointer 
    Declare Function cnc_wractpt Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Short, ByRef c As Integer ) As Short

    ' line edit (read program) 
    Declare Function cnc_rdprogline Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, _
        ByVal a As Integer, ByVal b As Integer, <[In], Out> ByVal c() As Char, ByRef d As Integer, ByRef e As Integer) As Short

    ' line edit (read program) 
    Declare Function cnc_rdprogline2 Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, _
        ByVal a As Integer, ByVal b As Integer, <[In], Out> ByVal c() As Char, ByRef d As Integer, ByRef e As Integer) As Short

    ' line edit (write program) 
    Declare Function cnc_wrprogline Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, ByVal c As String, ByVal d As Integer) As Short

    ' line edit (delete line in program) 
    Declare Function cnc_delprogline Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, ByVal c As Integer) As Short

    ' line edit (search string) 
    Declare Function cnc_searchword Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, _
        ByVal a As Integer, ByVal b As Integer, ByVal c As Short, ByVal d As Short, ByVal e As Integer, ByVal f As String) As Short

    ' line edit (search string) 
    Declare Function cnc_searchresult Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As Integer) As Short

    ' line edit (read program by file name)
    Declare Function cnc_rdpdf_line Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As String , ByVal b As Integer , <[In], Out> ByVal c() As Char , ByRef d As Integer , ByRef e As Integer ) As Short

    ' program lock 
    Declare Function cnc_setpglock Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Integer ) As Short

    ' program unlock 
    Declare Function cnc_resetpglock Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Integer ) As Short

    ' read the status of the program lock 
    Declare Function cnc_rdpglockstat Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As Integer, ByRef b As Integer ) As Short

    ' create file or directory
    Declare Function cnc_pdf_add Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As String ) As Short

    ' condense program file
    Declare Function cnc_pdf_cond Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As String ) As Short

    ' change attribute of program file and directory
    Declare Function cnc_wrpdf_attr Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As String , ByRef b As IDBPDFTDIR ) As Short

    ' copy program file
    Declare Function cnc_pdf_copy Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As String , ByVal b As String ) As Short

    ' delete file or directory
    Declare Function cnc_pdf_del Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As String ) As Short

    ' line edit (write program by file name)
    Declare Function cnc_wrpdf_line Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As String, ByVal b As Integer, ByVal c As String, ByVal d As Integer ) As Short

    ' line edit (delete line by file name)
    Declare Function cnc_pdf_delline Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As String , ByVal b As Integer , ByVal c As Integer ) As Short

    ' move program file
    Declare Function cnc_pdf_move Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As String , ByVal b As String ) As Short

    ' read current program and its pointer
    Declare Function cnc_pdf_rdactpt Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, <[In], Out> ByVal a() As Char , ByRef b As Integer ) As Short

    ' read selected file name
    Declare Function cnc_pdf_rdmain Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, <[In], Out> ByVal a() As Char ) As Short

    ' rename file or directory
    Declare Function cnc_pdf_rename Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As String , ByVal b As String ) As Short

    ' line edit (search string)
    Declare Function cnc_pdf_searchword Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As String , ByVal b As Integer , ByVal c As Integer , ByVal d As Integer , ByVal e As Integer , ByVal f As String ) As Short

    ' line edit (search string)
    Declare Function cnc_pdf_searchresult Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As Integer ) As Short

    ' select program file
    Declare Function cnc_pdf_slctmain Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As String ) As Short

    ' set current program and its pointer
    Declare Function cnc_pdf_wractpt Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As String , ByVal b As Short , ByRef c As Integer ) As Short

    ' read program drive information
    Declare Function cnc_rdpdf_inf Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As String , ByVal b As Short , ByRef c As ODBPDFINF ) As Short

    ' read program drive directory
    Declare Function cnc_rdpdf_drive Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As ODBPDFDRV ) As Short

    ' read current directory
    Declare Function cnc_rdpdf_curdir Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short , <[In], Out> ByVal b() As Char ) As Short

    ' set current directory
    Declare Function cnc_wrpdf_curdir Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short , <[In], Out> ByVal b() As Char ) As Short
    
    ' read directory (sub directories)
    Declare Function cnc_rdpdf_subdir Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As Short , ByRef b As IDBPDFSDIR , ByRef b As ODBPDFSDIR ) As Short

    ' read directory (all files) 
    Declare Function cnc_rdpdf_alldir Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As Short , ByRef b As IDBPDFADIR, ByRef c As ODBPDFADIR ) As Short

    ' read file count in directory
    Declare Function cnc_rdpdf_subdirn Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As String , ByRef b As ODBPDFNFIL ) As Short

'---------------------------
' CNC: NC file data related 
'---------------------------

    ' read tool offset value
    Declare Function cnc_rdtofs Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Short, ByVal c As Short, ByRef d As ODBTOFS) As Short

    ' write tool offset value 
    Declare Function cnc_wrtofs Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Short, ByVal c As Short, ByVal d As Integer ) As Short

    ' read tool offset value(area specified) 
    Declare Function cnc_rdtofsr Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, _
        ByVal a As Short, ByVal b As Short, ByVal c As Short, ByVal d As Short, ByRef e As IODBTO_1_1) As Short
    Declare Function cnc_rdtofsr Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, _
        ByVal a As Short, ByVal b As Short, ByVal c As Short, ByVal d As Short, ByRef e As IODBTO_1_2) As Short
    Declare Function cnc_rdtofsr Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, _
        ByVal a As Short, ByVal b As Short, ByVal c As Short, ByVal d As Short, ByRef e As IODBTO_1_3) As Short
    Declare Function cnc_rdtofsr Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, _
        ByVal a As Short, ByVal b As Short, ByVal c As Short, ByVal d As Short, ByRef e As IODBTO_2) As Short
    Declare Function cnc_rdtofsr Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, _
        ByVal a As Short, ByVal b As Short, ByVal c As Short, ByVal d As Short, ByRef e As IODBTO_3) As Short

    ' write tool offset value(area specified) 
    Declare Function cnc_wrtofsr Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As IODBTO_1_1 ) As Short
    Declare Function cnc_wrtofsr Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As IODBTO_1_2 ) As Short
    Declare Function cnc_wrtofsr Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As IODBTO_1_3 ) As Short
    Declare Function cnc_wrtofsr Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As IODBTO_2 ) As Short
    Declare Function cnc_wrtofsr Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As IODBTO_3 ) As Short

    ' read work zero offset value 
    Declare Function cnc_rdzofs Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Short, ByVal c As Short, ByRef d As IODBZOFS ) As Short

    ' write work zero offset value 
    Declare Function cnc_wrzofs Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As IODBZOFS ) As Short

    ' read work zero offset value(area specified) 
    Declare Function cnc_rdzofsr Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Short, ByVal c As Short, ByVal d As Short, ByRef e As IODBZOR ) As Short

    ' write work zero offset value(area specified) 
    Declare Function cnc_wrzofsr Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As IODBZOR ) As Short

    ' read mesured point value 
    Declare Function cnc_rdmsptype Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Short, ByVal c As Short, ByRef d As IODBMSTP) As Short

    ' write mesured point value 
    Declare Function cnc_wrmsptype Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByRef d As IODBMSTP) As Short

    ' read parameter 
    Declare Function cnc_rdparam Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Short, ByVal c As Short, ByRef d As IODBPSD_1) As Short
    Declare Function cnc_rdparam Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Short, ByVal c As Short, ByRef d As IODBPSD_2) As Short
    Declare Function cnc_rdparam Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Short, ByVal c As Short, ByRef d As IODBPSD_3) As Short
    Declare Function cnc_rdparam Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Short, ByVal c As Short, ByRef d As IODBPSD_4) As Short

    ' write parameter 
    Declare Function cnc_wrparam Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As IODBPSD_1 ) As Short
    Declare Function cnc_wrparam Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As IODBPSD_2 ) As Short
    Declare Function cnc_wrparam Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As IODBPSD_3 ) As Short
    Declare Function cnc_wrparam Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As IODBPSD_4 ) As Short

    ' read parameter 
    Declare Function cnc_rdparam3 Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Short, ByVal c As Short, ByVal d As Short, ByRef e As IODBPSD_1) As Short
    Declare Function cnc_rdparam3 Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Short, ByVal c As Short, ByVal d As Short, ByRef e As IODBPSD_2) As Short
    Declare Function cnc_rdparam3 Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Short, ByVal c As Short, ByVal d As Short, ByRef e As IODBPSD_3) As Short
    Declare Function cnc_rdparam3 Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Short, ByVal c As Short, ByVal d As Short, ByRef e As IODBPSD_4) As Short

    ' read parameter(area specified) 
    Declare Function cnc_rdparar Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, _
        ByRef a As Short, ByVal b As Short, ByRef c As Short, ByRef d As Short, ByRef e As IODBPSD_A ) As Short
    Declare Function cnc_rdparar Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, _
        ByRef a As Short, ByVal b As Short, ByRef c As Short, ByRef d As Short, ByRef e As IODBPSD_B ) As Short
    Declare Function cnc_rdparar Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, _
        ByRef a As Short, ByVal b As Short, ByRef c As Short, ByRef d As Short, ByRef e As IODBPSD_C ) As Short
    Declare Function cnc_rdparar Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, _
        ByRef a As Short, ByVal b As Short, ByRef c As Short, ByRef d As Short, ByRef e As IODBPSD_D ) As Short

    ' write parameter(area specified) 
    Declare Function cnc_wrparas Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As IODBPSD_A ) As Short
    Declare Function cnc_wrparas Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As IODBPSD_B ) As Short
    Declare Function cnc_wrparas Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As IODBPSD_C ) As Short
    Declare Function cnc_wrparas Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As IODBPSD_D ) As Short

    ' read setting data 
    Declare Function cnc_rdset Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Short, ByVal c As Short, ByRef d As IODBPSD_1 ) As Short
    Declare Function cnc_rdset Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Short, ByVal c As Short, ByRef d As IODBPSD_2 ) As Short
    Declare Function cnc_rdset Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Short, ByVal c As Short, ByRef d As IODBPSD_3 ) As Short
    Declare Function cnc_rdset Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Short, ByVal c As Short, ByRef d As IODBPSD_4 ) As Short

    ' write setting data 
    Declare Function cnc_wrset Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As IODBPSD_1 ) As Short
    Declare Function cnc_wrset Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As IODBPSD_2 ) As Short
    Declare Function cnc_wrset Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As IODBPSD_3 ) As Short
    Declare Function cnc_wrset Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As IODBPSD_4 ) As Short

    ' read setting data(area specified) 
    Declare Function cnc_rdsetr Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, _
        ByRef a As Short, ByVal b As Short, ByRef c As Short, ByRef d As Short, ByRef e As IODBPSD_A ) As Short
    Declare Function cnc_rdsetr Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, _
        ByRef a As Short, ByVal b As Short, ByRef c As Short, ByRef d As Short, ByRef e As IODBPSD_B ) As Short
    Declare Function cnc_rdsetr Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, _
        ByRef a As Short, ByVal b As Short, ByRef c As Short, ByRef d As Short, ByRef e As IODBPSD_C ) As Short
    Declare Function cnc_rdsetr Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, _
        ByRef a As Short, ByVal b As Short, ByRef c As Short, ByRef d As Short, ByRef e As IODBPSD_D ) As Short

    ' write setting data(area specified) 
    Declare Function cnc_wrsets Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As IODBPSD_A ) As Short
    Declare Function cnc_wrsets Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As IODBPSD_B ) As Short
    Declare Function cnc_wrsets Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As IODBPSD_C ) As Short
    Declare Function cnc_wrsets Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As IODBPSD_D ) As Short

    ' read parameters 
    Declare Function cnc_rdparam_ext Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As IODBPRMNO, ByVal b As Short, ByRef c As IODBPRM) As Short

    ' async parameter write start 
    Declare Function cnc_start_async_wrparam Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As IODBPRM ) As Short

    ' async parameter write end 
    Declare Function cnc_end_async_wrparam Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As Short ) As Short

    ' read cause of busy for async parameter write 
    Declare Function cnc_async_busy_state Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As Short ) As Short

    ' read diagnosis data 
    Declare Function cnc_rddiag_ext Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As IODBPRMNO, ByVal b As Short, ByRef c As IODBPRM) As Short

    ' read pitch error compensation data(area specified) 
    Declare Function cnc_rdpitchr Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, _
        ByVal a As Short, ByVal b As Short, ByVal c As Short, ByRef d As IODBPI ) As Short

    ' write pitch error compensation data(area specified) 
    Declare Function cnc_wrpitchr Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As IODBPI ) As Short

    ' read custom macro variable 
    Declare Function cnc_rdmacro Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Short, ByRef c As ODBM ) As Short

    ' write custom macro variable 
    Declare Function cnc_wrmacro Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Short, ByVal c As Integer, ByVal d As Short ) As Short

    ' read custom macro variables(area specified) 
    Declare Function cnc_rdmacror Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Short, ByVal c As Short, ByRef d As IODBMR ) As Short

    ' write custom macro variables(area specified) 
    Declare Function cnc_wrmacror Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As IODBMR ) As Short

    ' read custom macro variables(IEEE double version)
    Declare Function cnc_rdmacror2 Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Integer, ByRef b As Integer, ByVal c() As Double) As Short

    ' write custom macro variables(IEEE double version)
    Declare Function cnc_wrmacror2 Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Integer, ByRef b As Integer, ByVal c() As Double) As Short

    ' read P code macro variable 
    Declare Function cnc_rdpmacro Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Integer, ByRef b As ODBPM) As Short

    ' write P code macro variable 
    Declare Function cnc_wrpmacro Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, ByVal c As Short) As Short

    ' read P code macro variables(area specified) 
    Declare Function cnc_rdpmacror Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, _
        ByVal a As Integer, ByVal b As Integer, ByVal c As Integer, ByRef d As IODBPR) As Short

    ' write P code macro variables(area specified) 
    Declare Function cnc_wrpmacror Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Integer, ByRef b As IODBPR) As Short

    ' read P code macro variables(IEEE double version)
    Declare Function cnc_rdpmacror2 Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Integer, ByRef b As Integer, ByVal c As Short, <[In](), Out()> ByVal d() As Double) As Short

    ' write P code macro variables(IEEE double version)
    Declare Function cnc_wrpmacror2 Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Integer, ByRef b As Integer, ByVal c As Short, <[In](), Out()> ByVal d() As Double) As Short

    ' read tool offset information 
    Declare Function cnc_rdtofsinfo Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As ODBTLINF) As Short

    ' read tool offset information(2)
    Declare Function cnc_rdtofsinfo2 Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As ODBTLINF2) As Short

    ' read work zero offset information 
    Declare Function cnc_rdzofsinfo Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As Short) As Short

    ' read pitch error compensation data information 
    Declare Function cnc_rdpitchinfo Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As Short) As Short

    ' read custom macro variable information 
    Declare Function cnc_rdmacroinfo Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As ODBMVINF) As Short

    ' read P code macro variable information 
    Declare Function cnc_rdpmacroinfo Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As ODBPMINF) As Short

    ' read validity of tool offset
    Declare Function cnc_tofs_rnge Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, ByRef c As ODBDATRNG) As Short

    ' read validity of work zero offset
    Declare Function cnc_zofs_rnge Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, ByRef c As ODBDATRNG) As Short

    ' read validity of work zero offset
    Declare Function cnc_wksft_rnge Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Integer, ByRef b As ODBDATRNG) As Short

    ' read the information for function cnc_rdhsparam()
    Declare Function cnc_rdhsprminfo Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Integer, ByRef b As HSPINFO_data) As Short

    ' read parameters at the high speed
    Declare Function cnc_rdhsparam Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Integer, ByRef b As HSPINFO, ByRef c As HSPDATA_1) As Short

    Declare Function cnc_rdhsparam Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Integer, ByRef b As HSPINFO, ByRef c As HSPDATA_2) As Short

    Declare Function cnc_rdhsparam Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Integer, ByRef b As HSPINFO, ByRef c As HSPDATA_3) As Short


    '----------------------------------------
    ' CNC: Tool life management data related 
    '----------------------------------------

    ' read tool life management data(tool group number) 
    Declare Function cnc_rdgrpid Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As ODBTLIFE1) As Short

    ' read tool life management data(number of tool groups) 
    Declare Function cnc_rdngrp Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As ODBTLIFE2) As Short

    ' read tool life management data(number of tools) 
    Declare Function cnc_rdntool Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As ODBTLIFE3) As Short

    ' read tool life management data(tool life) 
    Declare Function cnc_rdlife Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As ODBTLIFE3) As Short

    ' read tool life management data(tool lift counter) 
    Declare Function cnc_rdcount Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As ODBTLIFE3) As Short

    ' read tool life management data(tool length number-1) 
    Declare Function cnc_rd1length Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Short, ByRef c As ODBTLIFE4) As Short

    ' read tool life management data(tool length number-2) 
    Declare Function cnc_rd2length Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Short, ByRef c As ODBTLIFE4) As Short

    ' read tool life management data(cutter compensation no.-1) 
    Declare Function cnc_rd1radius Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Short, ByRef c As ODBTLIFE4) As Short

    ' read tool life management data(cutter compensation no.-2) 
    Declare Function cnc_rd2radius Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Short, ByRef c As ODBTLIFE4) As Short

    ' read tool life management data(tool information-1) 
    Declare Function cnc_t1info Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Short, ByRef c As ODBTLIFE4) As Short

    ' read tool life management data(tool information-2) 
    Declare Function cnc_t2info Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Short, ByRef c As ODBTLIFE4) As Short

    ' read tool life management data(tool number) 
    Declare Function cnc_toolnum Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Short, ByRef c As ODBTLIFE4) As Short

    ' read tool life management data(tool number, tool life, tool life counter)(area specified) 
    Declare Function cnc_rdtoolrng Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Short, ByVal c As Short, ByRef d As IODBTR) As Short

    ' read tool life management data(all data within group) 
    Declare Function cnc_rdtoolgrp Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Short, ByRef c As ODBTG) As Short

    ' write tool life management data(tool life counter) (area specified) 
    Declare Function cnc_wrcountr Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As IDBWRC) As Short

    ' read tool life management data(used tool group number) 
    Declare Function cnc_rdusegrpid Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As ODBUSEGR) As Short

    ' read tool life management data(max. number of tool groups) 
    Declare Function cnc_rdmaxgrp Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As ODBLFNO) As Short

    ' read tool life management data(maximum number of tool within group) 
    Declare Function cnc_rdmaxtool Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As ODBLFNO) As Short

    ' read tool life management data(used tool no. within group) 
    Declare Function cnc_rdusetlno Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Short, ByVal c As Short, ByRef d As ODBTLUSE) As Short

    ' read tool life management data(tool data1) 
    Declare Function cnc_rd1tlifedata Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Short, ByRef c As IODBTD) As Short

    ' read tool life management data(tool data2) 
    Declare Function cnc_rd2tlifedata Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Short, ByRef c As IODBTD) As Short

    ' write tool life management data(tool data1) 
    Declare Function cnc_wr1tlifedata Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As IODBTD) As Short

    ' write tool life management data(tool data2) 
    Declare Function cnc_wr2tlifedata Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As IODBTD) As Short

    ' read tool life management data(tool group information) 
    Declare Function cnc_rdgrpinfo Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Short, ByVal c As Short, ByRef d As IODBTGI) As Short

    ' read tool life management data(tool group information 2) 
    Declare Function cnc_rdgrpinfo2 Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Short, ByVal c As Short, ByRef d As IODBTGI2) As Short

    ' read tool life management data(tool group information 3) 
    Declare Function cnc_rdgrpinfo3 Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Short, ByVal c As Short, ByRef d As IODBTGI3) As Short

    ' read tool life management data(tool group information 4)
    Declare Function cnc_rdgrpinfo4 Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Short, ByVal c As Short, ByRef d As Short, ByRef e As IODBTGI4) As Short

    ' write tool life management data(tool group information) 
    Declare Function cnc_wrgrpinfo Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As IODBTGI) As Short

    ' write tool life management data(tool group information 2) 
    Declare Function cnc_wrgrpinfo2 Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As IODBTGI2) As Short

    ' write tool life management data(tool group information 3) 
    Declare Function cnc_wrgrpinfo3 Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As IODBTGI3) As Short

    ' delete tool life management data(tool group) 
    Declare Function cnc_deltlifegrp Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short) As Short

    ' insert tool life management data(tool data) 
    Declare Function cnc_instlifedt Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As IDBITD) As Short

    ' delete tool life management data(tool data) 
    Declare Function cnc_deltlifedt Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Short) As Short

    ' clear tool life management data(tool life counter, tool information)(area specified) 
    Declare Function cnc_clrcntinfo Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Short) As Short

    ' read tool life management data(tool group number) 2 
    Declare Function cnc_rdgrpid2 Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Integer, ByRef b As ODBTLIFE5) As Short

    ' read tool life management data(tool data1) 2 
    Declare Function cnc_rd1tlifedat2 Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Integer, ByRef c As IODBTD2) As Short

    ' write tool life management data(tool data1) 2 
    Declare Function cnc_wr1tlifedat2 Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As IODBTD2) As Short

    ' read tool life management data 
    Declare Function cnc_rdtlinfo Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As ODBTLINFO) As Short

    ' read tool life management data(used tool group number) 
    Declare Function cnc_rdtlusegrp Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As ODBUSEGRP) As Short

    ' read tool life management data(tool group information 2) 
    Declare Function cnc_rdtlgrp Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Integer, ByRef b As Short, ByRef c As IODBTLGRP) As Short

    ' read tool life management data (tool data1) 
    Declare Function cnc_rdtltool Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, ByRef c As Short, ByRef d As IODBTLTOOL) As Short

    Declare Function cnc_rdexchgtgrp Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef b As Short, ByRef c As ODBEXGP) As Short

    '-----------------------------------
    ' CNC: Tool management data related 
    '-----------------------------------

    ' new registration of tool management data 
    Declare Function cnc_regtool Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As Short, ByRef c As IODBTLMNG) As Short

    ' new registration of tool management data 
    Declare Function cnc_regtool_f2 Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As Short, ByRef c As IODBTLMNG_F2) As Short

    ' deletion of tool management data 
    Declare Function cnc_deltool Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As Short) As Short

    ' lead of tool management data 
    Declare Function cnc_rdtool Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As Short, ByRef c As IODBTLMNG) As Short

    ' lead of tool management data 
    Declare Function cnc_rdtool_f2 Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As Short, ByRef c As IODBTLMNG_F2) As Short

    ' write of tool management data 
    Declare Function cnc_wrtool Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As IODBTLMNG) As Short

    ' write of individual data of tool management data 
    Declare Function cnc_wrtool2 Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As IDBTLM) As Short

    ' write tool management data
    Declare Function cnc_wrtool_f2 Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As IODBTLMNG_F2_data) As Short

    ' new registration of magazine management data 
    Declare Function cnc_regmagazine Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As Short, ByRef b As IODBTLMAG) As Short

    ' deletion of magazine management data 
    Declare Function cnc_delmagazine Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As Short, ByRef b As IODBTLMAG2) As Short

    ' lead of magazine management data 
    Declare Function cnc_rdmagazine Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As Short, ByRef b As IODBTLMAG) As Short

    ' Individual write of magazine management data 
    Declare Function cnc_wrmagazine Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Short, ByVal c As Short) As Short


    '-------------------------------------
    ' CNC: Operation history data related 
    '-------------------------------------

    ' stop logging operation history data 
    Declare Function cnc_stopophis Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer) As Short

    ' restart logging operation history data 
    Declare Function cnc_startophis Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer) As Short

    ' read number of operation history data 
    Declare Function cnc_rdophisno Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As Integer) As Short

    ' read operation history data 
    Declare Function cnc_rdophistry Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, ByVal c As Integer, ByRef d As ODBHIS) As Short

    ' read operation history data 
    Declare Function cnc_rdophistry2 Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, _
        ByVal a As Integer, ByRef b As Integer, ByRef c As Integer, <[In](), Out()> ByVal d() As ODBOPHIS) As Short

    ' read operation history data F30i
    Declare Function cnc_rdophistry4 Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, _
        ByVal a As Integer, ByRef b As Integer, ByRef c As Integer, ByRef d As ODBOPHIS4_1) As Short

    Declare Function cnc_rdophistry4 Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, _
        ByVal a As Integer, ByRef b As Integer, ByRef c As Integer, ByRef d As ODBOPHIS4_2) As Short

    Declare Function cnc_rdophistry4 Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, _
        ByVal a As Integer, ByRef b As Integer, ByRef c As Integer, ByRef d As ODBOPHIS4_3) As Short

    Declare Function cnc_rdophistry4 Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, _
        ByVal a As Integer, ByRef b As Integer, ByRef c As Integer, ByRef d As ODBOPHIS4_4) As Short

    Declare Function cnc_rdophistry4 Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, _
        ByVal a As Integer, ByRef b As Integer, ByRef c As Integer, ByRef d As ODBOPHIS4_5) As Short

    Declare Function cnc_rdophistry4 Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, _
        ByVal a As Integer, ByRef b As Integer, ByRef c As Integer, ByRef d As ODBOPHIS4_6) As Short

    Declare Function cnc_rdophistry4 Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, _
        ByVal a As Integer, ByRef b As Integer, ByRef c As Integer, ByRef d As ODBOPHIS4_7) As Short

    Declare Function cnc_rdophistry4 Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, _
        ByVal a As Integer, ByRef b As Integer, ByRef c As Integer, ByRef d As ODBOPHIS4_8) As Short

    Declare Function cnc_rdophistry4 Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, _
        ByVal a As Integer, ByRef b As Integer, ByRef c As Integer, ByRef d As ODBOPHIS4_9) As Short

    Declare Function cnc_rdophistry4 Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, _
        ByVal a As Integer, ByRef b As Integer, ByRef c As Integer, ByRef d As ODBOPHIS4_10) As Short

    Declare Function cnc_rdophistry4 Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, _
        ByVal a As Integer, ByRef b As Integer, ByRef c As Integer, ByRef d As ODBOPHIS4_11) As Short

    ' read number of alarm history data 
    Declare Function cnc_rdalmhisno Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As Integer) As Short

    ' read alarm history data 
    Declare Function cnc_rdalmhistry Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, ByVal c As Integer, ByRef d As ODBAHIS) As Short

    ' read alarm history data 
    Declare Function cnc_rdalmhistry_w Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Integer, ByVal c As Integer, ByRef d As ODBAHIS) As Short

    ' read alarm history data 
    Declare Function cnc_rdalmhistry2 Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, ByVal c As Integer, ByRef d As ODBAHIS2) As Short

    ' read alarm history data F30i
    Declare Function cnc_rdalmhistry3 Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, ByVal c As Integer, ByRef d As ODBAHIS3) As Short

    ' read alarm history data F30i
    Declare Function cnc_rdalmhistry5 Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, ByVal c As Integer, ByRef d As ODBAHIS5) As Short

    ' clear operation history data 
    Declare Function cnc_clearophis Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short) As Short

    ' read signals related operation history 
    Declare Function cnc_rdhissgnl Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As IODBSIG) As Short

    ' read signals related operation history 2
    Declare Function cnc_rdhissgnl2 Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As IODBSIG2) As Short

    ' read signals related operation history 3
    Declare Function cnc_rdhissgnl3 Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As IODBSIG3) As Short

    ' write signals related operation history 
    Declare Function cnc_wrhissgnl Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As IODBSIG) As Short

    ' write signals related operation history 2
    Declare Function cnc_wrhissgnl2 Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As IODBSIG2) As Short

    ' write signals related operation history for F30i
    Declare Function cnc_wrhissgnl3 Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As IODBSIG3) As Short

    ' read number of operater message history data
    Declare Function cnc_rdomhisno Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As Short) As Short

    '-------------
    ' CNC: Others 
    '-------------

    ' read CNC system information 
    Declare Function cnc_sysinfo Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As ODBSYS) As Short

    ' read CNC status information 
    Declare Function cnc_statinfo Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As ODBST) As Short

    ' read alarm status 
    Declare Function cnc_alarm Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As ODBALM) As Short

    ' read alarm status 
    Declare Function cnc_alarm2 Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As Integer) As Short

    ' read alarm information 
    Declare Function cnc_rdalminfo Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Short, ByVal c As Short, ByRef d As ALMINFO_1) As Short

    Declare Function cnc_rdalminfo Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Short, ByVal c As Short, ByRef d As ALMINFO_2) As Short

    ' read alarm message 
    Declare Function cnc_rdalmmsg Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As Short, ByRef c As ODBALMMSG) As Short

    ' read alarm message (2)
    Declare Function cnc_rdalmmsg2 Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As Short, ByRef c As ODBALMMSG2) As Short

    ' clear CNC alarm 
    Declare Function cnc_clralm Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short) As Short

    ' read modal data 
    Declare Function cnc_modal Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Short, ByRef c As ODBMDL_1) As Short
    Declare Function cnc_modal Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Short, ByRef c As ODBMDL_2) As Short
    Declare Function cnc_modal Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Short, ByRef c As ODBMDL_3) As Short
    Declare Function cnc_modal Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Short, ByRef c As ODBMDL_4) As Short
    Declare Function cnc_modal Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Short, ByRef c As ODBMDL_5) As Short

    ' read G code 
    Declare Function cnc_rdgcode Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Short, ByRef c As Short, ByRef d As ODBGCD) As Short

    ' read command value 
    Declare Function cnc_rdcommand Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Short, ByRef c As Short, ByRef d As ODBCMD) As Short

    ' read diagnosis data 
    Declare Function cnc_diagnoss Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Short, ByVal c As Short, ByRef d As ODBDGN_1) As Short
    Declare Function cnc_diagnoss Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Short, ByVal c As Short, ByRef d As ODBDGN_2) As Short
    Declare Function cnc_diagnoss Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Short, ByVal c As Short, ByRef d As ODBDGN_3) As Short
    Declare Function cnc_diagnoss Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Short, ByVal c As Short, ByRef d As ODBDGN_4) As Short

    ' read diagnosis data(area specified) 
    Declare Function cnc_diagnosr Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, _
        ByRef a As Short, ByVal b As Short, ByRef c As Short, ByRef d As Short, ByRef e As ODBDGN_A) As Short
    Declare Function cnc_diagnosr Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, _
        ByRef a As Short, ByVal b As Short, ByRef c As Short, ByRef d As Short, ByRef e As ODBDGN_B) As Short
    Declare Function cnc_diagnosr Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, _
        ByRef a As Short, ByVal b As Short, ByRef c As Short, ByRef d As Short, ByRef e As ODBDGN_C) As Short
    Declare Function cnc_diagnosr Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, _
        ByRef a As Short, ByVal b As Short, ByRef c As Short, ByRef d As Short, ByRef e As ODBDGN_D) As Short

    ' read A/D conversion data 
    Declare Function cnc_adcnv Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Short, ByRef c As ODBAD) As Short

    ' read operator's message 
    Declare Function cnc_rdopmsg Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Short, ByRef c As OPMSG) As Short

    ' read operator's message 
    Declare Function cnc_rdopmsg2 Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Short, ByRef c As OPMSG2) As Short

    ' read operator's message 
    Declare Function cnc_rdopmsg3 Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As Short, ByRef c As OPMSG3) As Short

    ' set path number(for 4 axes lathes, multi-path) 
    Declare Function cnc_setpath Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short) As Short

    ' get path number(for 4 axes lathes, multi-path) 
    Declare Function cnc_getpath Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As Short, ByRef b As Short) As Short

    ' allocate library handle 
    Declare Function cnc_allclibhndl Lib "FWLIB32.DLL" _
        (ByRef FlibHndl As Integer) As Short

    ' free library handle 
    Declare Function cnc_freelibhndl Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer) As Short

    ' get library option
    Declare Function cnc_getlibopt Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Integer, <[In](), Out()> ByVal b() As Char, ByRef c As Integer) As Short

    ' set library option
    Declare Function cnc_setlibopt Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b() As Char, ByVal c As Integer) As Short

    ' get custom macro type 
    Declare Function cnc_getmactype Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As Short) As Short

    ' set custom macro type 
    Declare Function cnc_setmactype Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short) As Short

    ' get P code macro type 
    Declare Function cnc_getpmactype Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As Short) As Short

    ' set P code macro type 
    Declare Function cnc_setpmactype Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short) As Short

    ' get screen status 
    Declare Function cnc_getcrntscrn Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As Short) As Short

    ' change screen mode 
    Declare Function cnc_slctscrn Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short) As Short

    ' read CNC configuration information 
    Declare Function cnc_sysconfig Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As ODBSYSC) As Short

    ' read program restart information 
    Declare Function cnc_rdprstrinfo Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As ODBPRS) As Short

    ' search sequence number for program restart 
    Declare Function cnc_rstrseqsrch Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, ByVal c As Short, ByVal d As Short) As Short

    ' search sequence number for program restart 2 
    Declare Function cnc_rstrseqsrch2 Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, _
        ByVal a As Integer, ByVal b As Integer, ByVal c As Short, ByVal d As Short, ByVal e As Integer) As Short

    ' read output signal image of software operator's panel  
    Declare Function cnc_rdopnlsgnl Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As IODBSGNL) As Short

    ' write output signal of software operator's panel  
    Declare Function cnc_wropnlsgnl Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As IODBSGNL) As Short

    ' read general signal image of software operator's panel  
    Declare Function cnc_rdopnlgnrl Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As IODBGNRL) As Short

    ' write general signal image of software operator's panel  
    Declare Function cnc_wropnlgnrl Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As IODBGNRL) As Short

    ' read general signal name of software operator's panel  
    Declare Function cnc_rdopnlgsname Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As IODBRDNA) As Short

    ' write general signal name of software operator's panel  
    Declare Function cnc_wropnlgsname Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As IODBRDNA) As Short

    ' get detail error 
    Declare Function cnc_getdtailerr Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As ODBERR) As Short

    ' read informations of CNC parameter 
    Declare Function cnc_rdparainfo Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Integer, ByRef c As ODBPARAIF) As Short

    ' read informations of CNC setting data 
    Declare Function cnc_rdsetinfo Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Integer, ByRef c As ODBSETIF) As Short

    ' read informations of CNC diagnose data 
    Declare Function cnc_rddiaginfo Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Integer, ByRef c As ODBDIAGIF) As Short

    ' read maximum, minimum and total number of CNC parameter 
    Declare Function cnc_rdparanum Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As ODBPARANUM) As Short

    ' read maximum, minimum and total number of CNC setting data 
    Declare Function cnc_rdsetnum Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As ODBSETNUM) As Short

    ' read maximum, minimum and total number of CNC diagnose data 
    Declare Function cnc_rddiagnum Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As ODBDIAGNUM) As Short

    ' get maximum valid figures and number of decimal places 
    Declare Function cnc_getfigure Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, _
        ByVal a As Short, ByRef b As Short, <[In](), Out()> ByVal c() As Short, <[In](), Out()> ByVal d() As Short) As Short

    ' read F-ROM information on CNC  
    Declare Function cnc_rdfrominfo Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As ODBFINFO) As Short

    ' start of reading F-ROM data from CNC 
    Declare Function cnc_fromsvstart Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As String, ByVal c As Integer) As Short

    ' read F-ROM data from CNC 
    Declare Function cnc_fromsave Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As Short, <[In](), Out()> ByVal b() As Byte, ByRef c As Integer) As Short

    ' end of reading F-ROM data from CNC 
    Declare Function cnc_fromsvend Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer) As Short

    ' start of writing F-ROM data to CNC 
    Declare Function cnc_fromldstart Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Integer) As Short

    ' write F-ROM data to CNC 
    Declare Function cnc_fromload Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal b() As Byte, ByRef b As Integer) As Short

    ' end of writing F-ROM data to CNC 
    Declare Function cnc_fromldend Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer) As Short

    ' delete F-ROM data on CNC 
    Declare Function cnc_fromdelete Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As String, ByVal c As Integer) As Short

    ' read S-RAM information on CNC 
    Declare Function cnc_rdsraminfo Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As ODBSINFO) As Short

    ' start of reading S-RAM data from CNC 
    Declare Function cnc_srambkstart Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As String, ByVal b As Integer) As Short

    ' read S-RAM data from CNC 
    Declare Function cnc_srambackup Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As Short, <[In](), Out()> ByVal b() As Byte, ByRef c As Integer) As Short

    ' end of reading S-RAM data from CNC 
    Declare Function cnc_srambkend Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer) As Short

    ' read F-ROM information on CNC  
    Declare Function cnc_getfrominfo Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As Short, ByRef c As ODBFINFORM) As Short

    ' start of reading F-ROM data from CNC 
    Declare Function cnc_fromgetstart Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As String) As Short

    ' read F-ROM data from CNC 
    Declare Function cnc_fromget Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As Short, <[In](), Out()> ByVal b() As Byte, ByRef c As Integer) As Short

    ' end of reading F-ROM data from CNC 
    Declare Function cnc_fromgetend Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer) As Short

    ' start of writing F-ROM data to CNC 
    Declare Function cnc_fromputstart Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short) As Short

    ' write F-ROM data to CNC 
    Declare Function cnc_fromput Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a() As Byte, ByRef b As Integer) As Short

    ' end of writing F-ROM data to CNC 
    Declare Function cnc_fromputend Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer) As Short

    ' delete F-ROM data on CNC 
    Declare Function cnc_fromremove Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As String) As Short

    ' read S-RAM information on CNC 
    Declare Function cnc_getsraminfo Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As ODBSINFO) As Short

    ' start of reading S-RAM data from CNC 
    Declare Function cnc_sramgetstart Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As String) As Short

    ' start of reading S-RAM data from CNC (2) 
    Declare Function cnc_sramgetstart2 Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As String) As Short

    ' read S-RAM data from CNC 
    Declare Function cnc_sramget Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As Short, <[In](), Out()> ByVal b() As Byte, ByRef c As Integer) As Short

    ' read S-RAM data from CNC (2) 
    Declare Function cnc_sramget2 Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As Short, <[In](), Out()> ByVal b() As Byte, ByRef c As Integer) As Short

    ' end of reading S-RAM data from CNC 
    Declare Function cnc_sramgetend Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer) As Short

    ' end of reading S-RAM data from CNC (2) 
    Declare Function cnc_sramgetend2 Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer) As Short

    ' read number of S-RAM data kind on CNC 
    Declare Function cnc_rdsramnum Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As Short) As Short

    ' read S-RAM data address information on CNC 
    Declare Function cnc_rdsramaddr Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As Short, ByRef b As SRAMADDR) As Short

    ' get current NC data protection information 
    Declare Function cnc_getlockstat Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, <[In](), Out()> ByVal b() As Byte) As Short

    ' change NC data protection status 
    Declare Function cnc_chgprotbit Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, <[In](), Out()> ByVal b() As Byte, ByVal c As Integer) As Short

    ' transfer a file from host computer to CNC by FTP 
    Declare Function cnc_dtsvftpget Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As String, ByVal b As String) As Short

    ' transfer a file from CNC to host computer by FTP 
    Declare Function cnc_dtsvftpput Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As String, ByVal b As String) As Short

    ' get transfer status for FTP 
    Declare Function cnc_dtsvftpstat Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer) As Short

    ' read file directory in Data Server 
    Declare Function cnc_dtsvrdpgdir Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As String, ByVal b As Short, ByRef c As ODBDSDIR) As Short

    ' delete files in Data Server 
    Declare Function cnc_dtsvdelete Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As String) As Short

    ' down load from CNC (transfer a file from CNC to MMC) 
    Declare Function cnc_dtsvdownload Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As String) As Short

    ' up load to CNC (transfer a file from MMC to CNC) 
    Declare Function cnc_dtsvupload Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As String) As Short

    ' close upload/download between Data Server and CNC 
    Declare Function cnc_dtsvcnclupdn Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer) As Short

    ' get transfer status for up/down load 
    Declare Function cnc_dtsvupdnstat Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer) As Short

    ' get file name for DNC operation in Data Server 
    Declare Function cnc_dtsvgetdncpg Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, <[In](), Out()> ByVal a() As Char) As Short

    ' set program number of DNC oparation to CNC 
    Declare Function cnc_dtsvsetdncpg Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As String) As Short

    ' read setting data for Data Server 
    Declare Function cnc_dtsvrdset Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As IODBDSSET) As Short

    ' write setting data for Data Server 
    Declare Function cnc_dtsvwrset Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As IODBDSSET) As Short

    ' check hard disk in Data Server 
    Declare Function cnc_dtsvchkdsk Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer) As Short

    ' format hard disk in Data Server 
    Declare Function cnc_dtsvhdformat Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer) As Short

    ' save interface area in Data Server 
    Declare Function cnc_dtsvsavecram Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer) As Short

    ' get interface area in Data Server 
    Declare Function cnc_dtsvrdcram Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Integer, ByRef b As Integer, <[In](), Out()> ByVal c() As Byte) As Short

    ' read maintenance information for Data Server 
    Declare Function cnc_dtsvmntinfo Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As ODBDSMNT) As Short

    ' get Data Server mode 
    Declare Function cnc_dtsvgetmode Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As Short) As Short

    ' set Data Server mode 
    Declare Function cnc_dtsvsetmode Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short) As Short

    ' read error message for Data Server 
    Declare Function cnc_dtsvrderrmsg Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, <[In](), Out()> ByVal b() As Char) As Short

    ' transfar file from Pc to Data Server 
    Declare Function cnc_dtsvwrfile Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As String, ByVal b As String, ByVal c As Short) As Short

    ' transfar file from Data Server to Pc 
    Declare Function cnc_dtsvrdfile Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As String, ByVal b As String, ByVal c As Short) As Short

    ' read the loop gain for each axis 
    Declare Function cnc_rdloopgain Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As Integer) As Short

    ' read the actual current for each axis 
    Declare Function cnc_rdcurrent Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As Short) As Short

    ' read the actual speed for each axis 
    Declare Function cnc_rdsrvspeed Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As Integer) As Short

    ' read the operation mode 
    Declare Function cnc_rdopmode Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As Short) As Short

    ' read the position deviation S 
    Declare Function cnc_rdposerrs Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As Integer) As Short

    ' read the position deviation S1 and S2 
    Declare Function cnc_rdposerrs2 Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As ODBPSER) As Short

    ' read the position deviation Z in the rigid tap mode 
    Declare Function cnc_rdposerrz Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As Integer) As Short

    ' read the synchronous error in the synchronous control mode 
    Declare Function cnc_rdsynerrsy Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As Integer) As Short

    ' read the synchronous error in the rigid tap mode 
    Declare Function cnc_rdsynerrrg Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As Integer) As Short

    ' read the spindle alarm 
    Declare Function cnc_rdspdlalm Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, <[In](), Out()> ByVal a() As Byte) As Short

    ' read the control input signal 
    Declare Function cnc_rdctrldi Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As ODBSPDI) As Short

    ' read the control output signal 
    Declare Function cnc_rdctrldo Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As ODBSPDO) As Short

    ' read the number of controled spindle 
    Declare Function cnc_rdnspdl Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As Short) As Short

    ' read data from FANUC BUS 
    Declare Function cnc_rdfbusmem Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, _
        ByVal a As Short, ByVal b As Short, ByVal c As Integer, ByVal d As Integer, <[In](), Out()> ByVal e() As Byte) As Short

    ' write data to FANUC BUS 
    Declare Function cnc_wrfbusmem Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, _
        ByVal a As Short, ByVal b As Short, ByVal c As Integer, ByVal d As Integer, ByVal e() As Byte) As Short

    ' read the parameter of wave diagnosis 
    Declare Function cnc_rdwaveprm Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As IODBWAVE) As Short

    ' write the parameter of wave diagnosis 
    Declare Function cnc_wrwaveprm Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As IODBWAVE) As Short

    ' read the parameter of wave diagnosis 2 
    Declare Function cnc_rdwaveprm2 Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As IODBWVPRM) As Short

    ' write the parameter of wave diagnosis 2 
    Declare Function cnc_wrwaveprm2 Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As IODBWVPRM) As Short

    ' start the sampling for wave diagnosis 
    Declare Function cnc_wavestart Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer) As Short

    ' stop the sampling for wave diagnosis 
    Declare Function cnc_wavestop Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer) As Short

    ' read the status of wave diagnosis 
    Declare Function cnc_wavestat Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As Short) As Short

    ' read the data of wave diagnosis 
    Declare Function cnc_rdwavedata Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, _
        ByVal a As Short, ByVal b As Short, ByVal c As Integer, ByRef d As Integer, ByRef e As ODBWVDT) As Short

    ' read the parameter of wave diagnosis for remort diagnosis 
    Declare Function cnc_rdrmtwaveprm Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As IODBRMTPRM, ByVal b As Short) As Short

    ' write the parameter of wave diagnosis for remort diagnosis 
    Declare Function cnc_wrrmtwaveprm Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As IODBRMTPRM) As Short

    ' start the sampling for wave diagnosis for remort diagnosis 
    Declare Function cnc_rmtwavestart Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer) As Short

    ' stop the sampling for wave diagnosis for remort diagnosis 
    Declare Function cnc_rmtwavestop Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer) As Short

    ' read the status of wave diagnosis for remort diagnosis
    Declare Function cnc_rmtwavestat Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As Short) As Short

    ' read the data of wave diagnosis for remort diagnosis 
    Declare Function cnc_rdrmtwavedt Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Integer, ByRef c As Integer, ByRef d As ODBRMTDT) As Short

    ' read of address for PMC signal batch save 
    Declare Function cnc_rdsavsigadr Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As IODBSIGAD, ByVal b As Short) As Short

    ' write of address for PMC signal batch save 
    Declare Function cnc_wrsavsigadr Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As IODBSIGAD, ByRef b As Short) As Short

    ' read of data for PMC signal batch save 
    Declare Function cnc_rdsavsigdata Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, _
        ByVal a As Short, ByVal b As Short, <[In](), Out()> ByVal c() As Byte, ByRef d As Short) As Short

    ' read M-code group data 
    Declare Function cnc_rdmgrpdata Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As Short, ByRef c As ODBMGRP) As Short

    ' write M-code group data 
    Declare Function cnc_wrmgrpdata Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As IDBMGRP) As Short

    ' read executing M-code group data 
    Declare Function cnc_rdexecmcode Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As Short, ByRef c As ODBEXEM) As Short

    ' read program restart M-code group data 
    Declare Function cnc_rdrstrmcode Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As Short, ByRef c As ODBRSTRM) As Short

    ' read processing time stamp data 
    Declare Function cnc_rdproctime Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As ODBPTIME) As Short

    ' read MDI program stat
    Declare Function cnc_rdmdiprgstat Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As Short) As Short

    ' read program directory for processing time data 
    Declare Function cnc_rdprgdirtime Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As Integer, ByRef b As Short, ByRef c As PRGDIRTM) As Short

    ' read program directory 2 
#If ONO8D = Nothing Then
    Declare Function cnc_rdprogdir2 Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As Short, ByRef c As Short, ByRef d As PRGDIR2 ) As Short
#Else
    Declare Function cnc_rdprogdir2 Lib "FWLIB32.DLL" Alias "cnc_rdprogdir2o8" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As Short, ByRef c As Short, ByRef d As PRGDIR2 ) As Short
#End If

    ' read program directory 3 
    Declare Function cnc_rdprogdir3 Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As Integer, ByRef c As Short, ByRef d As PRGDIR3 ) As Short

    ' read program directory 4 
    Declare Function cnc_rdprogdir4 Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Integer, ByRef c As Short, ByRef d As PRGDIR4 ) As Short

    ' read DNC file name for DNC1, DNC2, OSI-Ethernet 
    Declare Function cnc_rddncfname Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, <[In], Out> ByVal a() As Char) As Short

    ' write DNC file name for DNC1, DNC2, OSI-Ethernet 
    Declare Function cnc_wrdncfname Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As String) As Short

    ' read communication parameter for DNC1, DNC2, OSI-Ethernet 
    Declare Function cnc_rdcomparam Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As IODBCPRM ) As Short

    ' write communication parameter for DNC1, DNC2, OSI-Ethernet 
    Declare Function cnc_wrcomparam Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As IODBCPRM ) As Short

    ' read log message for DNC2 
    Declare Function cnc_rdcomlogmsg Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, <[In], Out> ByVal a() As Char) As Short

    ' read operator message for DNC1, DNC2 
    Declare Function cnc_rdcomopemsg Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, <[In], Out> ByVal a() As Char) As Short

    ' read recieve message for OSI-Ethernet 
    Declare Function cnc_rdrcvmsg Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, <[In], Out> ByVal a() As Char) As Short

    ' read send message for OSI-Ethernet 
    Declare Function cnc_rdsndmsg Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, <[In], Out> ByVal a() As Char) As Short

    ' send message for OSI-Ethernet 
    Declare Function cnc_sendmessage Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As String) As Short

    ' clear message buffer for OSI-Ethernet 
    Declare Function cnc_clrmsgbuff Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short ) As Short

    ' read message recieve status for OSI-Ethernet 
    Declare Function cnc_rdrcvstat Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As Short) As Short

    ' read interference check 
    Declare Function cnc_rdintchk Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Short, ByVal c As Short, ByVal d As Short, ByRef e As IODBINT ) As Short

    ' write interference check 
    Declare Function cnc_wrintchk Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As IODBINT ) As Short

    ' read interference check information 
    Declare Function cnc_rdintinfo Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As Short ) As Short

    ' read work coordinate shift 
    Declare Function cnc_rdwkcdshft Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Short, ByRef c As IODBWCSF ) As Short

    ' write work coordinate shift 
    Declare Function cnc_wrwkcdshft Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As IODBWCSF ) As Short

    ' read work coordinate shift measure 
    Declare Function cnc_rdwkcdsfms Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Short, ByRef c As IODBWCSF ) As Short

    ' write work coordinate shift measure 
    Declare Function cnc_wrwkcdsfms Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As IODBWCSF ) As Short

    ' stop the sampling for operator message history 
    Declare Function cnc_stopomhis Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer ) As Short

    ' start the sampling for operator message history 
    Declare Function cnc_startomhis Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer ) As Short

    ' read operator message history information 
    Declare Function cnc_rdomhisinfo Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As ODBOMIF ) As Short

    ' read operator message history 
    Declare Function cnc_rdomhistry Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As Integer, ByRef c As ODBOMHIS) As Short

    ' read operater message history data F30i
    Declare Function cnc_rdomhistry2 Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Short, ByVal c As Short, ByRef d As ODBOMHIS2 ) As Short

    ' write external key operation history for F30i
    Declare Function cnc_wrkeyhistry Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Byte ) As Short

    ' clear operator message history 
    Declare Function cnc_clearomhis Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer ) As Short

    ' read b-axis tool offset value(area specified) 
    Declare Function cnc_rdbtofsr Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Short, ByVal c As Short, ByVal d As Short, ByRef e As IODBBTO ) As Short

    ' write b-axis tool offset value(area specified) 
    Declare Function cnc_wrbtofsr Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As IODBBTO ) As Short

    ' read b-axis tool offset information 
    Declare Function cnc_rdbtofsinfo Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As ODBBTLINF ) As Short

    ' read b-axis command 
    Declare Function cnc_rdbaxis Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As ODBBAXIS ) As Short

    ' read CNC system soft series and version 
    Declare Function cnc_rdsyssoft Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As ODBSYSS ) As Short

    ' read CNC system soft series and version (2) 
    Declare Function cnc_rdsyssoft2 Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As ODBSYSS2 ) As Short

    ' read CNC module configuration information 
    Declare Function cnc_rdmdlconfig Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As ODBMDLC ) As Short

    ' read CNC module configuration information 2 
    Declare Function cnc_rdmdlconfig2 Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, <[In], Out> ByVal a() As SByte) As Short

    ' read processing condition file (processing data) 
    Declare Function cnc_rdpscdproc Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As Short, ByRef c As IODBPSCD ) As Short

    ' write processing condition file (processing data) 
    Declare Function cnc_wrpscdproc Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As Short, ByRef c As IODBPSCD ) As Short

    ' read processing condition file (piercing data) 
    Declare Function cnc_rdpscdpirc Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As Short, ByRef c As IODBPIRC ) As Short

    ' write processing condition file (piercing data) 
    Declare Function cnc_wrpscdpirc Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As Short, ByRef c As IODBPIRC ) As Short

    ' read processing condition file (edging data) 
    Declare Function cnc_rdpscdedge Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As Short, ByRef c As IODBEDGE ) As Short

    ' write processing condition file (edging data) 
    Declare Function cnc_wrpscdedge Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As Short, ByRef c As IODBEDGE ) As Short

    ' read processing condition file (slope data) 
    Declare Function cnc_rdpscdslop Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As Short, ByRef c As IODBSLOP ) As Short

    ' write processing condition file (slope data) 
    Declare Function cnc_wrpscdslop Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As Short, ByRef c As IODBSLOP ) As Short

    ' read power controll duty data 
    Declare Function cnc_rdlpwrdty Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As IODBLPWDT ) As Short

    ' write power controll duty data 
    Declare Function cnc_wrlpwrdty Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As IODBLPWDT ) As Short

    ' read laser power data 
    Declare Function cnc_rdlpwrdat Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As ODBLOPDT ) As Short

    ' read power complement 
    Declare Function cnc_rdlpwrcpst Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As Short ) As Short

    ' write power complement 
    Declare Function cnc_wrlpwrcpst Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short ) As Short

    ' read laser assist gas selection 
    Declare Function cnc_rdlagslt Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As IODBLAGSL ) As Short

    ' write laser assist gas selection 
    Declare Function cnc_wrlagslt Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As IODBLAGSL ) As Short

    ' read laser assist gas flow 
    Declare Function cnc_rdlagst Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As IODBLAGST ) As Short

    ' write laser assist gas flow 
    Declare Function cnc_wrlagst Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As IODBLAGST ) As Short

    ' read laser power for edge processing 
    Declare Function cnc_rdledgprc Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As IODBLEGPR ) As Short

    ' write laser power for edge processing 
    Declare Function cnc_wrledgprc Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As IODBLEGPR ) As Short

    ' read laser power for piercing 
    Declare Function cnc_rdlprcprc Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As IODBLPCPR ) As Short

    ' write laser power for piercing 
    Declare Function cnc_wrlprcprc Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As IODBLPCPR ) As Short

    ' read laser command data 
    Declare Function cnc_rdlcmddat Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As ODBLCMDT ) As Short

    ' read displacement 
    Declare Function cnc_rdldsplc Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As Short ) As Short

    ' write displacement 
    Declare Function cnc_wrldsplc Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short ) As Short

    ' read error for axis z 
    Declare Function cnc_rdlerrz Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As Short ) As Short

    ' read active number 
    Declare Function cnc_rdlactnum Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As ODBLACTN ) As Short

    ' read laser comment 
    Declare Function cnc_rdlcmmt Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As ODBLCMMT ) As Short

    ' read laser power select 
    Declare Function cnc_rdlpwrslt Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As Short ) As Short

    ' write laser power select 
    Declare Function cnc_wrlpwrslt Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short ) As Short

    ' read laser power controll 
    Declare Function cnc_rdlpwrctrl Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As Short ) As Short

    ' write laser power controll 
    Declare Function cnc_wrlpwrctrl Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short ) As Short

    ' read power correction factor history data 
    Declare Function cnc_rdpwofsthis Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Integer, ByRef b As Integer, ByRef c As ODBPWOFST ) As Short

    ' read management time 
    Declare Function cnc_rdmngtime Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Integer, ByRef b As Integer, ByRef c As IODBMNGTIME ) As Short

    ' write management time 
    Declare Function cnc_wrmngtime Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Integer, ByRef b As IODBMNGTIME ) As Short

    ' read data related to electrical discharge at power correction ends 
    Declare Function cnc_rddischarge Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As ODBDISCHRG ) As Short

    ' read alarm history data related to electrical discharg 
    Declare Function cnc_rddischrgalm Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Integer, ByRef b As Integer, ByRef c As ODBDISCHRGALM ) As Short

    ' get date and time from cnc 
    Declare Function cnc_gettimer Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As IODBTIMER ) As Short

    ' set date and time for cnc 
    Declare Function cnc_settimer Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As IODBTIMER ) As Short

    ' read timer data from cnc 
    Declare Function cnc_rdtimer Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As IODBTIME ) As Short

    ' write timer data for cnc 
    Declare Function cnc_wrtimer Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As IODBTIME ) As Short

    ' read tool controll data 
    Declare Function cnc_rdtlctldata Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As IODBTLCTL ) As Short

    ' write tool controll data 
    Declare Function cnc_wrtlctldata Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As IODBTLCTL ) As Short

    ' read tool data 
    Declare Function cnc_rdtooldata Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As Short, ByRef c As IODBTLDT ) As Short

    ' read tool data 
    Declare Function cnc_wrtooldata Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As Short, ByRef c As IODBTLDT ) As Short

    ' read multi tool data 
    Declare Function cnc_rdmultitldt Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As Short, ByRef c As IODBMLTTL ) As Short

    ' write multi tool data 
    Declare Function cnc_wrmultitldt Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As Short, ByRef c As IODBMLTTL ) As Short

    ' read multi tap data 
    Declare Function cnc_rdmtapdata Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As Short, ByRef c As IODBMTAP ) As Short

    ' write multi tap data 
    Declare Function cnc_wrmtapdata Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As Short, ByRef c As IODBMTAP ) As Short

    ' read multi-piece machining number 
    Declare Function cnc_rdmultipieceno Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As Integer ) As Short

    ' read tool information 
    Declare Function cnc_rdtoolinfo Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As ODBPTLINF ) As Short

    ' read safetyzone data 
    Declare Function cnc_rdsafetyzone Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As Short, ByRef c As IODBSAFE ) As Short

    ' write safetyzone data 
    Declare Function cnc_wrsafetyzone Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As Short, ByRef c As IODBSAFE ) As Short

    ' read toolzone data 
    Declare Function cnc_rdtoolzone Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As Short, ByRef c As IODBTLZN ) As Short

    ' write toolzone data 
    Declare Function cnc_wrtoolzone Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As Short, ByRef c As IODBTLZN ) As Short

    ' read active toolzone data 
    Declare Function cnc_rdacttlzone Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As ODBACTTLZN) As Short

    ' read setzone number 
    Declare Function cnc_rdsetzone Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As Short ) As Short

    ' write setzone number 
    Declare Function cnc_wrsetzone Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short ) As Short

    ' read block restart information 
    Declare Function cnc_rdbrstrinfo Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As ODBBRS ) As Short

    ' read menu switch signal 
    Declare Function cnc_rdmenuswitch Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As Short ) As Short

    ' write menu switch signal 
    Declare Function cnc_wrmenuswitch Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Short ) As Short

    ' read tool radius offset for position data 
    Declare Function cnc_rdradofs Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As ODBROFS ) As Short

    ' read tool length offset for position data 
    Declare Function cnc_rdlenofs Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As ODBLOFS ) As Short

    ' read fixed cycle for position data 
    Declare Function cnc_rdfixcycle Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As ODBFIX ) As Short

    ' read coordinate rotate for position data 
    Declare Function cnc_rdcdrotate Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As ODBROT ) As Short

    ' read 3D coordinate convert for position data 
    Declare Function cnc_rd3dcdcnv Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As ODB3DCD ) As Short

    ' read programable mirror image for position data 
    Declare Function cnc_rdmirimage Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As ODBMIR ) As Short

    ' read scaling for position data 
    Declare Function cnc_rdscaling Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As ODBSCL ) As Short

    ' read 3D tool offset for position data 
    Declare Function cnc_rd3dtofs Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As ODB3DTO ) As Short

    ' read tool position offset for position data 
    Declare Function cnc_rdposofs Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As ODBPOFS ) As Short

    ' read hpcc setting data 
    Declare Function cnc_rdhpccset Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As IODBHPST ) As Short

    ' write hpcc setting data 
    Declare Function cnc_wrhpccset Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As IODBHPST ) As Short

    ' hpcc data auto setting data 
    Declare Function cnc_hpccatset Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer ) As Short

    ' read hpcc tuning data ( parameter input ) 
    Declare Function cnc_rdhpcctupr Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As IODBHPPR ) As Short

    ' write hpcc tuning data ( parameter input ) 
    Declare Function cnc_wrhpcctupr Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As IODBHPPR ) As Short

    ' read hpcc tuning data ( acc input ) 
    Declare Function cnc_rdhpcctuac Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As IODBHPAC ) As Short

    ' write hpcc tuning data ( acc input ) 
    Declare Function cnc_wrhpcctuac Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As IODBHPAC ) As Short

    ' hpcc data auto tuning 
    Declare Function cnc_hpccattune Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As Short ) As Short

    ' read hpcc fine level 
    Declare Function cnc_hpccactfine Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As Short ) As Short

    ' select hpcc fine level 
    Declare Function cnc_hpccselfine Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short ) As Short

    ' read active fixture offset 
    Declare Function cnc_rdactfixofs Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As IODBZOFS ) As Short

    ' read fixture offset 
    Declare Function cnc_rdfixofs Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Short, ByVal c As Short, ByVal d As Short, ByRef e As IODBZOR ) As Short

    ' write fixture offset 
    Declare Function cnc_wrfixofs Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As IODBZOR ) As Short

    ' read tip of tool for 3D handle 
    Declare Function cnc_rd3dtooltip Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As ODB3DHDL ) As Short

    ' read pulse for 3D handle 
    Declare Function cnc_rd3dpulse Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As ODB3DPLS ) As Short

    ' read move overrlap of tool for 3D handle 
    Declare Function cnc_rd3dmovrlap Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As ODB3DHDL ) As Short

    ' read change offset for 3D handle 
    Declare Function cnc_rd3dofschg Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As Integer ) As Short

    ' clear pulse and change offset for 3D handle 
    Declare Function cnc_clr3dplsmov Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short ) As Short

    ' cycle start 
    Declare Function cnc_start Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer ) As Short

    ' reset CNC 
    Declare Function cnc_reset Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer ) As Short

    ' reset CNC 
    Declare Function cnc_reset2 Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer ) As Short

    ' read axis name 
    Declare Function cnc_rdaxisname Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As Short, ByRef b As ODBAXISNAME ) As Short

    ' read spindle name 
    Declare Function cnc_rdspdlname Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As Short, ByRef b As ODBSPDLNAME ) As Short

    ' read extended axis name
    Declare Function cnc_exaxisname Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As Short, ByRef c As ODBEXAXISNAME ) As Short

    ' read SRAM variable area for C language executor 
    Declare Function cnc_rdcexesram Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Integer, <[In], Out> ByVal b() As Byte, ByRef c As Integer ) As Short

    ' write SRAM variable area for C language executor 
    Declare Function cnc_wrcexesram Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b() As Byte, ByRef c As Integer ) As Short

    ' read maximum size and linear address of SRAM variable area for C language executor 
    Declare Function cnc_cexesraminfo Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As Short, ByRef b As Integer, ByRef c As Integer ) As Short

    ' read maximum size of SRAM variable area for C language executor 
    Declare Function cnc_cexesramsize Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As Integer ) As Short

    ' read additional workpiece coordinate systems number 
    Declare Function cnc_rdcoordnum Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As Short ) As Short

    ' converts from FANUC code to Shift JIS code 
    Declare Function cnc_ftosjis Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a() As Byte, <[In], Out> ByVal b() As Char) As Short

    ' Set the unsolicited message parameters 
    Declare Function cnc_wrunsolicprm Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As IODBUNSOLIC ) As Short

    ' Get the unsolicited message parameters 
    Declare Function cnc_rdunsolicprm Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As IODBUNSOLIC ) As Short

    ' Start of unsolicited message 
    Declare Function cnc_unsolicstart Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, _
        ByVal a As Short, ByVal hWnd As Integer, ByVal c As Integer, ByVal d As Short, ByRef e As Short) As Short

    ' End of unsolicited message 
    Declare Function cnc_unsolicstop Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short ) As Short

    ' Reads the unsolicited message data 
    Declare Function cnc_rdunsolicmsg Lib "FWLIB32.DLL" _
        ( ByVal a As Short, ByRef b As IDBUNSOLICMSG ) As Short

    ' read cnc maintenance item
    Declare Function cnc_rdpm_mcnitem Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As Short , ByRef c As IODBITEM ) As Short

    ' write machine specific maintenance item
    Declare Function cnc_wrpm_mcnitem Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Short , ByRef c As IODBITEM ) As Short

    ' read machine specific maintenance item
    Declare Function cnc_rdpm_cncitem Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As Short , ByRef c As IODBITEM ) As Short

    ' read maintenance item status
    Declare Function cnc_rdpm_item Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As Short , ByRef c As IODBPMAINTE ) As Short

    ' write maintenance item status
    Declare Function cnc_wrpm_item Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Short, ByVal c As Short, ByRef d As IODBPMAINTE ) As Short

    ' Display of optional message 
    Declare Function cnc_dispoptmsg Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As String) As Short

    ' Reading of answer for optional message display 
    Declare Function cnc_optmsgans Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As Short ) As Short

    ' Get CNC Model 
    Declare Function cnc_getcncmodel Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As Short ) As Short

    ' read number of repeats
    Declare Function cnc_rdrepeatval Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As Integer ) As Short

    ' read CNC system hard info
    Declare Function cnc_rdsyshard Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As Short, ByRef c As ODBSYSH ) As Short

    ' read CNC system soft series and version (3)
    Declare Function cnc_rdsyssoft3 Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short , ByRef b As Short , ByRef c As Short , ByRef d As ODBSYSS3 ) As Short

    ' read digit of program number 
    Declare Function cnc_progdigit Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As Short ) As Short

    ' read CNC system path information
    Declare Function cnc_sysinfo_ex Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As ODBSYSEX ) As Short

'------------------
' CNC : SERCOS I/F 
'------------------

    ' Get reservation of service channel for SERCOS I/F 
    Declare Function cnc_srcsrsvchnl Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer ) As Short

    ' Read ID information of SERCOS I/F 
    Declare Function cnc_srcsrdidinfo Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, _
        ByVal a As Integer, ByVal b As Short, ByVal c As Short, ByRef d As IODBIDINF ) As Short

    ' Write ID information of SERCOS I/F 
    Declare Function cnc_srcswridinfo Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As IODBIDINF ) As Short

    ' Start of reading operation data from drive of SERCOS I/F 
    Declare Function cnc_srcsstartrd Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Short ) As Short

    ' Start of writing operation data to drive of SERCOS I/F 
    Declare Function cnc_srcsstartwrt Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Short ) As Short

    ' Stop of reading/writing operation data from/to drive of SERCOS I/F 
    Declare Function cnc_srcsstopexec Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer ) As Short

    ' Get execution status of reading/writing operation data of SERCOS I/F 
    Declare Function cnc_srcsrdexstat Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As ODBSRCSST ) As Short

    ' Read operation data from data buffer for SERCOS I/F 
    Declare Function cnc_srcsrdopdata Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Integer, ByRef b As Integer, <[In], Out> ByVal c() As Byte) As Short

    ' Write operation data to data buffer for SERCOS I/F 
    Declare Function cnc_srcswropdata Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, ByVal c() As Byte) As Short

    ' Free reservation of service channel for SERCOS I/F 
    Declare Function cnc_srcsfreechnl Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer ) As Short

    ' Read drive assign of SERCOS I/F 
    Declare Function cnc_srcsrdlayout Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As ODBSRCSLYT ) As Short

    ' Read communication phase of drive of SERCOS I/F 
    Declare Function cnc_srcsrddrvcp Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As Short ) As Short


'----------------------------
' CNC : Graphic command data 
'----------------------------

    ' Start drawing position 
    Declare Function cnc_startdrawpos Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer ) As Short

    ' Stop drawing position 
    Declare Function cnc_stopdrawpos Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer ) As Short

    ' Start dynamic graphic 
    Declare Function cnc_startdyngrph Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer ) As Short

    ' Stop dynamic graphic 
    Declare Function cnc_stopdyngrph Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer ) As Short

    ' Read graphic command data 
    Declare Function cnc_rdgrphcmd Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As Short, <[In], Out> ByVal b() As Short) As Short

    ' Update graphic command read pointer 
    Declare Function cnc_wrgrphcmdptr Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short ) As Short

    ' Read cancel flag 
    Declare Function cnc_rdgrphcanflg Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As Short ) As Short

    ' Clear graphic command 
    Declare Function cnc_clrgrphcmd Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer ) As Short


'---------------------------
' CNC : Servo learning data 
'---------------------------

    ' Servo learning data read start 
    Declare Function cnc_svdtstartrd Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short ) As Short

    ' Servo learning data write start 
    Declare Function cnc_svdtstartwr Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short ) As Short

    ' Servo learning data read end 
    Declare Function cnc_svdtendrd Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer ) As Short

    ' Servo learning data write end 
    Declare Function cnc_svdtendwr Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer ) As Short

    ' Servo learning data read/write stop 
    Declare Function cnc_svdtstopexec Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer ) As Short

    ' Servo learning data read from I/F buffer 
    Declare Function cnc_svdtrddata Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As Short, ByRef b As Integer, <[In], Out> ByVal c() As Byte) As Short

    ' Servo learning data write to I/F buffer 
    Declare Function cnc_svdtwrdata Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As Short, ByRef b As Integer, ByVal c() As Byte) As Short


'----------------------------
' CNC : Servo Guide          
'----------------------------
    ' Servo Guide (Channel data set) 
    Declare Function cnc_sdsetchnl Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As IDBCHAN ) As Short

    ' Servo Guide (Channel data clear) 
    Declare Function cnc_sdclrchnl Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer ) As Short

    ' Servo Guide (Sampling start) 
    Declare Function cnc_sdstartsmpl Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Integer, <[In], Out> ByVal c() As Short) As Short

    ' Servo Guide (Sampling cancel) 
    Declare Function cnc_sdcancelsmpl Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer ) As Short

    ' Servo Guide (read Sampling data) 
    Declare Function cnc_sdreadsmpl Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As Short, ByVal b As Integer, ByRef c As ODBSD ) As Short

    ' Servo Guide (Sampling end) 
    Declare Function cnc_sdendsmpl Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer ) As Short

    ' Servo Guide (read 1 shot data) 
    Declare Function cnc_sdread1shot Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, <[In], Out> ByVal c() As Short) As Short

    ' Servo feedback data (Channel data set) 
    Declare Function cnc_sfbsetchnl Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Integer, ByRef c As IDBSFBCHAN ) As Short

    ' Servo feedback data (Channel data clear) 
    Declare Function cnc_sfbclrchnl Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer ) As Short

    ' Servo feedback data (Sampling start) 
    Declare Function cnc_sfbstartsmpl Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Integer ) As Short

    ' Servo feedback data (Sampling cancel) 
    Declare Function cnc_sfbcancelsmpl Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer ) As Short

    ' Servo feedback data (read Sampling data) 
    Declare Function cnc_sfbreadsmpl Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As Short, ByVal b As Integer, ByRef c As ODBSD ) As Short

    ' Servo feedback data (Sampling end) 
    Declare Function cnc_sfbendsmpl Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer ) As Short

    ' Servo Guide (Channel data set) 
    Declare Function cnc_sdtsetchnl Lib "FWLIB32.DLL" _ 
    (ByVal FlibHndl As Integer, ByVal num As Short, ByVal datanum As Integer, <[In]()> channel As IDBSDTCHAN()) As Short 

    ' Servo Guide (Channel data clear) 
    Declare Function cnc_sdtclrchnl Lib "FWLIB32.DLL" _
    (ByVal FlibHndl As Integer) As Short

    ' Servo Guide (Sampling start) 
    Declare Function cnc_sdtstartsmpl Lib "FWLIB32.DLL" _
    (ByVal FlibHndl As Integer, ByVal path As Short, ByVal seq_no As Integer) As Short

    ' Servo Guide (Sampling cancel) 
    Declare Function cnc_sdtcancelsmpl Lib "FWLIB32.DLL" _
    (ByVal FlibHndl As Integer) As Short

    ' Servo Guide (read data) 
    Declare Function cnc_sdtreadsmpl Lib "FWLIB32.DLL" _ 
    (ByVal FlibHndl As Integer, ByRef stat As Short, ByVal datanum As Integer, <[In]()> sampleddata As ODBSD()) As Short 

    ' Servo Guide (Sampling end) 
    Declare Function cnc_sdtendsmpl Lib "FWLIB32.DLL" _
    (ByVal FlibHndl As Integer) As Short

    ' Servo Guide (read 1 shot data) 
    Declare Function cnc_sdtread1shot Lib "FWLIB32.DLL" _
    (ByVal FlibHndl As Integer, ByRef data As UShort()) As Short

'----------------------------
' CNC : NC display function  
'----------------------------

    ' Start NC display 
    Declare Function cnc_startnccmd Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer) As Short

    ' Start NC display (2) 
    Declare Function cnc_startnccmd2 Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As String) As Short

    ' Stop NC display 
    Declare Function cnc_stopnccmd Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer) As Short

    ' Get NC display mode 
    Declare Function cnc_getdspmode Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As Short) As Short


'------------------------------------
' CNC : Remote diagnostics function  
'------------------------------------

    ' Start remote diagnostics function 
    Declare Function cnc_startrmtdgn Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer ) As Short

    ' Stop remote diagnostics function 
    Declare Function cnc_stoprmtdgn Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer ) As Short

    ' Read data from remote diagnostics I/F 
    Declare Function cnc_rdrmtdgn Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As Integer, <[In], Out> ByVal b() As Byte) As Short

    ' Write data to remote diagnostics I/F 
    Declare Function cnc_wrrmtdgn Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As Integer, ByVal b() As Byte) As Short

    ' Set CommStatus of remote diagnostics I/F area 
    Declare Function cnc_wrcommstatus Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short ) As Short

    ' Check remote diagnostics I/F 
    Declare Function cnc_chkrmtdgn Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer ) As Short


'-------------------------
' CNC : FS18-LN function  
'-------------------------

    ' read allowance 
    Declare Function cnc_allowance Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Short, ByRef c As ODBAXIS) As Short

    ' read allowanced state 
    Declare Function cnc_allowcnd Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Short, ByRef c As ODBCAXIS ) As Short

    ' set work zero 
    Declare Function cnc_workzero Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As IODBZOFS ) As Short

    ' set slide position 
    Declare Function cnc_slide Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Short, ByRef c As ODBAXIS) As Short


'----------------------------------
' CNC: Teaching data I/F function  
'----------------------------------

    ' Teaching data get start 
    Declare Function cnc_startgetdgdat Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer ) As Short

    ' Teaching data get stop 
    Declare Function cnc_stopgetdgdat Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer ) As Short

    ' Teaching data read 
    Declare Function cnc_rddgdat Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As Short, <[In], Out> ByVal b() As Short) As Short

    ' Teaching data read pointer write 
    Declare Function cnc_wrdgdatptr Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short ) As Short

    ' Teaching data clear 
    Declare Function cnc_clrdgdat Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer ) As Short


'---------------------------------
' CNC : C-EXE SRAM file function  
'---------------------------------

    ' open C-EXE SRAM file 
    Declare Function cnc_opencexefile Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As String, ByVal b As Short, ByVal c As Short ) As Short

    ' close C-EXE SRAM file 
    Declare Function cnc_closecexefile Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer ) As Short

    ' read C-EXE SRAM file 
    Declare Function cnc_rdcexefile Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, <[In], Out> ByVal a() As Byte, ByRef b As Integer) As Short

    ' write C-EXE SRAM file 
    Declare Function cnc_wrcexefile Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a() As Byte, ByRef b As Integer) As Short

    ' read C-EXE SRAM disk directory 
    Declare Function cnc_cexedirectory Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, _
        <[In], Out> ByVal a() As Char, ByRef b As Short, ByVal c As Short, ByRef d As CFILEINFO) As Short


'-----
' PMC 
'-----

    ' read message from PMC to MMC 
    Declare Function pmc_rdmsg Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As Short, <[In], Out> ByVal b() As Short) As Short

    ' write message from MMC to PMC 
    Declare Function pmc_wrmsg Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByVal b() As Short) As Short

    ' read message from PMC to MMC(conditional) 
    Declare Function pmc_crdmsg Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As Short, <[In], Out> ByVal b() As Short) As Short

    ' write message from MMC to PMC(conditional) 
    Declare Function pmc_cwrmsg Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByVal b() As Short) As Short

    ' read PMC data(area specified) 
    Declare Function pmc_rdpmcrng Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, _
        ByVal a As Short, ByVal b As Short, ByVal c As Integer, ByVal d As Integer, ByVal e As Integer, ByRef f As IODBPMC0) As Short
    Declare Function pmc_rdpmcrng Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, _
        ByVal a As Short, ByVal b As Short, ByVal c As Integer, ByVal d As Integer, ByVal e As Integer, ByRef f As IODBPMC1) As Short
    Declare Function pmc_rdpmcrng Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, _
        ByVal a As Short, ByVal b As Short, ByVal c As Integer, ByVal d As Integer, ByVal e As Integer, ByRef f As IODBPMC2) As Short

    ' write PMC data(area specified) 
    Declare Function pmc_wrpmcrng Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Integer, ByRef b As IODBPMC0) As Short
    Declare Function pmc_wrpmcrng Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Integer, ByRef b As IODBPMC1) As Short
    Declare Function pmc_wrpmcrng Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Integer, ByRef b As IODBPMC2) As Short

    ' read data from extended backup memory 
    Declare Function pmc_rdkpm Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Integer, <[In], Out> ByVal b() As Byte, ByVal c As Integer) As Short

    ' write data to extended backup memory 
    Declare Function pmc_wrkpm Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b() As Byte, ByVal c As Integer) As Short

    ' read data from extended backup memory 2 
    Declare Function pmc_rdkpm2 Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Integer, <[In], Out> ByVal b() As Byte, ByVal c As Integer) As Short

    ' write data to extended backup memory 2 
    Declare Function pmc_wrkpm2 Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b() As Byte, ByVal c As Integer) As Short

    ' read maximum size of extended backup memory 
    Declare Function pmc_kpmsiz Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As Integer) As Short

    ' read informations of PMC data 
    Declare Function pmc_rdpmcinfo Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As ODBPMCINF ) As Short

    ' read PMC parameter data table contorol data 
    Declare Function pmc_rdcntldata Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Short, ByVal c As Short, ByRef d As IODBPMCCNTL ) As Short

    ' write PMC parameter data table contorol data 
    Declare Function pmc_wrcntldata Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As IODBPMCCNTL ) As Short

    ' read PMC parameter data table contorol data group number 
    Declare Function pmc_rdcntlgrp Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As Short ) As Short

    ' write PMC parameter data table contorol data group number 
    Declare Function pmc_wrcntlgrp Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short ) As Short

    ' read PMC alarm message 
    Declare Function pmc_rdalmmsg Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As Short, ByRef c As Short, ByRef d As ODBPMCALM ) As Short

    ' get detail error for pmc 
    Declare Function pmc_getdtailerr Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As ODBPMCERR ) As Short

    ' read PMC memory data 
    Declare Function pmc_rdpmcmem Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, _
        ByVal a As Short, ByVal b As Integer, ByVal c As Integer, <[In], Out> ByVal d() As Byte) As Short

    ' write PMC memory data 
    Declare Function pmc_wrpmcmem Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, _
        ByVal a As Short, ByVal b As Integer, ByVal c As Integer, ByVal d() As Byte) As Short

    ' read PMC-SE memory data 
    Declare Function pmc_rdpmcsemem Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, _
        ByVal a As Short, ByVal b As Integer, ByVal c As Integer, <[In], Out> ByVal d() As Byte) As Short

    ' write PMC-SE memory data 
    Declare Function pmc_wrpmcsemem Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, _
        ByVal a As Short, ByVal b As Integer, ByVal c As Integer, ByVal d() As Byte) As Short

    ' read pmc title data 
    Declare Function pmc_rdpmctitle Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As ODBPMCTITLE ) As Short

    ' read PMC parameter start
    Declare Function pmc_rdprmstart Lib "FWLIB32.DLL" _
       (ByVal FlibHndl As Integer) As Short

    ' read PMC parameter
    Declare Function pmc_rdpmcparam Lib "FWLIB32.DLL" _
       (ByVal FlibHndl As Integer, ByRef a As Integer, ByVal b() As Byte) As Short

    ' read PMC parameter end
    Declare Function pmc_rdprmend Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer) As Short

    ' write PMC parameter start
    Declare Function pmc_wrprmstart Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer) As Short

    ' write PMC parameter
    Declare Function pmc_wrpmcparam Lib "FWLIB32.DLL" _
       (ByVal FlibHndl As Integer, ByRef a As Integer, ByVal b() As Byte) As Short

    ' write PMC parameter end
    Declare Function pmc_wrprmend Lib "FWLIB32.DLL" _
       (ByVal FlibHndl As Integer) As Short

    ' read PMC data 
'  Declare Function pmc_rdpmcrng_ext Lib "FWLIB32.DLL" _
'      ( ByVal FlibHndl As Integer, _
'      ByVal a As Short, ByRef b As IODBPMCEXT ) As Short

    ' write PMC I/O link assigned data 
'  Declare Function pmc_wriolinkdat Lib "FWLIB32.DLL" _
'      ( ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b() As Byte, ByVal c As Integer ) As Short

    ' read PMC address information 
    Declare Function pmc_rdpmcaddr Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As ODBPMCADR ) As Short

    ' select PMC unit
    Declare Function pmc_select_pmc_unit Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Integer) As Short

    ' get current PMC unit
    Declare Function pmc_get_current_pmc_unit Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As Integer) As Short

    ' get number of PMC
    Declare Function pmc_get_number_of_pmc Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As Integer) As Short

    ' get PMC unit types
    Declare Function pmc_get_pmc_unit_types Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a() As Integer, ByVal b As Integer) As Short

    ' set PMC Timer type
    Declare Function pmc_set_timer_type Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, ByVal c() As Short) As Short

    ' get PMC Timer type
    Declare Function pmc_get_timer_type Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Integer, ByVal b As Integer, <[In](), Out()> ByVal c() As Short) As Short

'----------------------------
' PMC : PROFIBUS function    
'----------------------------

    ' read PROFIBUS configration data 
    Declare Function pmc_prfrdconfig Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As ODBPRFCNF ) As Short

    ' read bus parameter for master function 
    Declare Function pmc_prfrdbusprm Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As IODBBUSPRM ) As Short

    ' write bus parameter for master function 
    Declare Function pmc_prfwrbusprm Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As IODBBUSPRM ) As Short

    ' read slave parameter for master function 
    Declare Function pmc_prfrdslvprm Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As IODBSLVPRM ) As Short
    Declare Function pmc_prfrdslvprm Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As IODBSLVPRM2 ) As Short

    ' write slave parameter for master function 
    Declare Function pmc_prfwrslvprm Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As IODBSLVPRM ) As Short
    Declare Function pmc_prfwrslvprm Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As IODBSLVPRM2 ) As Short

    ' read allocation address for master function 
    Declare Function pmc_prfrdallcadr Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As IODBPRFADR ) As Short

    ' set allocation address for master function 
    Declare Function pmc_prfwrallcadr Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As IODBPRFADR ) As Short

    ' read allocation address for slave function 
    Declare Function pmc_prfrdslvaddr Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As IODBSLVADR ) As Short

    ' set allocation address for slave function 
    Declare Function pmc_prfwrslvaddr Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As IODBSLVADR ) As Short

    ' read status for slave function 
    Declare Function pmc_prfrdslvstat Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As ODBSLVST ) As Short

    ' Reads DI/DO parameter of master function
    Declare Function pmc_prfrddido Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As IODBDIDO) As Short

    ' Writes DI/DO parameter of master function
    Declare Function pmc_prfwrdido Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As IODBDIDO) As Short

    ' Reads slave index data of master function */
    Declare Function pmc_prfrdslvid Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As IODBSLVID) As Short

    ' Writes slave index data of master function
    Declare Function pmc_prfwrslvid Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As IODBSLVID) As Short

    ' Reads slave parameter of master function(2)
    Declare Function pmc_prfrdslvprm2 Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As IODBSLVPRM3) As Short

    ' Writes slave parameter of master function(2)
    Declare Function pmc_prfwrslvprm2 Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As IODBSLVPRM3) As Short

    ' Reads indication address of master function
    Declare Function pmc_prfrdindiadr Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As IODBINDEADR) As Short

    ' Writes indication address of master function
    Declare Function pmc_prfwrindiadr Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As IODBINDEADR) As Short

    ' Reads operation mode of master function
    Declare Function pmc_prfrdopmode Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As Short) As Short

    ' Writes operation mode of master function
    Declare Function pmc_prfwropmode Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As Short) As Short

'-----------------------------------------------v
' DS : Data server & Ethernet board function    '
'-----------------------------------------------'

    ' read the parameter of the Ethernet board */
    Declare Function etb_rdparam Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As IODBETP_TCP) As Short
    Declare Function etb_rdparam Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As IODBETP_HOST) As Short
    Declare Function etb_rdparam Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As IODBETP_FTP) As Short
    Declare Function etb_rdparam Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As IODBETP_ETB) As Short

    ' write the parameter of the Ethernet board */
    Declare Function etb_wrparam Lib "FWLIB32.DLL" _
       (ByVal FlibHndl As Integer, ByRef a As IODBETP_TCP) As Short
    Declare Function etb_wrparam Lib "FWLIB32.DLL" _
       (ByVal FlibHndl As Integer, ByRef a As IODBETP_HOST) As Short
    Declare Function etb_wrparam Lib "FWLIB32.DLL" _
       (ByVal FlibHndl As Integer, ByRef a As IODBETP_FTP) As Short

    ' read the error message of the Ethernet board */
    Declare Function etb_rderrmsg Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As ODBETMSG) As Short

    ' read the mode of the Data Server
    Declare Function ds_rdmode Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As Short) As Short

    ' write the mode of the Data Server
    Declare Function ds_wrmode Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short) As Short

    ' read information of the Data Server's HDD
    Declare Function ds_rdhddinfo Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As ODBHDDINF) As Short

    ' read the file list of the Data Server's HDD
    Declare Function ds_rdhdddir Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As String, ByVal b As Integer, ByRef c As Short, ByRef d As ODBHDDDIR) As Short

    ' delete the file of the Data Serve's HDD 
    Declare Function ds_delhddfile Lib "FWLIB32.DLL" _
       (ByVal FlibHndl As Integer, ByVal a As String) As Short

    ' copy the file of the Data Server's HDD
    Declare Function ds_copyhddfile Lib "FWLIB32.DLL" _
       (ByVal FlibHndl As Integer, ByVal a As String, ByVal b As String) As Short

    ' change the file name of the Data Server's HDD */
    Declare Function ds_renhddfile Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As String, ByVal b As String) As Short

    ' execute the PUT command of the FTP
    Declare Function ds_puthddfile Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As String, ByVal b As String) As Short

    ' execute the MPUT command of the FTP
    Declare Function ds_mputhddfile Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As String) As Short

    ' read information of the host
    Declare Function ds_rdhostinfo Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As Integer, ByVal b As Integer) As Short

    ' read the file list of the host
    Declare Function ds_rdhostdir Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Integer, ByRef c As Short, ByRef d As ODBHOSTDIR, ByVal e As Integer) As Short

    ' read the file list of the host 2
    Declare Function ds_rdhostdir2 Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Integer, ByRef c As Short, ByRef d As Integer, ByRef e As ODBHOSTDIR, ByVal f As Integer) As Short

    ' delete the file of the host
    Declare Function ds_delhostfile Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As String, ByVal b As Integer) As Short

    ' execute the GET command of the FTP
    Declare Function ds_gethostfile Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As String, ByVal b As String) As Short

    ' execute the MGET command of the FTP
    Declare Function ds_mgethostfile Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As String) As Short

    ' read the execution result
    Declare Function ds_rdresult Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer) As Short

    ' stop the execution of the command
    Declare Function ds_cancel Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer) As Short

    ' read the file from the Data Server
    Declare Function ds_rdncfile Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As String) As Short

    ' read the file from the Data Server 2
    Declare Function ds_rdncfile2 Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As String) As Short

    ' write the file to the Data Server
    Declare Function ds_wrncfile Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByVal b As Integer) As Short

    ' read the file name for the DNC operation in the Data Server's HDD
    Declare Function ds_rddnchddfile Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, <[In](), Out()> ByVal a() As Char) As Short
    '(ByVal FlibHndl As Integer, ByRef a As String) As Short

    ' write the file name for the DNC operation in the Data Server's HDD
    Declare Function ds_wrdnchddfile Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As String) As Short

    ' read the file name for the DNC operation in the host
    Declare Function ds_rddnchostfile Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As Short, <[In](), Out()> ByVal b() As Char) As Short

    ' write the file name for the DNC operation in the host
    Declare Function ds_wrdnchostfile Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As String) As Short

    ' read the connecting host number
    Declare Function ds_rdhostno Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As Short) As Short

    ' read maintenance information
    Declare Function ds_rdmntinfo Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As DSMNTINFO) As Short

    ' check the Data Server's HDD
    Declare Function ds_checkhdd Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer) As Short

    ' format the Data Server's HDD
    Declare Function ds_formathdd Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer) As Short

    ' create the directory in the Data Server's HDD */
    Declare Function ds_makehdddir Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As String) As Short

    ' delete directory in the Data Server's HDD
    Declare Function ds_delhdddir Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As String) As Short

    ' change the current directory
    Declare Function ds_chghdddir Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As String) As Short

    ' execute the PUT command according to the list file
    Declare Function ds_lputhddfile Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As String) As Short

    ' delete files according to the list file
    Declare Function ds_ldelhddfile Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As String) As Short

    ' execute the GET command according to the list file
    Declare Function ds_lgethostfile Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As String) As Short

    ' read the directory for M198 operation
    Declare Function ds_rdm198hdddir Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As String) As Short
    '(ByVal FlibHndl As Integer, ByVal a() As Byte) As Short

    ' write the directory for M198 operation
    Declare Function ds_wrm198hdddir Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer) As Short

    ' read the connecting host number for the M198 operation
    Declare Function ds_rdm198host Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByRef a As Short) As Short

    ' write the connecting host number for the M198 operation
    Declare Function ds_wrm198host Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer) As Short

    ' write the connecting host number
    Declare Function ds_wrhostno Lib "FWLIB32.DLL" _
        (ByVal FlibHndl As Integer, ByVal a As Short) As Short

    ' search string in data server program
    Declare Function ds_searchword Lib "FWLIB32.DLL" _
       (ByVal FlibHndl As Integer, ByVal a As String) As Short

    ' read the searching result
    Declare Function ds_searchresult Lib "FWLIB32.DLL" _
      (ByVal FlibHndl As Integer) As Short

    ' read file in the Data Server's HDD
    Declare Function ds_rdfile Lib "FWLIB32.DLL" _
      (ByVal FlibHndl As Integer, ByVal a As String, ByVal b As String) As Short

    ' write file in the Data Server's HDD
    Declare Function ds_wrfile Lib "FWLIB32.DLL" _
      (ByVal FlibHndl As Integer, ByVal a As String, ByVal b As String) As Short

'--------------------------
' HSSB multiple connection 
'--------------------------

    ' read number of node 
    Declare Function cnc_rdnodenum Lib "FWLIB32.DLL" _
        ( ByRef a As Integer ) As Short

    ' read node informations 
    Declare Function cnc_rdnodeinfo Lib "FWLIB32.DLL" _
        ( ByVal a As Integer, ByRef b As ODBNODE ) As Short

    ' set default node number 
    Declare Function cnc_setdefnode Lib "FWLIB32.DLL" _
        ( ByVal a As Integer ) As Short

    ' allocate library handle 2 
    Declare Function cnc_allclibhndl2 Lib "FWLIB32.DLL" _
        (ByVal node As Integer, ByRef FlibHndl As Integer) As Short


'---------------------
' Ethernet connection 
'---------------------

    ' allocate library handle 3 
    Declare Function cnc_allclibhndl3 Lib "FWLIB32.DLL" _
        (ByVal ip As String, ByVal port As Short, ByVal timeout As Integer, ByRef FlibHndl As Integer) As Short

    ' allocate library handle 4 
    Declare Function cnc_allclibhndl4 Lib "FWLIB32.DLL" _
        (ByVal ip As String, ByVal port As Short, ByVal timeout As Integer, ByVal id As Integer, ByRef FlibHndl As Integer) As Short

    ' set timeout for socket 
    Declare Function cnc_settimeout Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Integer ) As Short

    ' reset all socket connection 
    Declare Function cnc_resetconnect Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer ) As Short

    ' get option state for FOCAS1/Ethernet 
    Declare Function cnc_getfocas1opt Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByVal a As Short, ByRef b As Integer ) As Short

    ' read Ethernet board information 
    Declare Function cnc_rdetherinfo Lib "FWLIB32.DLL" _
        ( ByVal FlibHndl As Integer, ByRef a As Short, ByRef b As Short ) As Short

End Class 'Focas1
