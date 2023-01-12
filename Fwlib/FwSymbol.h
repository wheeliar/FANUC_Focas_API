/*-------------------------------------------------------------------*/
/* fwsymbol.h                                                        */
/*                                                                   */
/* Declaration of constants for FOCAS1 function                      */
/*                                                                   */
/* Copyright (C) 2003-2011 by FANUC CORPORATION All rights reserved. */
/*                                                                   */
/*-------------------------------------------------------------------*/

#ifndef _INC_FWSYMBOL
#define _INC_FWSYMBOL

#ifdef __cplusplus
extern "C" {
#endif

/* cnc_allclibhndl2 */
/* cnc_rdnodeinfo */
/* cnc_setdefnode */
#define HSSB_NODE_0		0
#define HSSB_NODE_1		1
#define HSSB_NODE_2		2
#define HSSB_NODE_3		3
#define HSSB_NODE_4		4
#define HSSB_NODE_5		5
#define HSSB_NODE_6		6
#define HSSB_NODE_7		7

/* cnc_rdnodeinfo */
#define NOT_INSTALLED		0
#define INSTALLED		1

/* cnc_rdnodeinfo */
#define SERIES_160		1
#define SERIES_150		2
#define POWER_MATE		3
#define POWER_MATE_i		4
#define SERIES_160iW		5
#define SERIES_150i		6
#define SERIES_0i_A		7
#define SERIES_0i_B		8
#define SERIES_300i		9

/* cnc_absolute */
/* cnc_machine */
/* cnc_relative */
/* cnc_distance */
/* cnc_skip */
/* cnc_srvdelay */
/* cnc_accdecdly */
/* cnc_rddynamic */
/* cnc_rddynamic2 */
/* cnc_wrrelpos */
/* cnc_prstwkcd */
/* cnc_rdmovrlap */
/* cnc_canmovrlap */
/* cnc_absolute2 */
/* cnc_relative2 */
/* cnc_rdzofs */
/* cnc_wrzofs */
/* cnc_rdzofsr */
/* cnc_wrzofsr */
/* cnc_rdparam */
/* cnc_wrparam */
/* cnc_rdparar */
/* cnc_wrparas */
/* cnc_rdset */
/* cnc_wrset */
/* cnc_rdsetr */
/* cnc_wrsets */
/* cnc_rdactfixofs */
/* cnc_rdfixofs */
/* cnc_wrfixofs */
/* cnc_rdwkcdshft */
/* cnc_wrwkcdshft */
/* cnc_rdwkcdsfms */
/* cnc_wrwkcdsfms */
/* cnc_svdtstartrd */
/* cnc_svdtstartrd2 */
/* cnc_svdtstartwr */
/* cnc_svdtstartwr2 */
/* cnc_diagnoss */
/* cnc_diagnosr */
/* #define ALL_AXES		-1 */ /* defined in fwlib32.h */
#define AXIS_1			1
#define AXIS_2			2
#define AXIS_3			3
#define AXIS_4			4
#define AXIS_5			5
#define AXIS_6			6
#define AXIS_7			7
#define AXIS_8			8
#define AXIS_9			8
#define AXIS_10			10
#define AXIS_11			11
#define AXIS_12			12
#define AXIS_13			13
#define AXIS_14			14
#define AXIS_15			15
#define AXIS_16			16
#define AXIS_17			17
#define AXIS_18			18
#define AXIS_19			19
#define AXIS_20			20
#define AXIS_21			21
#define AXIS_22			22
#define AXIS_23			23
#define AXIS_24			24
#define AXIS_25			25
#define AXIS_26			26
#define AXIS_27			27
#define AXIS_28			28
#define AXIS_29			29
#define AXIS_30			30
#define AXIS_31			31
#define AXIS_32			32

/* cnc_rddynamic */
/* cnc_rdparainfo */
/* cnc_rdsetinfo */
/* cnc_rdhpccset */
/* cnc_wrhpccset */
/* cnc_rdhpcctupr */
/* cnc_wrhpcctupr */
/* cnc_rdhpcctuac */
/* cnc_wrhpcctuac */
/* cnc_hpccattune */
/* cnc_rdbaxis */
/* cnc_rdophistry */
/* cnc_rdalmhistry */
/* cnc_rdalmhistry2 */
/* cnc_rdctrldi */
/* cnc_rdctrldo */
/* cnc_rdwaveprm */
/* cnc_wrwaveprm */
/* cnc_rdwaveprm2 */
/* cnc_wrwaveprm2 */
/* cnc_rdcomparam */
/* cnc_wrcomparam */
/* cnc_rdtlctldata */
/* cnc_wrtlctldata */
/* cnc_rdtooldata */
/* cnc_wrtooldata */
/* cnc_rdmultitldt */
/* cnc_wrmultitldt */
/* cnc_rdsafetyzone */
/* cnc_wrsafetyzone */
/* cnc_rdtoolzone */
/* cnc_wrtoolzone */
/* cnc_rdacttlzone */
/* cnc_rdpscdproc */
/* cnc_wrpscdproc */
/* cnc_rdpscdpirc */
/* cnc_wrpscdpirc */
/* cnc_rdpscdedge */
/* cnc_wrpscdedge */
/* cnc_rdpscdslop */
/* cnc_wrpscdslop */
/* cnc_rdlpwrdty */
/* cnc_wrlpwrdty */
/* cnc_rdlpwrdat */
/* cnc_rdlagslt */
/* cnc_wrlagslt */
/* cnc_rdlagst */
/* cnc_wrlagst */
/* cnc_rdledgprc */
/* cnc_wrledgprc */
/* cnc_rdlprcprc */
/* cnc_wrlprcprc */
/* cnc_rdlcmddat */
/* cnc_rdlactnum */
/* cnc_sysinfo */
/* cnc_alarm */
/* cnc_alarm2 */
/* cnc_rdalminfo */
/* cnc_modal */
/* cnc_rdgcode */
/* cnc_rdcommand */
/* cnc_rddiaginfo */
/* cnc_rdopnlsgnl */
/* cnc_wropnlsgnl */
/* cnc_rdopnlgnrl */
/* cnc_wropnlgnrl */
/* cnc_rdopnlgsname */
/* cnc_wropnlgsname */
/* cnc_rdmenuswitch */
/* cnc_wrmenuswitch */
/* cnc_rdcdslctprm */
/* cnc_rdpdf_alldir */
/* pmc_wrcntldata */
/* pmc_prfrdbusprm */
/* pmc_prfwrbusprm */
/* pmc_prfrdslvprm */
/* pmc_prfwrslvprm */
#define BIT16_0			0x0001
#define BIT16_1			0x0002
#define BIT16_2			0x0004
#define BIT16_3			0x0008
#define BIT16_4			0x0010
#define BIT16_5			0x0020
#define BIT16_6			0x0040
#define BIT16_7			0x0080
#define BIT16_8			0x0100
#define BIT16_9			0x0200
#define BIT16_10		0x0400
#define BIT16_11		0x0800
#define BIT16_12		0x1000
#define BIT16_13		0x2000
#define BIT16_14		0x4000
#define BIT16_15		0x8000
#define BIT32_0			0x00000001
#define BIT32_1			0x00000002
#define BIT32_2			0x00000004
#define BIT32_3			0x00000008
#define BIT32_4			0x00000010
#define BIT32_5			0x00000020
#define BIT32_6			0x00000040
#define BIT32_7			0x00000080
#define BIT32_8			0x00000100
#define BIT32_9			0x00000200
#define BIT32_10		0x00000400
#define BIT32_11		0x00000800
#define BIT32_12		0x00001000
#define BIT32_13		0x00002000
#define BIT32_14		0x00004000
#define BIT32_15		0x00008000
#define BIT32_16		0x00010000
#define BIT32_17		0x00020000
#define BIT32_18		0x00040000
#define BIT32_19		0x00080000
#define BIT32_20		0x00100000
#define BIT32_21		0x00200000
#define BIT32_22		0x00400000
#define BIT32_23		0x00800000
#define BIT32_24		0x01000000
#define BIT32_25		0x02000000
#define BIT32_26		0x04000000
#define BIT32_27		0x08000000
#define BIT32_28		0x10000000
#define BIT32_29		0x20000000
#define BIT32_30		0x40000000
#define BIT32_31		0x80000000

/* cnc_rdspload */
/* cnc_rdspmaxrpm */
/* cnc_rdspgear */
/* cnc_acts2 */
/* cnc_rdspdlspeed */
/* #define ALL_SPINDLES		-1 */ /* defined in fwlib32.h */
#define SPINDLE_1		1
#define SPINDLE_2		2
#define SPINDLE_3		3
#define SPINDLE_4		4
#define SPINDLE_5		5
#define SPINDLE_6		6
#define SPINDLE_7		7
#define SPINDLE_8		8

/* cnc_setvrtclpos */
#define AXIS_U_V		-1
#define AXIS_U			3
#define AXIS_V			4

/* cnc_clr3dplsmov */
#define MODE_1			1
#define MODE_2			2
#define MODE_3			3
#define MODE_4			4
#define MODE_5			5
#define MODE_6			6
#define MODE_7			7
#define MODE_8			8
#define MODE_9			9
#define MODE_10			10
#define ALL_MODE		-1

/* cnc_rdposition */
#define POS_ABS			0
#define POS_MACH		1
#define POS_REL			2
#define POS_DIST		3
#define POS_ALL			-1

/* cnc_rdposition */
#define UNIT_MM			0
#define UNIT_INCH		1
#define UNIT_DEGREE		2

/* cnc_rdposition */
#define NOTDISPLAY		0
#define DISPLAY			1

/* cnc_rdspeed */
#define FEED_RATE		0
#define SPINDLE_SPEED		1
#define ALL_SPEED		-1

/* cnc_rdspeed */
#define UNIT_MM_MIN		0
#define UNIT_INCH_MIN		1
#define UNIT_RPM		2
#define UNIT_MM_REV		3
#define UNIT_INCH_REV		4

/* cnc_rdspmeter */
#define SP_LOAD_METER		0
#define SP_MOTER_SPEED		1
#define SP_ALL_TYPE		-1

/* cnc_rdspmeter */
#define SP_UNIT_PERCENT		0
#define SP_UNIT_RPM		1

/* cnc_rdspdlspeed */
#define GEAR_1			1
#define GEAR_2			2
#define GEAR_3			3

/* cnc_dncend2 */
/* #define DNC_NORMAL		(-1)     */ /* defined in fwlib32.h */
/* #define DNC_CANCEL		(-32768) */ /* defined in fwlib32.h */
/* #define DNC_OPENERR		(-514)   */ /* defined in fwlib32.h */
/* #define DNC_NOFILE		(-516)   */ /* defined in fwlib32.h */
/* #define DNC_READERR		(-517)   */ /* defined in fwlib32.h */

/* cnc_rdprogdir */
/* cnc_rdprogdir2 */
/* cnc_rdprogdir3 */
/* cnc_rdprogdir4 */
#define PRGDIR_0		0
#define PRGDIR_1		1
#define PRGDIR_2		2

/* cnc_rdpdf_alldir */
#define SIZE_PAGE		0
#define SIZE_BYTE		1
#define SIZE_KBYTE		2
#define SIZE_MBYTE		3

/* cnc_rdpdf_alldir */
#define DIR_TYPE_0		0
#define DIR_TYPE_1		1

/* cnc_rdpdf_alldir */
#define KIND_FOLDER		0
#define KIND_FILE		1

/* cnc_rdproginfo */
#define FORMAT_BINARY		0
#define FORMAT_ASCII		1

/* cnc_rdproginfo */
#define BINARY_15_LEN		16
#define ASCII_15_LEN		35
#define BINARY_16_LEN		12
#define ASCII_16_LEN		31

/* cnc_dwnstart3 */
/* cnc_upstart3 */
#define TYPE_NC_PROGRAM		0
#define TYPE_TOOL_OFFSET	1
#define TYPE_PARAMETER		2
#define TYPE_PITCH_ERROR	3
#define TYPE_CUSTOM_MACRO	4
#define TYPE_WORK_ZERO_OFFSET	5

/* cnc_dwnend3m */
#define SLCT_NONE		0
#define SLCT_PROG		1

/* cnc_searchword */
#define SEARCH_CHAR		0
#define SEARCH_WORD		1
#define SEARCH_LAST_LINE	2

/* cnc_searchword */
#define SEARCH_REVERSE		0
#define SEARCH_FORWORD		1

/* cnc_condense */
#define CONDENSE_MDI_PROG	0
#define CONDENSE_ONE_PROG	1
#define CONDENSE_ALL_PROG	2

/* cnc_rdmdiprgstat */
#define MDI_NO_PROG		0x0000
#define MDI_INIT		0x0001
#define MDI_EXIST		0x0002

/* cnc_rdmdiprgstat */
#define MDI_START_TOP		0x0004
#define MDI_START_POINTER	0x0000

/* cnc_rdtofs */
/* cnc_wrtofs */
#define OFST_M_WEAR_R		0
#define OFST_M_GEOM_R		1
#define OFST_M_WEAR_T		2
#define OFST_M_GEOM_T		3
#define OFST_T_WEAR_X		0
#define OFST_T_GEOM_X		1
#define OFST_T_WEAR_Z		2
#define OFST_T_GEOM_Z		3
#define OFST_T_WEAR_R		4
#define OFST_T_GEOM_R		5
#define OFST_T_WEAR_N		6
#define OFST_T_GEOM_N		7
#define OFST_T_WEAR_Y		8
#define OFST_T_GEOM_Y		9

/* cnc_rdtofsr */
/* cnc_wrtofsr */
#define OFST_M_A		0
#define OFST_M_A_N		9
#define OFST_M_A_ALL		-1
#define OFST_M_A_ALL2		-4
#define OFST_M_B_GEOM		0
#define OFST_M_B_WEAR		1
#define OFST_M_B_N		9
#define OFST_M_B_ALL		-2
#define OFST_M_B_ALL2		-5
#define OFST_M_C_GEOM_T		0
#define OFST_M_C_WEAR_T		1
#define OFST_M_C_GEOM_R		2
#define OFST_M_C_WEAR_R		3
#define OFST_M_C_N		9
#define OFST_M_C_ALL		-3
#define OFST_M_C_ALL2		-6
#define OFST_T_A_N		0
#define OFST_T_A_X		1
#define OFST_T_A_Y		2
#define OFST_T_A_Z		3
#define OFST_T_A_R		4
#define OFST_T_A_ALL		-1
#define OFST_T_B_N		0
#define OFST_T_B_GEOM_X		1
#define OFST_T_B_GEOM_Y		2
#define OFST_T_B_GEOM_Z		3
#define OFST_T_B_GEOM_R		4
#define OFST_T_B_WEAR_X		5
#define OFST_T_B_WEAR_Y		6
#define OFST_T_B_WEAR_Z		7
#define OFST_T_B_WEAR_R		8
#define OFST_T_B_ALL		-2
#define OFST_T_2ND_GEOM_X	100
#define OFST_T_2ND_GEOM_Y	101
#define OFST_T_2ND_GEOM_Z	102
#define OFST_T_2ND_GEOM_ALL	-100

/* cnc_rdtofsinfo */
/* cnc_rdbtofsinfo */
#define OFST_TYPE_A		0
#define OFST_TYPE_B		1
#define OFST_TYPE_C		2

/* cnc_rdparam */
/* cnc_wrparam */
/* cnc_rdparar */
/* cnc_wrparas */
/* cnc_rdset */
/* cnc_wrset */
/* cnc_rdsetr */
/* cnc_wrsets */
/* cnc_rdcdslctprm */
#define TYPE_BIT		0
#define TYPE_BYTE		1
#define TYPE_WORD		2
#define TYPE_2WORD		3
#define TYPE_REAL		4

/* cnc_getmactype */
/* cnc_setmactype */
#define MACRO_TYPE_DECIMAL	0
#define MACRO_TYPE_BINARY	1

/* cnc_rdmacroinfo */
#define MACRO_TYPE_0		0
#define MACRO_TYPE_1		1
#define MACRO_TYPE_2		2
#define MACRO_TYPE_3		3
#define MACRO_TYPE_4		4
#define MACRO_TYPE_5		5

/* cnc_rdpmacroinfo */
#define PMACRO_TYPE_A		0
#define PMACRO_TYPE_B		1
#define PMACRO_TYPE_C		2
#define PMACRO_TYPE_D		3

/* cnc_getpmactype */
/* cnc_setpmactype */
#define PMACRO_TYPE_DECIMAL	0
#define PMACRO_TYPE_BINARY	1

/* cnc_rdexecmcode */
/* cnc_rdrstrmcode */
#define STAT_EXECUTED		0
#define STAT_EXECUTING		1
#define STAT_RESTARTING		2

/* cnc_rdintchk */
/* cnc_wrintchk */
#define INTCHK_AREA1_X		0
#define INTCHK_AREA1_Z		1
#define INTCHK_AREA1_I		2
#define INTCHK_AREA1_K		3
#define INTCHK_AREA2_X		4
#define INTCHK_AREA2_Z		5
#define INTCHK_AREA2_I		6
#define INTCHK_AREA2_K		7
#define INTCHK_X		0
#define INTCHK_Y		1
#define INTCHK_Z		2
#define INTCHK_I		3
#define INTCHK_J		4
#define INTCHK_K		5

/* cnc_rdhpccset */
/* cnc_wrhpccset */
/* cnc_rdlpwrctrl */
/* cnc_wrlpwrctrl */
#define INVALID			0
#define VALID			1

/* cnc_hpccattune */
/* cnc_hpccactfine */
/* cnc_hpccselfine */
#define TYPE_FINE		0
#define TYPE_MEDIUM		1
#define TYPE_ROUGH		2

/* cnc_rdbtofsr */
/* cnc_wrbtofsr */
#define BOFST_A			0
#define BOFST_A_ALL		-1
#define BOFST_B_GEOM		0
#define BOFST_B_WEAR		1
#define BOFST_B_ALL		-2

/* cnc_rdbaxis */
#define NONE			0
#define EXISTS			1

/* cnc_t1info */
/* cnc_t2info */
/* cnc_rd1tlifedata */
/* cnc_rd2tlifedata */
/* cnc_wr1tlifedata */
/* cnc_wr2tlifedata */
/* cnc_rd1tlifedat2 */
/* cnc_wr1tlifedat2 */
#define TLIFE_REFER		0
#define TLIFE_REGST		1
#define TLIFE_EXPIRED		2
#define TLIFE_SKIPPED		3

/* cnc_rdgrpinfo */
/* cnc_wrgrpinfo */
/* cnc_rdgrpinfo4 */
#define COUNTER_COUNT		0
#define COUNTER_MINUTE		1

/* cnc_rdgrpinfo4 */
#define ORDER_ASCENDING		0
#define ORDER_DESCENDING	1

/* cnc_wrtool2 */
#define TLMNG_ID_1		1
#define TLMNG_ID_2		2
#define TLMNG_ID_3		3
#define TLMNG_ID_4		4
#define TLMNG_ID_5		5
#define TLMNG_ID_6		6
#define TLMNG_ID_7		7
#define TLMNG_ID_8		8
#define TLMNG_ID_9		9
#define TLMNG_ID_10		10
#define TLMNG_ID_11		11
#define TLMNG_ID_12		12
#define TLMNG_ID_13		13
#define TLMNG_ID_14		14
#define TLMNG_ID_15		15
#define TLMNG_ID_16		16
#define TLMNG_ID_17		17
#define TLMNG_ID_18		18
#define TLMNG_ID_19		19
#define TLMNG_ID_20		20
#define TLMNG_ID_21		21
#define TLMNG_ID_22		22
#define TLMNG_ID_23		23
#define TLMNG_ID_24		24
#define TLMNG_ID_25		25
#define TLMNG_ID_26		26
#define TLMNG_ID_27		27
#define TLMNG_ID_28		28
#define TLMNG_ID_29		29
#define TLMNG_ID_30		30
#define TLMNG_ID_31		31
#define TLMNG_ID_32		32
#define TLMNG_ID_33		33
#define TLMNG_ID_34		34
#define TLMNG_ID_35		35
#define TLMNG_ID_36		36
#define TLMNG_ID_37		37
#define TLMNG_ID_38		38
#define TLMNG_ID_39		39
#define TLMNG_ID_40		40
#define TLMNG_ID_41		41
#define TLMNG_ID_42		42
#define TLMNG_ID_43		43
#define TLMNG_ID_44		44
#define TLMNG_ID_45		45
#define TLMNG_ID_46		46
#define TLMNG_ID_47		47
#define TLMNG_ID_48		48
#define TLMNG_ID_49		49
#define TLMNG_ID_50		50

/* cnc_rdophistry */
#define HIS_MDI			0
#define HIS_SIGNAL		1
#define HIS_ALARM		2
#define HIS_DATE		3
#define HIS_TIME		4
#define HIS_MDI_2		5
#define HIS_SIGNAL_2		6
#define HIS_ALARM_2		7
#define HIS_MDI_3		10
#define HIS_SIGNAL_3		11
#define HIS_ALARM_3		12

/* cnc_rdophistry */
/* cnc_rdophistry2 */
#define PW_ON			1
#define PW_OTHERS		0

/* cnc_rdophistry */
/* cnc_rdophistry2 */
/* cnc_rdhissgnl */
/* cnc_wrhissgnl */
#define SIGNAL_NON		0
#define SIGNAL_X		1
#define SIGNAL_G		2
#define SIGNAL_Y		3
#define SIGNAL_F		4

/* cnc_rdophistry2 */
#define HIS2_MDI		HIS_MDI
#define HIS2_SIGNAL		HIS_SIGNAL
#define HIS2_ALARM		HIS_ALARM
#define HIS2_DATE		HIS_DATE

/* cnc_rdophistry2 */
/* cnc_rdalmmsg */
/* cnc_rdalmmsg2 */
#define ALARM_TYPE_0		0
#define ALARM_TYPE_1		1
#define ALARM_TYPE_2		2
#define ALARM_TYPE_3		3
#define ALARM_TYPE_4		4
#define ALARM_TYPE_5		5
#define ALARM_TYPE_6		6
#define ALARM_TYPE_7		7
#define ALARM_TYPE_8		8
#define ALARM_TYPE_9		9
#define ALARM_TYPE_10		10
#define ALARM_TYPE_11		11
#define ALARM_TYPE_12		12
#define ALARM_TYPE_13		13
#define ALARM_TYPE_14		14
#define ALARM_TYPE_15		15
#define ALARM_TYPE_16		16
#define ALARM_TYPE_17		17
#define ALARM_TYPE_18		18
#define ALARM_TYPE_19		19
#define ALARM_TYPE_20		20
#define ALARM_TYPE_21		21
#define ALARM_TYPE_22		22
#define ALARM_TYPE_23		23
#define ALARM_TYPE_24		24
#define ALARM_TYPE_25		25
#define ALARM_TYPE_26		26
#define ALARM_TYPE_27		27
#define ALARM_TYPE_28		28
#define ALARM_TYPE_29		29
#define ALARM_TYPE_30		30
#define ALARM_TYPE_31		31

/* cnc_rdophistry2 */
#define EVENT_POWER_OFF		0
#define EVENT_POWER_ON		1
#define EVENT_CHG_DATE		2
#define EVENT_TIME_STAMP	3
#define EVENT_ERACE_DATA	4

/* cnc_clearophis */
#define CLEAR_OP_HIS		0
#define CLEAR_ALARM_HIS		1

/* cnc_rdopmode */
#define OPMODE_OTHERS		0
#define OPMODE_NORMAL		1
#define OPMODE_ORIENTATION	2
#define OPMODE_SYNCHRONOUS	3
#define OPMODE_RIGID		4
#define OPMODE_CS		5
#define OPMODE_POSITIONING	6

/* cnc_rdwaveprm */
/* cnc_wrwaveprm */
#define SAMPLE_BY_START		0
#define SAMPLE_RISES		1
#define SAMPLE_FALLS		2
#define SAMPLE_ALARM		100
#define SAMPLE_TRIGRER_RISES	101
#define SAMPLE_TRIGGER_FALLS	102

/* cnc_rdwaveprm */
/* cnc_wrwaveprm */
/* cnc_rdwaveprm2 */
/* cnc_wrwaveprm2 */
/* cnc_rdwavedata */
#define TYPE_NO_DATA		-1
#define TYPE_SERVO_ERROR8M	0
#define TYPE_PULSES		1
#define TYPE_TORQUE		2
#define TYPE_SERVO_ERROR2M	3
#define TYPE_AFTER_PULSES	4
#define TYPE_ACTUAL_SPEED	5
#define TYPE_ELECTRIC_VALUE	6
#define TYPE_HEAT		7
#define TYPE_COMPOSITE_SPEED	9
#define TYPE_SPINDLE_SPEED	10
#define TYPE_LOAD_METER		11
#define TYPE_CONVERSION_ERROR	12
#define TYPE_SIGNAL		13

/* cnc_rdwaveprm2 */
/* cnc_wrwaveprm2 */
#define SAMPLE2_BY_START	0
#define SAMPLE2_SIGNAL_ON	1
#define SAMPLE2_SIGNAL_OFF	2
#define SAMPLE2_SIGNAL_CHANGE	3
#define SAMPLE2_ALARM		10
#define SAMPLE2_SIGNAL_ON2	11
#define SAMPLE2_SIGNAL_OFF2	12
#define SAMPLE2_SIGNAL_CHANGE2	13
#define SAMPLE2_EVENT_NON	20
#define SAMPLE2_TRIGGER_ALARM	100
#define SAMPLE2_TRIGGER_ON	101
#define SAMPLE2_TRIGGER_OFF	102
#define SAMPLE2_TRIGGER_CHANGE	103
#define SAMPLE2_TRIGGER_ON2	111
#define SAMPLE2_TRIGGER_OFF2	112
#define SAMPLE2_TRIGGER_CHANGE2	113
#define SAMPLE2_TRIGGER_NON	120

/* cnc_wavestat */
#define STAT_COMPLETED		0
#define STAT_SAMPLING		1

/* cnc_dtsvgetmode */
/* cnc_dtsvsetmode */
/* ds_rdmode */
/* ds_wrmode */
#define MODE_STORAGE		0
#define MODE_BUFFER		1
#define MODE_FTP		2

/* cnc_dtsvrderrmsg */
#define TYPE_ERROR		0
#define TYPE_SYSTEM_ERROR	1

/* cnc_dtsvwrfile */
/* cnc_dtsvrdfile */
#define USER_INVALID		0
#define USER_CNC_NO1		1
#define USER_CNC_NO2		2
#define USER_CNC_NO3		3
#define USER_CNC_NO4		4
#define USER_CNC_NO5		5
#define USER_CNC_NO6		6
#define USER_CNC_NO7		7
#define USER_CNC_NO8		8
#define USER_OPEN_NO1		11
#define USER_OPEN_NO2		12
#define USER_OPEN_NO3		13
#define USER_OPEN_NO4		14
#define USER_OPEN_NO5		15
#define USER_OPEN_NO6		16
#define USER_OPEN_NO7		17
#define USER_PMC		21
#define USER_C_EXE		31

/* cnc_clrmsgbuff */
#define BUFFER_RECEIVE		0
#define BUFFER_TRANSMIT		1

/* cnc_rdrcvstat */
#define RECEIVED_NO_MESSAGE	0
#define RECEIVED_MESSAGE	1

/* etb_rdparam */
/* etb_wrparam */
#define PARAM_TCPIP		0
#define PARAM_HOST1		1
#define PARAM_HOST2		2
#define PARAM_HOST3		3
#define PARAM_FTP		4
#define PARAM_ETHERNET		5

/* etb_rdparam */
#define HDD_NOT_MOUNTED		0
#define HDD_MOUNTED		2

/* ds_rdhostdir */
/* ds_rdhostdir2 */
#define FILE_NAME		0
#define DETAIL_INFO		1

/* ds_rddnchostfile */
/* ds_rdhostno */
/* ds_wrhostno */
#define HOST_SERVER1		1
#define HOST_SERVER2		2
#define HOST_SERVER3		3

/* ds_rdm198host */
#define M198HOST_SERVER1	0
#define M198HOST_SERVER2	1
#define M198HOST_SERVER3	2

/* cnc_rdlpwrslt */
/* cnc_wrlpwrslt */
#define USE_ALL_TUBES		0
#define USE_HALF_TUBES		1

/* cnc_svdtrddata */
/* cnc_svdtrddata2 */
#define STAT_READING		1
#define STAT_READING_END	3

/* cnc_svdtwrdata */
/* cnc_svdtwrdata2 */
#define STAT_WRITING		2
#define STAT_WRITING_END	4

/* cnc_statinfo */
#define AUTO15_NO_SELECT	0
#define AUTO15_MDI		1
#define AUTO15_TAPE		2
#define AUTO15_MEMORY		3
#define AUTO15_EDIT		4
#define AUTO15_TEACHIN		5

/* cnc_statinfo */
#define MANUAL15_NO_SELECT	0
#define MANUAL15_REFERENCE	1
#define MANUAL15_INC_FEED	2
#define MANUAL15_HANDLE		3
#define MANUAL15_JOG		4
#define MANUAL15_ANGULAR_JOG	5
#define MANUAL15_INC_HANDLE	6
#define MANUAL15_JOG_HANDLE	7

/* cnc_statinfo */
#define RUN15_STOP		0
#define RUN15_HOLD		1
#define RUN15_START		2
#define RUN15_MSTR		3
#define RUN15_RESTART		4
#define RUN15_PRSR		5
#define RUN15_NSRC		6
#define RUN15_RESTART2		7
#define RUN15_RESET		8
#define RUN15_HPCC		13

/* cnc_statinfo */
#define EDIT15_NO_EDIT		0
#define EDIT15_EDIT		1
#define EDIT15_SEARCH		2
#define EDIT15_VERIFY		3
#define EDIT15_CONDENSE		4
#define EDIT15_READ		5
#define EDIT15_PUNCH		6

/* cnc_statinfo */
#define MOTION15_NOTHING	0
#define MOTION15_MOTION		1
#define MOTION15_DWELL		2
#define MOTION15_WAIT		3

/* cnc_statinfo */
#define MSTB15_NON		0
#define MSTB15_FIN		1

/* cnc_statinfo */
#define EMG15_NOT		0
#define EMG15_EMERGENCY		1

/* cnc_statinfo */
#define WRITE15_NOT		0
#define WRITE15_WRITING		1

/* cnc_statinfo */
#define LABELSKIP15		0
#define LABELSKIP15_NO		1

/* cnc_statinfo */
#define ALARM15_NO		0
#define ALARM15_ALARM		1

/* cnc_statinfo */
#define WARNING15_NO		0
#define WARNING15_WARNING	1

/* cnc_statinfo */
#define BATTERY15_NORMAL	0
#define BATTERY15_LOW		1
#define BATTERY15_LOW2		2

/* cnc_statinfo */
#define AUTO16_MDI		0
#define AUTO16_MEMORY		1
#define AUTO16_NO_SELECT	2
#define AUTO16_EDIT		3
#define AUTO16_HANDLE		4
#define AUTO16_JOG		5
#define AUTO16_TEACHIN_JOG	6
#define AUTO16_TEACHIN_HANDLE	7
#define AUTO16_INC_FEED		8
#define AUTO16_REFERENCE	9
#define AUTO16_REMOTE		10
#define AUTO16_TEST		11

/* cnc_statinfo */
#define MODE16_T		0
#define MODE16_M		1

/* cnc_statinfo */
#define RUN16_RESET		0
#define RUN16_STOP		1
#define RUN16_HOLD		2
#define RUN16_START		3
#define RUN16_MSTR		4

/* cnc_statinfo */
#define MOTION16_NOTHING	0
#define MOTION16_MOTION		1
#define MOTION16_DWELL		2

/* cnc_statinfo */
#define MSTB16_NON		0
#define MSTB16_FIN		1

/* cnc_statinfo */
#define EMG16_NOT		0
#define EMG16_EMERGENCY		1
#define EMG16_RESET		2

/* cnc_statinfo */
#define ALARM16_NO		0
#define ALARM16_ALARM		1
#define ALARM16_BATTERY		2

/* cnc_statinfo */
#define EDIT16M_NO_EDIT		0
#define EDIT16M_EDIT		1
#define EDIT16M_SEARCH		2
#define EDIT16M_OUTPUT		3
#define EDIT16M_INPUT		4
#define EDIT16M_COMPARE		5
#define EDIT16M_LABEL_SKIP	6
#define EDIT16M_RESTART		7
#define EDIT16M_HPCC		8
#define EDIT16M_PTRR		9
#define EDIT16M_RVRS		10
#define EDIT16M_RTRY		11
#define EDIT16M_RVED		12
#define EDIT16M_HANDLE		13
#define EDIT16M_OFFSET		14
#define EDIT16M_WORK_OFFSET	15
#define EDIT16M_AICC		16
#define EDIT16M_MEMORY_CHECK	17
#define EDIT16M_CUSTOMER_BOARD	18
#define EDIT16M_SAVE		19
#define EDIT16M_AI_NANO		20
#define EDIT16M_AI_APC		21
#define EDIT16M_MBL_APC		22
#define EDIT16M_NANO_HP		23
#define EDIT16M_AI_HPCC		24
#define EDIT16M_5_AXIS		25
#define EDIT16M_LEN		26
#define EDIT16M_RAD		27
#define EDIT16M_WZR		28
#define EDIT16M_TCP		39
#define EDIT16M_TWP		40
#define EDIT16M_TCP_TWP		41
#define EDIT16M_APC		42

/* cnc_statinfo */
#define EDIT16T_NO_EDIT		0
#define EDIT16T_EDIT		1
#define EDIT16T_SEARCH		2
#define EDIT16T_OUTPUT		3
#define EDIT16T_INPUT		4
#define EDIT16T_COMPARE		5
#define EDIT16T_LABEL_SKIP	6
#define EDIT16T_OFFSET		7
#define EDIT16T_WORK_SHIFT	8
#define EDIT16T_RESTART		9
#define EDIT16T_PTRR		14
#define EDIT16T_MEMORY_CHECK	17
#define EDIT16T_SAVE		19
#define EDIT16T_NANO_HP		23
#define EDIT16T_AI_HPCC		24
#define EDIT16T_OFSX		26
#define EDIT16T_OFSZ		27
#define EDIT16T_WZR		28
#define EDIT16T_OFSY		29
#define EDIT16T_TOFS		31
#define EDIT16T_TCP		39
#define EDIT16T_TWP		40
#define EDIT16T_TCP_TWP		41
#define EDIT16T_APC		42

/* cnc_statinfo */
#define AUTO_MDI		AUTO16_MDI
#define AUTO_MEMORY		AUTO16_MEMORY
#define AUTO_NO_SELECT		AUTO16_NO_SELECT
#define AUTO_EDIT		AUTO16_EDIT
#define AUTO_HANDLE		AUTO16_HANDLE
#define AUTO_JOG		AUTO16_JOG
#define AUTO_TEACHIN_JOG	AUTO16_TEACHIN_JOG
#define AUTO_TEACHIN_HANDLE	AUTO16_TEACHIN_HANDLE
#define AUTO_INC_FEED		AUTO16_INC_FEED
#define AUTO_REFERENCE		AUTO16_REFERENCE
#define AUTO_REMOTE		AUTO16_REMOTE
#define AUTO_TEST		AUTO16_TEST

/* cnc_statinfo */
#define MODE_T			MODE16_T
#define MODE_M			MODE16_M

/* cnc_statinfo */
#define RUN_RESET		RUN16_RESET
#define RUN_STOP		RUN16_STOP
#define RUN_HOLD		RUN16_HOLD
#define RUN_START		RUN16_START
#define RUN_MSTR		RUN16_MSTR

/* cnc_statinfo */
#define MOTION_NOTHING		MOTION16_NOTHING
#define MOTION_MOTION		MOTION16_MOTION
#define MOTION_DWELL		MOTION16_DWELL

/* cnc_statinfo */
#define MSTB_NON		MSTB16_NON
#define MSTB_FIN		MSTB16_FIN

/* cnc_statinfo */
#define EMG_NOT			EMG16_NOT
#define EMG_EMERGENCY		EMG16_EMERGENCY
#define EMG_RESET		EMG16_RESET

/* cnc_statinfo */
#define ALARM_NO		ALARM16_NO
#define ALARM_ALARM		ALARM16_ALARM
#define ALARM_BATTERY		ALARM16_BATTERY

/* cnc_statinfo */
#define EDIT_M_NO_EDIT		EDIT16M_NO_EDIT
#define EDIT_M_EDIT		EDIT16M_EDIT
#define EDIT_M_SEARCH		EDIT16M_SEARCH
#define EDIT_M_OUTPUT		EDIT16M_OUTPUT
#define EDIT_M_INPUT		EDIT16M_INPUT
#define EDIT_M_COMPARE		EDIT16M_COMPARE
#define EDIT_M_LABEL_SKIP	EDIT16M_LABEL_SKIP
#define EDIT_M_RESTART		EDIT16M_RESTART
#define EDIT_M_HPCC		EDIT16M_HPCC
#define EDIT_M_PTRR		EDIT16M_PTRR
#define EDIT_M_RVRS		EDIT16M_RVRS
#define EDIT_M_RTRY		EDIT16M_RTRY
#define EDIT_M_RVED		EDIT16M_RVED
#define EDIT_M_HANDLE		EDIT16M_HANDLE
#define EDIT_M_OFFSET		EDIT16M_OFFSET
#define EDIT_M_WORK_OFFSET	EDIT16M_WORK_OFFSET
#define EDIT_M_AICC		EDIT16M_AICC
#define EDIT_M_MEMORY_CHECK	EDIT16M_MEMORY_CHECK
#define EDIT_M_CUSTOMER_BOARD	EDIT16M_CUSTOMER_BOARD
#define EDIT_M_SAVE		EDIT16M_SAVE
#define EDIT_M_AI_NANO		EDIT16M_AI_NANO
#define EDIT_M_AI_APC		EDIT16M_AI_APC
#define EDIT_M_MBL_APC		EDIT16M_MBL_APC
#define EDIT_M_NANO_HP		EDIT16M_NANO_HP
#define EDIT_M_AI_HPCC		EDIT16M_AI_HPCC
#define EDIT_M_5_AXIS		EDIT16M_5_AXIS
#define EDIT_M_LEN		EDIT16M_LEN
#define EDIT_M_RAD		EDIT16M_RAD
#define EDIT_M_WZR		EDIT16M_WZR
#define EDIT_M_TCP		EDIT16M_TCP
#define EDIT_M_TWP		EDIT16M_TWP
#define EDIT_M_TCP_TWP		EDIT16M_TCP_TWP
#define EDIT_M_APC		EDIT16M_APC

/* cnc_statinfo */
#define EDIT_T_NO_EDIT		EDIT16T_NO_EDIT
#define EDIT_T_EDIT		EDIT16T_EDIT
#define EDIT_T_SEARCH		EDIT16T_SEARCH
#define EDIT_T_OUTPUT		EDIT16T_OUTPUT
#define EDIT_T_INPUT		EDIT16T_INPUT
#define EDIT_T_COMPARE		EDIT16T_COMPARE
#define EDIT_T_LABEL_SKIP	EDIT16T_LABEL_SKIP
#define EDIT_T_OFFSET		EDIT16T_OFFSET
#define EDIT_T_WORK_SHIFT	EDIT16T_WORK_SHIFT
#define EDIT_T_RESTART		EDIT16T_RESTART
#define EDIT_T_PTRR		EDIT16T_PTRR
#define EDIT_T_MEMORY_CHECK	EDIT16T_MEMORY_CHECK
#define EDIT_T_SAVE		EDIT16T_SAVE
#define EDIT_T_NANO_HP		EDIT16T_NANO_HP
#define EDIT_T_AI_HPCC		EDIT16T_AI_HPCC
#define EDIT_T_OFSX		EDIT16T_OFSX
#define EDIT_T_OFSZ		EDIT16T_OFSZ
#define EDIT_T_WZR		EDIT16T_WZR
#define EDIT_T_OFSY		EDIT16T_OFSY
#define EDIT_T_TOFS		EDIT16T_TOFS
#define EDIT_T_TCP		EDIT16T_TCP
#define EDIT_T_TWP		EDIT16T_TWP
#define EDIT_T_TCP_TWP		EDIT16T_TCP_TWP
#define EDIT_T_APC		EDIT16T_APC

/* cnc_statinfo */
#define AUTO16W_MDI		0
#define AUTO16W_MEM		1
#define AUTO16W_EDT		3
#define AUTO16W_HAND		4
#define AUTO16W_JOG		5
#define AUTO16W_TAPE		10

/* cnc_statinfo */
#define RUN16W_NOT_READY	0
#define RUN16W_M_READY		1
#define RUN16W_C_START		2
#define RUN16W_F_HOLD		3
#define RUN16W_B_STOP		4

/* cnc_statinfo */
#define MOTION16W_NOTHING	0
#define MOTION16W_CMTN		1
#define MOTION16W_CDWL		2

/* cnc_statinfo */
#define MSTB16W_NON		0
#define MSTB16W_CFIN		1

/* cnc_statinfo */
#define ALARM16W_NO		0
#define ALARM16W_ALARM		1
#define ALARM16W_BATTERY	2

/* cnc_statinfo */
#define EDIT16W_NO_EDIT		0
#define EDIT16W_EDITING		1
#define EDIT16W_SEARCH		2
#define EDIT16W_RESTART		3
#define EDIT16W_RETRACE		4

/* cnc_rdalmmsg */
/* cnc_rdalmmsg2 */
#define ALARM_TYPE_ALL		-1

/* cnc_rdalminfo */
#define ALARM_INFORMATION1	0
#define ALARM_INFORMATION2	1

/* cnc_rdalminfo */
#define TYPE15_BG		0
#define TYPE15_PS		1
#define TYPE15_OH		2
#define TYPE15_SB		3
#define TYPE15_SN		4
#define TYPE15_SW		5
#define TYPE15_OT		6
#define TYPE15_PC		7
#define TYPE15_EX		8
#define TYPE15_SR		10
#define TYPE15_SV		12
#define TYPE15_IO		13
#define TYPE15_PW		14
#define TYPE15_MC		19
#define TYPE15_SP		20

/* cnc_rdalminfo */
#define TYPE16_PS100		0
#define TYPE16_PS000		1
#define TYPE16_PS101		2
#define TYPE16_PS		3
#define TYPE16_OT		4
#define TYPE16_OH		5
#define TYPE16_SV		6
#define TYPE16_APC		8
#define TYPE16_SP		9
#define TYPE16_PS2		10
#define TYPE16_LS		11
#define TYPE16_RIGID		13
#define TYPE16_EX		15

/* cnc_rdalminfo */
#define TYPE16W_PS		0
#define TYPE16W_OT		1
#define TYPE16W_SV		2
#define TYPE16W_OH		4
#define TYPE16W_SL1		5
#define TYPE16W_SL2		6
#define TYPE16W_EDIT		7
#define TYPE16W_APC		8
#define TYPE16W_PS2		10
#define TYPE16W_EX		14
#define TYPE16W_RC		15

/* cnc_modal */
/* cnc_rdcommand */
#define GET_ALL_1SHOT_G		-4
#define GET_ALL_AXES_DATA	-3
#define GET_ALL_OTHER_G		-2
#define GET_ALL_G		-1
#define GET_G_TYPE0		0
#define GET_G_TYPE1		1
#define GET_G_TYPE2		2
#define GET_G_TYPE3		3
#define GET_G_TYPE4		4
#define GET_G_TYPE5		5
#define GET_G_TYPE6		6
#define GET_G_TYPE7		7
#define GET_G_TYPE8		8
#define GET_G_TYPE9		9
#define GET_G_TYPE10		10
#define GET_G_TYPE11		11
#define GET_G_TYPE12		12
#define GET_G_TYPE13		13
#define GET_G_TYPE14		14
#define GET_G_TYPE15		15
#define GET_G_TYPE16		16
#define GET_G_TYPE17		17
#define GET_G_TYPE18		18
#define GET_G_TYPE19		19
#define GET_G_TYPE20		20
#define GET_G_TYPE31		31
#define GET_G_TYPE32		32
#define GET_G_TYPE33		33
#define GET_G_TYPE34		34
#define GET_OTHER_G_TYPE100	100
#define GET_OTHER_G_TYPE101	101
#define GET_OTHER_G_TYPE102	102
#define GET_OTHER_G_TYPE103	103
#define GET_OTHER_G_TYPE104	104
#define GET_OTHER_G_TYPE105	105
#define GET_OTHER_G_TYPE106	106
#define GET_OTHER_G_TYPE107	107
#define GET_OTHER_G_TYPE108	108
#define GET_OTHER_G_TYPE109	109
#define GET_OTHER_G_TYPE110	110
#define GET_OTHER_G_TYPE111	111
#define GET_OTHER_G_TYPE112	112
#define GET_OTHER_G_TYPE113	113
#define GET_OTHER_G_TYPE114	114
#define GET_OTHER_G_TYPE115	115
#define GET_OTHER_G_TYPE116	116
#define GET_OTHER_G_TYPE117	117
#define GET_OTHER_G_TYPE118	118
#define GET_OTHER_G_TYPE119	119
#define GET_OTHER_G_TYPE120	120
#define GET_OTHER_G_TYPE121	121
#define GET_OTHER_G_TYPE122	122
#define GET_OTHER_G_TYPE123	123
#define GET_OTHER_G_TYPE124	124
#define GET_OTHER_G_TYPE125	125
#define GET_OTHER_G_TYPE126	126
#define GET_OTHER_G_TYPE127	127
#define GET_OTHER_G_TYPE128	128
#define GET_OTHER_G_TYPE129	129
#define GET_AXIS_1		200
#define GET_AXIS_2		201
#define GET_AXIS_3		202
#define GET_AXIS_4		203
#define GET_AXIS_5		204
#define GET_AXIS_6		205
#define GET_AXIS_7		206
#define GET_AXIS_8		207
#define GET_AXIS_9		208
#define GET_AXIS_10		209
#define GET_AXIS_11		210
#define GET_AXIS_12		211
#define GET_AXIS_13		212
#define GET_AXIS_14		213
#define GET_AXIS_15		214
#define GET_AXIS_16		215
#define GET_AXIS_17		216
#define GET_AXIS_18		217
#define GET_AXIS_19		218
#define GET_AXIS_20		219
#define GET_AXIS_21		220
#define GET_AXIS_22		221
#define GET_AXIS_23		222
#define GET_AXIS_24		223
#define GET_1SHOT_TYPE300	300
#define GET_1SHOT_TYPE301	301
#define GET_1SHOT_TYPE302	302
#define GET_1SHOT_TYPE303	303

/* cnc_rdgcode */
#define GET_G_TYPE24		24
#define GET_G_TYPE25		25
#define GET_G_TYPE27		27
#define GET_ALL_GCODE		-1
#define GET_1SHOT_TYPE100	100
#define GET_1SHOT_TYPE101	101
#define GET_1SHOT_TYPE102	102
#define GET_1SHOT_TYPE103	103
#define GET_ALL_1SHOT_GCODE	-2

/* cnc_modal */
#define BLOCK15_PREVIOUS	0
#define BLOCK15_ACTIVE		1
#define BLOCK15_NEXT		2
#define BLOCK16_ACTIVE		0
#define BLOCK16_NEXT		1
#define BLOCK16_AFTER_NEXT	2
#define BLOCK16_PREVIOUS	3

/* cnc_rdgcode */
/* cnc_rdcommand */
#define BLOCK_PREVIOUS		0
#define BLOCK_ACTIVE		1
#define BLOCK_NEXT		2

/* cnc_rdcommand */
#define GET_OTHER_G_TYPE0	0
#define GET_OTHER_G_TYPE1	1
#define GET_OTHER_G_TYPE2	2
#define GET_OTHER_G_TYPE3	3
#define GET_OTHER_G_TYPE4	4
#define GET_OTHER_G_TYPE5	5
#define GET_OTHER_G_TYPE6	6
#define GET_OTHER_G_TYPE7	7
#define GET_OTHER_G_TYPE8	8
#define GET_OTHER_G_TYPE9	9
#define GET_OTHER_G_TYPE10	10
#define GET_OTHER_G_TYPE11	11
#define GET_OTHER_G_TYPE12	12
#define GET_OTHER_G_TYPE13	13
#define GET_OTHER_G_TYPE14	14
#define GET_OTHER_G_TYPE15	15
#define GET_OTHER_G_TYPE16	16
#define GET_OTHER_G_TYPE17	17
#define GET_OTHER_G_TYPE18	18
#define GET_OTHER_G_TYPE19	19
#define GET_OTHER_G_TYPE20	20
#define GET_OTHER_G_TYPE21	21
#define GET_OTHER_G_TYPE22	22
#define GET_OTHER_G_TYPE23	23
#define GET_OTHER_G_TYPE24	24
#define GET_OTHER_G_TYPE25	25
#define GET_OTHER_G_TYPE26	26
#define GET_OTHER_G_TYPE27	27
#define GET_OTHER_G_TYPE28	28
#define GET_OTHER_G_TYPE29	29
#define GET_OTHER_ALL_G		-1
#define GET_COMMAND_ALL		-2
#define GET_AXES_ALL_DATA	-3

/* cnc_diagnoss */
/* cnc_diagnosr */
#define DIAG_BYTE		0
#define DIAG_WORD		1
#define DIAG_2WORD		2
#define DIAG_8BIT		3
#define DIAG_1BIT		4
#define DIAG_NOT_USED		4
#define DIAG_REAL		5

/* cnc_adcnv */
#define TYPE15_ANALOG_CN1	0
#define TYPE15_ANALOG_CN2	1
#define TYPE15_ANALOG_SP1	100
#define TYPE15_ANALOG_SP2	101
#define TYPE15_CNC_AXIS		200
#define TYPE16_ANALOG		0
#define TYPE16_CNC_AXIS		2

/* cnc_adcnv */
#define AV_TYPE0		0
#define AV_TYPE1		1
#define AV_TYPE2		2
#define AV_TYPE3		3
#define AV_TYPE4		4
#define AV_TYPE5		5
#define AV_TYPE6		6
#define AV_TYPE7		7
#define AV_TYPE8		8
#define AV_TYPE9		9
#define AV_TYPE10		10
#define AV_TYPE11		11
#define AV_TYPE12		12
#define AV_TYPE13		13
#define AV_TYPE14		14
#define AV_TYPE15		15
#define AV_TYPE16		16
#define AV_TYPE17		17
#define AV_TYPE18		18
#define AV_TYPE19		19
#define AV_TYPE20		20
#define AV_TYPE21		21
#define AV_TYPE22		22
#define AV_TYPE23		23
#define AV_TYPE24		24

/* cnc_rdopmsg */
/* cnc_rdopmsg2 */
/* cnc_rdopmsg3 */
#define OPMSG_NO1		0
#define OPMSG_NO2		1
#define OPMSG_NO3		2
#define OPMSG_NO4		3
#define OPMSG_MACRO		4
#define OPMSG_ALL		-1

/* cnc_rstrseqsrch */
#define TYPE_SEQUENCE		0
#define TYPE_BLOCK		1

/* cnc_rstrseqsrch */
/* cnc_rstrseqsrch2 */
#define TYPE_P			0
#define TYPE_Q			1

/* cnc_rdopnlsgnl */
/* cnc_wropnlsgnl */
#define MODE15_MDI		0
#define MODE15_MEM		1
#define MODE15_EDIT		2
#define MODE15_HND		3
#define MODE15_JOG		4
#define MODE15_REF		5
#define MODE15_TAP		6
#define MODE15_INC		7

/* cnc_rdopnlsgnl */
/* cnc_wropnlsgnl */
#define MODE16_MDI		0
#define MODE16_MEM		1
#define MODE16_EDIT		2
#define MODE16_HNDL		3
#define MODE16_JOG		4
#define MODE16_REF		5

/* cnc_rdopnlsgnl */
/* cnc_wropnlsgnl */
#define HANDLE_HX		0
#define HANDLE_HY		1
#define HANDLE_HZ		2
#define HANDLE_H4		3

/* cnc_rdopnlsgnl */
/* cnc_wropnlsgnl */
#define HANDLE_1		0
#define HANDLE_10		1
#define HANDLE_100		2

/* cnc_rdopnlsgnl */
/* cnc_wropnlsgnl */
#define RAPID_100		0
#define RAPID_50		1
#define RAPID_25		2
#define RAPID_F0		3

/* cnc_rdopnlsgnl */
/* cnc_wropnlsgnl */
#define JOG_0P0			0
#define JOG_0P1			1
#define JOG_0P14		2
#define JOG_0P2			3
#define JOG_0P27		4
#define JOG_0P37		5
#define JOG_0P52		6
#define JOG_0P72		7
#define JOG_1P0			8
#define JOG_1P4			9
#define JOG_2P0			10
#define JOG_2P7			11
#define JOG_3P7			12
#define JOG_5P2			13
#define JOG_7P2			14
#define JOG_10P0		15
#define JOG_14P0		16
#define JOG_20P0		17
#define JOG_27P0		18
#define JOG_37P0		19
#define JOG_52P0		20
#define JOG_72P0		21
#define JOG_100P0		22
#define JOG_140P0		23
#define JOG_200P0		24

/* cnc_rdopnlsgnl */
/* cnc_wropnlsgnl */
#define FEED_0			0
#define FEED_10			1
#define FEED_20			2
#define FEED_30			3
#define FEED_40			4
#define FEED_50			5
#define FEED_60			6
#define FEED_70			7
#define FEED_80			8
#define FEED_90			9
#define FEED_100		10
#define FEED_110		11
#define FEED_120		12
#define FEED_130		13
#define FEED_140		14
#define FEED_150		15
#define FEED_160		16
#define FEED_170		17
#define FEED_180		18
#define FEED_190		19
#define FEED_200		20

/* cnc_rdopnlsgnl */
/* cnc_wropnlsgnl */
#define SPINDLE_0		0
#define SPINDLE_10		1
#define SPINDLE_20		2
#define SPINDLE_30		3
#define SPINDLE_40		4
#define SPINDLE_50		5
#define SPINDLE_60		6
#define SPINDLE_70		7
#define SPINDLE_80		8
#define SPINDLE_90		9
#define SPINDLE_100		10
#define SPINDLE_110		11
#define SPINDLE_120		12
#define SPINDLE_130		13
#define SPINDLE_140		14
#define SPINDLE_150		15
#define SPINDLE_160		16
#define SPINDLE_170		17
#define SPINDLE_180		18
#define SPINDLE_190		19
#define SPINDLE_200		10

/* cnc_rdradofs */
/* cnc_rdlenofs */
/* cnc_rdfixcycle */
/* cnc_rdcdrotate */
/* cnc_rd3dcdcnv */
/* cnc_rdmirimage */
/* cnc_rdscaling */
/* cnc_rd3dtofs */
/* cnc_rdposofs */
#define MODE_CANCEL		0
#define MODE_OFFSET		1
#define MODE_CYCLE		1
#define MODE_ROTATION		1
#define MODE_CONVERSION		1
#define MODE_MIRROR		1
#define MODE_SCALING		1

/* cnc_getfigure */
#define FIG_AXIS		0
#define FIG_TOOL_OFFSET		1
#define FIG_CUSTOM_MACRO	2
#define FIG_WORK_ZERO_OFFSET	3
#define FIG_FEEDRATE		4

/* cnc_getpath */
/* cnc_setpath */
#define PATH_1ST		1
#define PATH_2ND		2
#define PATH_3RD		3
#define PATH_4TH		4
#define PATH_5TH		5
#define PATH_LOADER		5
#define PATH_6TH		6
#define PATH_7TH		7
#define PATH_8TH		8
#define PATH_9TH		9
#define PATH_10TH		10

/* cnc_gettimer */
/* cnc_settimer */
#define TYPE_DATE		0
#define TYPE_TIME		1

/* cnc_clralm */
#define CLEAR_PS100		0
#define CLEAR_PS101		1

/* cnc_rdtimer */
/* cnc_wrtimer */
#define TYPE_POWER_ON		0
#define TYPE_OPERATING		1
#define TYPE_CUTTING		2
#define TYPE_CYCLE		3
#define TYPE_FREE		4

/* cnc_getfrominfo */
#define ATTRIB_SYSTEM		1
#define ATTRIB_USER		2

/* cnc_fromget */
#define GET_READING_END		0
#define GET_READING		1

/* cnc_wractpt */
#define TYPE_ABSOLUTE		0
#define TYPE_RELATIVE		1

/* pmc_rdpmcrng */
/* pmc_wrpmcrng */
/* pmc_rdpmcinfo */
#define ADDRESS_G		0
#define ADDRESS_F		1
#define ADDRESS_Y		2
#define ADDRESS_X		3
#define ADDRESS_A		4
#define ADDRESS_R		5
#define ADDRESS_T		6
#define ADDRESS_K		7
#define ADDRESS_C		8
#define ADDRESS_D		9
#define ADDRESS_M		10
#define ADDRESS_N		11
#define ADDRESS_E		12
#define ADDRESS_ALL		-1

/* pmc_rdpmcrng */
/* pmc_wrpmcrng */
/* pmc_wrcntldata */
#define PMC_BYTE		0
#define PMC_WORD		1
#define PMC_LONG		2

/* pmc_rdalmmsg */
#define PMC_ALARM_NO		-1
#define PMC_ALARM_ALL_READ	0
#define PMC_ALARM_STILL		1

/* pmc_prfrdbusprm */
/* pmc_prfwrbusprm */
#define BAUDRATE_9P6K		0
#define BAUDRATE_19P2K		1
#define BAUDRATE_93P75K		2
#define BAUDRATE_187P5K		3
#define BAUDRATE_500P0K		4
#define BAUDRATE_1P5M		6
#define BAUDRATE_3P0M		7
#define BAUDRATE_6P0M		8
#define BAUDRATE_12P0M		9

/* pmc_prfrdslvprm */
/* pmc_prfwrslvprm */
#define DISABLE			0
#define ENABLE			1

/* pmc_prfrdslvstat */
#define STAT_INITIAL		0xff
#define STAT_NORMAL		0x00
#define STAT_UNUSUAL		0x01

/* pmc_prfrdslvstat */
#define STAT_CONNECTING		0x00
#define STAT_DISCONNECTING	0x01


/* pmc_select_pmc_unit */
/* pmc_get_current_pmc_unit */
/* pmc_get_pmc_unit_types */
#define PMCUNIT_PMC1		1 /* 1st PMC */
#define PMCUNIT_PMC2		2 /* 2nd PMC */
#define PMCUNIT_PMC3		3 /* 3rd PMC */
#define PMCUNIT_DCS			9 /* Dual-check safety PMC */

/* cnc_setcurscrn */
/* cnc_sdfstatchg */
/* POSITION */
#define CRT_POS_ABS		0x0000 /* ABSOLUTE */
#define CRT_POS_REL		0x0100 /* RELATIVE */
#define CRT_POS_ALL		0x0200 /* ALL */
#define CRT_POS_HNDL		0x0300 /* HANDLE INTERRUPT */
#define CRT_POS_MONI		0x0400 /* OPERATING MONITOR */
#define CRT_POS_CEXE		0x0500 /* C Executor */
#define CRT_POS_NCDMY		0x0600 /* for CNC Screen */
#define CRT_POS_CEXE2		0x0800 /* C Executor #2 */
#define CRT_POS_CEXE3		0x0900 /* C Executor #3 */
#define CRT_POS_CEXE4		0x0a00 /* C Executor #4 */
#define CRT_POS_CEXE5		0x0b00 /* C Executor #5 */
#define CRT_POS_CNC		0x3f00 /* Previous CNC screen */
/* PROGRAM */
#define CRT_PRG_MDI		0x0001 /* MDI PROGRAM */
#define CRT_PRG_PRG		0x0101 /* PROGRAM */
#define CRT_PRG_LIB		0x0201 /* LIBRARY */
#define CRT_PRG_CURR		0x0301 /* CURRENT BLOCK */
#define CRT_PRG_NEXT		0x0401 /* NEXT BLOCK */
#define CRT_PRG_PCHK		0x0501 /* PROGRAM CHECK */
#define CRT_PRG_REST		0x0601 /* PROGRAM RESTART */
#define CRT_PRG_FDIR		0x0701 /* FLOPPY DIRECTORY */
#define CRT_PRG_CAP		0x0801 /* C.A.P. */
#define CRT_PRG_SCDL		0x0901 /* SCHEDULE */
#define CRT_PRG_CYTM		0x0a01 /* CYCLE TIME */
#define CRT_PRG_DSDIR		0x0b01 /* DATA SERVER DIRECTORY */
#define CRT_PRG_JOG		0x0c01 /* JOG */
#define CRT_PRG_CEXE		0x0d01 /* C Executor */
#define CRT_PRG_DSDIRD		0x0e01 /* DS-DIR HDD */
#define CRT_PRG_DSDIRH		0x0f01 /* DS-DIR HOST */
#define CRT_PRG_HOST		0x1001 /* HOST CONNECT */
#define CRT_PRG_MDNC		0x1101 /* MEM-CARD DNC OPERATION */
#define CRT_PRG_CEXE2		0x1201 /* C Executor #2 */
#define CRT_PRG_CEXE3		0x1301 /* C Executor #3 */
#define CRT_PRG_CEXE4		0x1401 /* C Executor #4 */
#define CRT_PRG_CEXE5		0x1501 /* C Executor #5 */
#define CRT_PRG_CNC		0x3f01 /* Previous CNC screen */
/* OFFSET */
#define CRT_OFS_OFS		0x0002 /* OFFSET (GEOM,WEAR) */
#define CRT_OFS_SETP		0x0102 /* SETTING PARAMETER */
#define CRT_OFS_WORK		0x0202 /* WORK COORDINATE */
#define CRT_OFS_MCR		0x0302 /* MACRO VARIABLE */
#define CRT_OFS_MENU		0x0402 /* MENU */
#define CRT_OFS_OPR		0x0502 /* OPERATOR'S PANEL */
#define CRT_OFS_TLLF		0x0602 /* TOOL LIFE */
#define CRT_OFS_MODEM		0x1002 /* MODEM MONITOR/SETTING */
#define CRT_OFS_CHOP		0x0702 /* [M] CHOPPING PARAMETER */
#define CRT_OFS_TLMB		0x0802 /* [M] TOOL LENGTH MES.-B */
#define CRT_OFS_INTC		0x0902 /* [M] INTERFERENCE CHECK */
#define CRT_OFS_MCEXE		0x0a02 /* [M] C Executor */
#define CRT_OFS_FXOFA		0x1102 /* [M] FIXTURE OFFSET (ACT) */
#define CRT_OFS_FXOFO		0x1202 /* [M] FIXTURE OFFSET (OFS) */
#define CRT_OFS_MCEXE2		0x1302 /* [M] C Executor #2 */
#define CRT_OFS_MCEXE3		0x1402 /* [M] C Executor #3 */
#define CRT_OFS_MCEXE4		0x1502 /* [M] C Executor #4 */
#define CRT_OFS_MCEXE5		0x1602 /* [M] C Executor #5 */
#define CRT_OFS_WSFT		0x0702 /* [T] WORK SHIFT */
#define CRT_OFS_YOFS		0x0802 /* [T] Y-AXIS OFFSET (GEOM,WEAR) */
#define CRT_OFS_TLFM		0x0902 /* [T] TOOL FORM */
#define CRT_OFS_BARR		0x0a02 /* [T] BARRIER */
#define CRT_OFS_BOFS		0x0b02 /* [T] B-AXIS OFFSET */
#define CRT_OFS_GEOM2		0x0c02 /* [T] 2ND OFFSET GEOMETORY */
#define CRT_OFS_TCEXE		0x0d02 /* [T] C Executor */
#define CRT_OFS_TCEXE2		0x1302 /* [T] C Executor #2 */
#define CRT_OFS_TCEXE3		0x1402 /* [T] C Executor #3 */
#define CRT_OFS_TCEXE4		0x1502 /* [T] C Executor #4 */
#define CRT_OFS_TCEXE5		0x1602 /* [T] C Executor #5 */
#define CRT_OFS_CNC		0x3f02 /* Previous CNC screen */
/* SYSTEM */
#define CRT_SYS_PRM		0x0003 /* PARAMETER */
#define CRT_SYS_DGN		0x0103 /* DIAGNOSE */
#define CRT_SYS_PMC		0x0203 /* PMC */
#define CRT_SYS_SYS		0x0303 /* SYSTEM */
#define CRT_SYS_MEM		0x0403 /* MEMORY */
#define CRT_SYS_PIT		0x0503 /* PITCH ERROR */
#define CRT_SYS_SVPR		0x0603 /* SERVO SETTING */
#define CRT_SYS_SPPR		0x0703 /* SPINDLE SETTING */
#define CRT_SYS_MAP		0x0803 /* MAP */
#define CRT_SYS_CSRV		0x0803 /* [i] C-SERV */
#define CRT_SYS_WANL		0x0903 /* WAVE ANALYZER */
#define CRT_SYS_MCOD		0x0a03 /* M-CODE GRP SETTING */
#define CRT_SYS_OHIS		0x0b03 /* OPERATION HISTORY */
#define CRT_SYS_ALIO		0x0c03 /* ALL I/O */
#define CRT_SYS_NMAP		0x0d03 /* MAP */
#define CRT_SYS_COPR		0x0d03 /* C-OPER */
#define CRT_SYS_DSSET		0x0e03 /* DATA SERVER SETTING */
#define CRT_SYS_DSMNT		0x0f03 /* DATA SERVER MAINTENANCE */
#define CRT_SYS_DSMOD		0x1003 /* DATA SERVER MODE STORAGE. */
#define CRT_SYS_SERCS		0x1103 /* SERCOS INTERFACE DISPLAY */
#define CRT_SYS_COLOR		0x1203 /* VGA COLOR SETTING */
#define CRT_SYS_MAINT		0x1303 /* PERIODICAL MAINTENANCE DISP.*/
#define CRT_SYS_MINFO		0x1403 /* MAINTENANCE INFORMATION DISP. */
#define CRT_SYS_PMM		0x1503 /* PMM */
#define CRT_SYS_TMCHK		0x1603 /* PROGRAM TAPE MEMORY CHECK DISP. */
#define CRT_SYS_TPNL		0x1703 /* TOUCH PANEL CALIBRATION DISP. */
#define CRT_SYS_FSSB		0x1803 /* FSSB SETTING DISPLAY */
#define CRT_SYS_CEXE		0x1903 /* C Executor */
#define CRT_SYS_TRQPR		0x1a03 /* FINE TORQUE SENSING (PARAMETER) */
#define CRT_SYS_TRQMN		0x1b03 /* FINE TORQUE SENSING (MONITER) */
#define CRT_SYS_ETHPRM		0x1c03 /* ETHERNET PARAMETER */
#define CRT_SYS_CEXE2		0x1d03 /* C Executor #2 */
#define CRT_SYS_CEXE3		0x1e03 /* C Executor #3 */
#define CRT_SYS_CEXE4		0x1f03 /* C Executor #4 */
#define CRT_SYS_CEXE5		0x2003 /* C Executor #5 */
#define CRT_SYS_CNC		0x3f03 /* Previous CNC screen */
/* MESSAGE */
#define CRT_MSG_ALM		0x0004 /* ALARM */
#define CRT_MSG_EMSG		0x0104 /* EXTERNAL MESSAGE */
#define CRT_MSG_AHIS		0x0204 /* ALARM HISTORY */
#define CRT_MSG_MMSG		0x0304 /* MAP MESSAGE */
#define CRT_MSG_OMSG		0x0404 /* OPERATOR'S MESSAGE */
#define CRT_MSG_RMT		0x0504 /* REMOTE DIAGNOSTICS */
#define CRT_MSG_MHIS		0x0604 /* EXTERNAL/OPERATOR'S MESSAGE HISTORY */
#define CRT_MSG_NET		0x0704 /* NETWORK MESSAGE */
#define CRT_MSG_DSM		0x0804 /* DATA SERVER MESSAGE */
#define CRT_MSG_OCS		0x0904 /* ONLINE CUSTOM SCREEN */
#define CRT_MSG_CEXE		0x0a04 /* C Executor */
#define CRT_MSG_SAHIS		0x0b04 /* SYSTEM ALARM HISTORY */
#define CRT_MSG_ETHLOG		0x0c04 /* ETHERNET LOG SCREEN */
#define CRT_MSG_CEXE2		0x1004 /* C Executor #2 */
#define CRT_MSG_CEXE3		0x1104 /* C Executor #3 */
#define CRT_MSG_CEXE4		0x1204 /* C Executor #4 */
#define CRT_MSG_CEXE5		0x1304 /* C Executor #5 */
#define CRT_MSG_CNC		0x3f04 /* Previous CNC screen */
/* USER (small key) */
#define CRT_USR_PAS		0x0005 /* PASCAL */
#define CRT_USR_AUX		0x0105 /* AUX */
#define CRT_USR_MCR		0x0205 /* MACRO */
#define CRT_USR_MENU		0x0305 /* MENU */
#define CRT_USR_GRPH		0x0405 /* GRAPHIC */
#define CRT_USR_SGRP		0x0505 /* [M] SOLID GRAPHIC */
#define CRT_USR_CNC		0x3f05 /* Previous CNC screen */
/* GRAPHIC (full key) */
#define CRT_GRP_GRPH		0x0005 /* GRAPHIC */
#define CRT_GRP_SGRP		0x0105 /* [M] SOLID GRAPHIC */
#define CRT_GRP_CNC		0x3f05 /* Previous CNC screen */
/* CUSTOM (full key) */
#define CRT_CST_PAS		0x0006 /* PASCAL */
#define CRT_CST_AUX		0x0106 /* AUX */
#define CRT_CST_MCR		0x0206 /* MACRO */
#define CRT_CST_MENU		0x0306 /* MENU */
#define CRT_CST_CNC		0x3f06 /* Previous CNC screen */
/* FAPT */
#define CRT_FAPT		0x0007 /* FAPT */

/* cnc_sdfstatchg(for 30i/31i/32i)*/
/* POSITION */
#define CRT30I_POS_ABS		0x0100 /* ABS */
#define CRT30I_POS_REL		0x0200 /* REL */
#define CRT30I_POS_ALL		0x0300 /* ALL */
#define CRT30I_POS_HNDL		0x0400 /* HNDL */
#define CRT30I_POS_MONI		0x0600 /* MONITOR */
#define CRT30I_POS_5AX		0x0700 /* 5AXMAN */
#define CRT30I_POS_CEXE		0x3200 /* C Executor */
#define CRT30I_POS_CEXE2	0x3300 /* C Executor 2 */
#define CRT30I_POS_CEXE3	0x3400 /* C Executor 3 */
#define CRT30I_POS_CEXE4	0x3500 /* C Executor 4 */
#define CRT30I_POS_CEXE5	0x3600 /* C Executor 5 */
#define CRT30I_POS_PREV		0x3f00 /* Previous CNC screen */
/* PROGRAM */
#define CRT30I_PRG_PRG		0x0101 /* PROG */
#define CRT30I_PRG_LIB		0x0201 /* FOLDER */
#define CRT30I_PRG_NEXT		0x0301 /* NEXT */
#define CRT30I_PRG_PCHK		0x0401 /* CHECK */
#define CRT30I_PRG_TIME_STAMP	0x0601 /* STAMP */
#define CRT30I_PRG_JOG		0x0701 /* JOG */
#define CRT30I_PRG_REST		0x0801 /* RSTR */
#define CRT30I_PRG_CEXE		0x3201 /* C Executor */
#define CRT30I_PRG_CEXE2	0x3301 /* C Executor 2 */
#define CRT30I_PRG_CEXE3	0x3401 /* C Executor 3 */
#define CRT30I_PRG_CEXE4	0x3501 /* C Executor 4 */
#define CRT30I_PRG_CEXE5	0x3601 /* C Executor 5 */
#define CRT30I_PRG_PREV		0x3f01 /* Previous CNC screen */
/* OFFSET */
#define CRT30I_OFS_OFS		0x0102 /* OFFSET */
#define CRT30I_OFS_SETP		0x0202 /* SETTING */
#define CRT30I_OFS_WORK		0x0302 /* WORK */
#define CRT30I_OFS_MCR		0x0602 /* MACRO */
#define CRT30I_OFS_OPR		0x0802 /* OPR */
#define CRT30I_OFS_TOOLMNG	0x0902 /* TOOL MANAGER */
#define CRT30I_OFS_YOFS		0x0b02 /* OFST.2 */
#define CRT30I_OFS_WSFT		0x0c02 /* W.SHFT */
#define CRT30I_OFS_GEOM2	0x0d02 /* GEOM.2 */
#define CRT30I_OFS_TLFM		0x0e02 /* TOOL.F */
#define CRT30I_OFS_MODEM	0x1002 /* MODEM */
#define CRT30I_OFS_PRELEV	0x1102 /* PR-LEV */
#define CRT30I_OFS_CHOP		0x1302 /* CHOP */
#define CRT30I_OFS_CHUCK	0x1502 /* CHUCK TAIL */
#define CRT30I_OFS_LANG		0x1602 /* LANG. */
#define CRT30I_OFS_PROTECT	0x1702 /* PROTECT */
#define CRT30I_OFS_SAFEGUARD	0x1802 /* GUARD */
#define CRT30I_OFS_ACT_FIXTURE	0x1a02 /* ACTFIX */
#define CRT30I_OFS_RTTDYN_FIXTURE	0x1b02 /* RTTFIX */
#define CRT30I_OFS_CEXE		0x3202 /* C Executor */
#define CRT30I_OFS_CEXE2	0x3302 /* C Executor 2 */
#define CRT30I_OFS_CEXE3	0x3402 /* C Executor 3 */
#define CRT30I_OFS_CEXE4	0x3502 /* C Executor 4 */
#define CRT30I_OFS_CEXE5	0x3602 /* C Executor 5 */
#define CRT30I_OFS_PREV		0x3f02 /* Previous CNC screen */
/* SYSTEM */
#define CRT30I_SYS_PRM		0x0103 /* PARAM */
#define CRT30I_SYS_DGN		0x0203 /* DGNOS */
#define CRT30I_SYS_SRVGUID	0x0303 /* SERVO GUIDEM */
#define CRT30I_SYS_SYS		0x0403 /* SYSTEM */
#define CRT30I_SYS_MEM		0x0603 /* MEMORY */
#define CRT30I_SYS_PIT		0x0703 /* PITCH */
#define CRT30I_SYS_SVPR		0x0803 /* SV-PRM */
#define CRT30I_SYS_SPPR		0x0903 /* SP.SET */
#define CRT30I_SYS_MAINTENANCE	0x0b03 /* PMC MAINTE */
#define CRT30I_SYS_LADDER	0x0c03 /* PMC LADDER */
#define CRT30I_SYS_PMCCONFIG	0x0d03 /* PMC CONFIG */
#define CRT30I_SYS_MTUNE	0x1003 /* M-TUN */
#define CRT30I_SYS_ALIO		0x1103 /* ALL IO */
#define CRT30I_SYS_ALIO2	0x1203 /* ALL IO */
#define CRT30I_SYS_OHIS		0x1303 /* OPEHIS */
#define CRT30I_SYS_COLOR	0x1503 /* COLOR */
#define CRT30I_SYS_MAINT	0x1603 /* MAINTE */
#define CRT30I_SYS_MINFO	0x1703 /* M-INFO */
#define CRT30I_SYS_WANL		0x1803 /* W.DGNS */
#define CRT30I_SYS_TOUCH	0x1a03 /* TOUCH */
#define CRT30I_SYS_FSSB		0x1b03 /* FSSB */
#define CRT30I_SYS_PARAMTUNE	0x1c03 /* PRMTUN */
#define CRT30I_SYS_PMM		0x1d03 /* PMM */
#define CRT30I_SYS_EMB_ETH	0x1f03 /* EMBED PORT */
#define CRT30I_SYS_PCM_ETH	0x2003 /* PCMCIA LAN */
#define CRT30I_SYS_BOARD_ETH	0x2103 /* ETHER BOARD */
#define CRT30I_SYS_MAS_PROFI	0x2203 /* PROFI MASTER */
#define CRT30I_SYS_REMOTEDGN	0x2403 /* RMTDGN */
#define CRT30I_SYS_MCOD		0x2503 /* M CODE */
#define CRT30I_SYS_LEARN_CTRL	0x2603 /* LEARNCTRL */
#define CRT30I_SYS_ADJUST3D	0x2703 /* ADJUST3D */
#define CRT30I_SYS_CEXE		0x3203 /* C Executor */
#define CRT30I_SYS_CEXE2	0x3303 /* C Executor 2 */
#define CRT30I_SYS_CEXE3	0x3403 /* C Executor 3 */
#define CRT30I_SYS_CEXE4	0x3503 /* C Executor 4 */
#define CRT30I_SYS_CEXE5	0x3603 /* C Executor 5 */
#define CRT30I_SYS_PREV		0x3f03 /* Previous CNC screen */
/* MESSAGE */
#define CRT30I_MSG_ALM		0x0104 /* ALARM */
#define CRT30I_MSG_EMSG		0x0204 /* MSG */
#define CRT30I_MSG_AHIS		0x0304 /* HISTRY */
#define CRT30I_MSG_MHIS		0x0404 /* MSGHIS */
#define CRT30I_MSG_EMB_ETHLOG	0x0604 /* EMBED LOG */
#define CRT30I_MSG_PCM_ETHLOG	0x0704 /* PCMCIA LOG */
#define CRT30I_MSG_BOARD_ETHLOG	0x0804 /* BOARD LOG */
#define CRT30I_MSG_CEXE		0x3204 /* C Executor */
#define CRT30I_MSG_CEXE2	0x3304 /* C Executor 2 */
#define CRT30I_MSG_CEXE3	0x3404 /* C Executor 3 */
#define CRT30I_MSG_CEXE4	0x3504 /* C Executor 4 */
#define CRT30I_MSG_CEXE5	0x3604 /* C Executor 5 */
#define CRT30I_MSG_PREV		0x3f04 /* Previous CNC screen */
/* GRAPHIC */
#define CRT30I_GRH_PRM		0x0105 /* PARAMETER */
#define CRT30I_GRH_GRP		0x0205 /* GRAPH */
#define CRT30I_GRH_CEXE		0x3205 /* C Executor */
#define CRT30I_GRH_CEXE2	0x3305 /* C Executor 2 */
#define CRT30I_GRH_CEXE3	0x3405 /* C Executor 3 */
#define CRT30I_GRH_CEXE4	0x3505 /* C Executor 4 */
#define CRT30I_GRH_CEXE5	0x3605 /* C Executor 5 */
#define CRT30I_GRH_PREV		0x3f05 /* Previous CNC screen */
/* CUSTOM */
#define CRT30I_CUS_TMAC1	0x0106 /* TMAC1 */
#define CRT30I_CUS_TMAC2	0x0206 /* TMAC2 */
#define CRT30I_CUS_TMAC3	0x0306 /* TMAC3 */
#define CRT30I_CUS_CEXE		0x3206 /* C Executor */
#define CRT30I_CUS_CEXE2	0x3306 /* C Executor 2 */
#define CRT30I_CUS_CEXE3	0x3406 /* C Executor 3 */
#define CRT30I_CUS_CEXE4	0x3506 /* C Executor 4 */
#define CRT30I_CUS_CEXE5	0x3606 /* C Executor 5 */
#define CRT30I_CUS_PREV		0x3f06 /* Previous CNC screen */
/* CUSTOM2 */
#define CRT30I_CUS2_CEXE	0x3207 /* C Executor */
#define CRT30I_CUS2_CEXE2	0x3307 /* C Executor 2 */
#define CRT30I_CUS2_CEXE3	0x3407 /* C Executor 3 */
#define CRT30I_CUS2_CEXE4	0x3507 /* C Executor 4 */
#define CRT30I_CUS2_CEXE5	0x3607 /* C Executor 5 */
#define CRT30I_CUS2_PREV	0x3f07 /* Previous CNC screen */

/* cnc_rdcdslctprm */
#define APC_PRM_VELOCITY	1
#define APC_PRM_PRECISION	2
#define APC_PRM_AUTOSET		3
#define HPPC_PRM_VELOCITY	11
#define HPPC_PRM_PRECISION	12
#define HPPC_PRM_AUTOSET	13

/* cnc_cdautoset */
#define APC		0
#define HPPC		1

/* cnc_cdautoset */
#define LEVEL_1		1
#define LEVEL_2		2
#define LEVEL_3		3
#define LEVEL_4		4
#define LEVEL_5		5
#define LEVEL_6		6
#define LEVEL_7		7
#define LEVEL_8		8
#define LEVEL_9		9
#define LEVEL_10	10

#ifdef __cplusplus
}
#endif

#endif  /* _INC_FWSYMBOL */
