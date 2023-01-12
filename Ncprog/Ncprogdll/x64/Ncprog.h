#pragma once

#ifdef __cplusplus
extern "C" {
#endif

#ifdef NCPROG_EXPORTS
#define NCPROG_API __declspec(dllexport)
#else
#define NCPROG_API __declspec(dllimport)
#endif

typedef struct bininfo {
	long used_page;
	long all_page;
	long used_dir;
	long all_dir;
} BININFO;

#define	FINDINFO_NAME_SIZE			36

typedef struct nameinfo {
	unsigned long type;
	char          name[FINDINFO_NAME_SIZE];
	unsigned long size;
	unsigned long block;
	struct{
		short year;
		short month;
		short day;
		short hour;
		short minute;
		short second;
	} update;
} NAMEINFO;


/* Ncprog err code */
typedef enum NCPROG_ERROR_CODE
{
	RET_OK,							// NORMAL
	RET_WRONG_WINPATH,				// 01
	RET_WRONG_FILESIZE,				// 02
	RET_WRONG_OPENMODE,				// 03
	RET_WRONG_GETCRNTMODE,			// 04
	RET_WRONG_WRITELENGTH,			// 05
	RET_WRONG_WRITESIZE,			// 06
	RET_WRONG_PROGPOINTER,			// 07
	RET_ERR08,
	RET_ERR09,
	RET_ERR10,
	RET_ERR11,
	RET_ERR12,
	RET_ERR13,
	RET_ERR14,
	RET_FAIL_FILEOPEN,				// 15
	RET_ERR16,
	RET_NON_OPENFILE,				// 17
	RET_NON_OPENRWMODE,				// 18
	RET_UNDER_OPENFILE,				// 19
	RET_NOSPACE_DISK,				// 20
	RET_UNDER_MAKEFILE,				// 21
	RET_NOTEXIST_FLDRPROG,			// 22
	RET_REGISTERED_PROG,			// 23
	RET_UNJUST_LETTER,				// 24
	RET_EXCEEDED_ENROLLMENT,		// 25
	RET_UNDER_OPENPROG,				// 26
	RET_EXCEEDED_HIERARCHY,			// 27
	RET_UNDER_OPENPROGINFOLDER,		// 28
	RET_NOTINFO_SEARCH,				// 29
	RET_NOTINFO_FLDRPROG,			// 30
	RET_UNJUST_HANDLE,				// 31
	RET_OUTRANGE_POSITION,			// 32
	RET_UNJUST_WRITE,				// 33
	RET_NOSPACE_MEMORY,				// 34
	RET_WRONG_SENTENCE,				// 35
	RET_NOTEXIST_ASYNC,				// 36
	RET_FAIL_ASYNC,					// 37
	RET_CANCEL_ASYNC,				// 38
	RET_OPERATION_NC,				// 39
	RET_EXCEEDED_OPENPROG,			// 40
	RET_NOTTOP_PROGPOINT,			// 41
	RET_BROKEN_PROG,				// 42
	RET_FAIL_CTRLFILE,				// 43
	RET_FAIL_CTRLSHM,				// 44
	RET_EXCEEDED_MOUNTFILE,			// 45
	RET_UNDER_EXCLUSION				// 46
}stNcprogErr;

/****************************************/
/*	function definition					*/
/****************************************/
NCPROG_API int fbin_create(const char*, int);
NCPROG_API int fbin_open(const char*, int);
NCPROG_API int fbin_close(void);
NCPROG_API int fbin_expansion(const char*, int);
NCPROG_API int fbin_get_current_folder(int, char*);
NCPROG_API int fbin_set_current_folder(const char*);
NCPROG_API int fbin_create_program(const char*);
NCPROG_API int fbin_delete_program(const char*);
NCPROG_API int fbin_rename_program(const char*, const char*);
NCPROG_API int fbin_create_folder(const char*);
NCPROG_API int fbin_delete_folder(const char*);
NCPROG_API int fbin_rename_folder(const char*, const char*);
NCPROG_API int fbin_information(BININFO*);
NCPROG_API int fbin_findfirst(const char*, NAMEINFO*, void**);
NCPROG_API int fbin_findnext(void*, NAMEINFO*);
NCPROG_API int fbin_findclose(void*);
NCPROG_API int fbin_open_program(const char*, void**);
NCPROG_API int fbin_close_program(void*);
NCPROG_API int fbin_seek(void*, long, int);
NCPROG_API int fbin_tell(void*, long*);
NCPROG_API int fbin_tell2(void*, unsigned long*);
NCPROG_API int fbin_read(void*, char*, long*);
NCPROG_API int fbin_write (void*, const char*, long*);
NCPROG_API int fbin_write2(void*, long, const char*, long*);
NCPROG_API int fbin_get_program_entry(void*, int*);
NCPROG_API int fbin_condense(const char*);
NCPROG_API int fbin_get_progress(int*, int*);
NCPROG_API int fbin_get_state(int*, int*);
NCPROG_API int fbin_cancel(void);
NCPROG_API int fbin_flush_program(void*);

#ifdef __cplusplus
}
#endif
