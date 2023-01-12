/*---------------------------------------------------------------------------*/
/*																			 */
/*	FCA32.H 																 */
/*																			 */
/*		Serial/FCA Library function declarations, constant definitions		 */
/*																			 */
/*---------------------------------------------------------------------------*/
/*	Copyright (C) 1996-2011 by FANUC CORPORATION All rights reserved.		 */
/*---------------------------------------------------------------------------*/

#ifndef _FCA32_
#define _FCA32_

#ifdef _FCADLL_
#define FCAAPI __declspec(dllexport)
#else
#define FCAAPI __declspec(dllimport)
#endif

#ifdef __cplusplus
extern "C" {
#endif

/*----------------------*/
/* constant definitions */
/*----------------------*/
/* for rs_open, fca_setparam (ser_t) */
#define FCA_BAUD_1200	1200
#define FCA_BAUD_2400	2400
#define FCA_BAUD_4800	4800
#define FCA_BAUD_9600	9600
#define FCA_BAUD_19200	19200
#define FCA_BAUD_38400	38400
#define FCA_BAUD_57600	57600
#define FCA_BAUD_76800	76800

#define DATA_7		7
#define DATA_8		8

#define PARITY_N	0
#define PARITY_E	1
#define PARITY_O	2

#define STOP_1		1
#define STOP_2		2

/* for rs_buffer */
#define RS_CHK_BUF_R	0
#define RS_CHK_BUF_W	1
#define RS_CLR_BUF_R	2
#define RS_CLR_BUF_W	3
#define RS_GET_BUF_R	4
#define RS_GET_BUF_W	5

/* for rs_status */
#define STS_CD_ON		0x0002
#define STS_PARITY		0x0008
#define STS_OVERRUN 	0x0010
#define STS_FRAME		0x0020
#define STS_DR_ON		0x0080
#define STS_OVERFLOW	0x0100
#define STS_EMPTY		0x0200
#define STS_R_STOP		0x0800
#define STS_BUF_FULL	0x2000
#define STS_S_STOP		0x8000

/*--------------------*/
/* struct definitions */
/*--------------------*/
/* for rs_open, fca_setparam */
typedef struct PortDefUser {
	int baud; 		/* baud rate at which running	*/
	int stop_bit; 	/* stop bit(s)					*/
	int parity;		/* parity bit(s)				*/
	int data_bit; 	/* Number of bits/byte			*/
	int hardflow; 	/* hardware flow control		*/
	int dc_enable;	/* DC code flow control 		*/
	int dc_put;		/* output DC code on open/close */
	int dc1_code; 	/* DC1 code number				*/
	int dc2_code; 	/* DC2 code number				*/
	int dc3_code; 	/* DC3 code number				*/
	int dc4_code; 	/* DC4 code number				*/
} ser_t;

/* for fca_readdir */
typedef struct FCA_DIRINFO {
	char file_name[18]; /* file name (ASCZ string)			*/
						/*	 first char. : 0xFF - not used	*/
						/*				 : 0x00 - deleted	*/
	char file_size[9];	/* file size (ASCZ string)			*/
	char wrt_protect;	/* write protect					*/
						/*	'P' - on						*/
						/*	' ' - off						*/
	char record_code;	/* recording code					*/
						/*	'B' - binary					*/
						/*	'E' - EIA						*/
						/*	' ' - ISO						*/
	char vol_no[3]; 	/* volume number (ASCZ string)		*/
						/*	"  " - single volume			*/
	char multi_vol; 	/* multi volume 					*/
						/*	' ' - single volume 			*/
						/*	'C' - continuous volume 		*/
						/*	'L' - last volume				*/
} fca_dir;

/*-----------------------*/
/* function declarations */
/*-----------------------*/
FCAAPI int WINAPI rs_open(int port, ser_t *param, char *mode);
FCAAPI int WINAPI rs_close(int port);
FCAAPI int WINAPI rs_putc(int c, int port);
FCAAPI int WINAPI rs_write(char *buffer, int size, int port);
FCAAPI int WINAPI rs_getc(int port);
FCAAPI int WINAPI rs_read(char *buffer, int size, int port);
FCAAPI int WINAPI rs_buffer(int port, int cmnd);
FCAAPI int WINAPI rs_status(int port);

FCAAPI int WINAPI fca_setparam(int port, ser_t *param);
FCAAPI int WINAPI fca_bye(int port);
FCAAPI int WINAPI fca_open(char *name, int mode);
FCAAPI int WINAPI fca_close(void);
FCAAPI int WINAPI fca_read(char *buffer, int bytes);
FCAAPI int WINAPI fca_write(char *buffer, int bytes);
FCAAPI int WINAPI fca_fopen(char *name, char *mode);
FCAAPI int WINAPI fca_fclose(void);
FCAAPI int WINAPI fca_getc(void);
FCAAPI int WINAPI fca_putc(int c);
FCAAPI int WINAPI fca_readdir(fca_dir *buffer, int ndir, int nfile);
FCAAPI int WINAPI fca_rename(char *oldname, char *newname);
FCAAPI int WINAPI fca_delete(char *name);
FCAAPI int WINAPI fca_remains(long *remains);
FCAAPI int WINAPI fca_status(char *buffer);

#ifdef __cplusplus
}
#endif

#endif	/* _FCA32_ */

