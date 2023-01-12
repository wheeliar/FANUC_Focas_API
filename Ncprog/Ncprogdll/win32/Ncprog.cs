/*-------------------------------------------------------------------*/
/* Ncprog.cs                                                         */
/*                                                                   */
/* PANEL i Program operation on large capacity memory DLL C# Wrapper */
/*                                                                   */
/* Copyright (C) 2014 by FANUC CORPORATION All rights reserved.      */
/*                                                                   */
/*-------------------------------------------------------------------*/

using System;   
using System.Runtime.InteropServices;
using System.Text;

class Ncprog
{

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public class BININFO
    {
        public int used_page;           /* Used Page */
        public int all_page;            /* All Page */
        public int used_dir;            /* Used Directory */
        public int all_dir;             /* All Directory */
    }

    public const short FINDINFO_NAME_SIZE = (36);

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public class NAMEINFO_UPDATE
    {
        public short year;              /* Year */
        public short month;             /* Month */
        public short day;               /* Day */
        public short hour;              /* Hour */
        public short minute;            /* Minutes */
        public short second;            /* Second */
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 8)]
    public class NAMEINFO
    {
        public uint type;               /* Data Type */
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = FINDINFO_NAME_SIZE)]
        public string name = new string(' ', FINDINFO_NAME_SIZE);
        public uint size;               /* Program Size(Kbyte) */
        public uint block;              /* Program Block Num */

        public NAMEINFO_UPDATE update = new NAMEINFO_UPDATE();
    }

    /****************************************/
    /*	function definition					*/
    /****************************************/
    [DllImport("Ncprog.dll", EntryPoint="fbin_create", CharSet = CharSet.Ansi)]
    public static extern int fbin_create([In , MarshalAs(UnmanagedType.LPStr)]string path, int size);

    [DllImport("Ncprog.dll", EntryPoint="fbin_open", CharSet = CharSet.Ansi)]
    public static extern int fbin_open([In, MarshalAs(UnmanagedType.LPStr)]string path, int mode);

    [DllImport("Ncprog.dll", EntryPoint="fbin_close")]
    public static extern int fbin_close();

    [DllImport("Ncprog.dll", EntryPoint="fbin_expansion", CharSet = CharSet.Ansi)]
    public static extern int fbin_expansion([In, MarshalAs(UnmanagedType.LPStr)]string path, int size);

    [DllImport("Ncprog.dll", EntryPoint="fbin_get_current_folder", CharSet = CharSet.Ansi)]
    public static extern int fbin_get_current_folder(int type, StringBuilder name);

    [DllImport("Ncprog.dll", EntryPoint="fbin_set_current_folder", CharSet = CharSet.Ansi)]
    public static extern int fbin_set_current_folder([In, MarshalAs(UnmanagedType.LPStr)]string name);

    [DllImport("Ncprog.dll", EntryPoint="fbin_create_program", CharSet = CharSet.Ansi)]
    public static extern int fbin_create_program([In, MarshalAs(UnmanagedType.LPStr)]string name);

    [DllImport("Ncprog.dll", EntryPoint="fbin_delete_program", CharSet = CharSet.Ansi)]
    public static extern int fbin_delete_program([In, MarshalAs(UnmanagedType.LPStr)]string name);

    [DllImport("Ncprog.dll", EntryPoint="fbin_rename_program", CharSet = CharSet.Ansi)]
    public static extern int fbin_rename_program([In, MarshalAs(UnmanagedType.LPStr)]string src_name, [In, MarshalAs(UnmanagedType.LPStr)]string dist_name);

    [DllImport("Ncprog.dll", EntryPoint="fbin_create_folder", CharSet = CharSet.Ansi)]
    public static extern int fbin_create_folder([In, MarshalAs(UnmanagedType.LPStr)]string name);

    [DllImport("Ncprog.dll", EntryPoint="fbin_delete_folder", CharSet = CharSet.Ansi)]
    public static extern int fbin_delete_folder([In, MarshalAs(UnmanagedType.LPStr)]string name);

    [DllImport("Ncprog.dll", EntryPoint="fbin_rename_folder", CharSet = CharSet.Ansi)]
    public static extern int fbin_rename_folder([In, MarshalAs(UnmanagedType.LPStr)]string src_name, [In, MarshalAs(UnmanagedType.LPStr)]string dist_name);

    [DllImport("Ncprog.dll", EntryPoint="fbin_information")]
    public static extern int fbin_information([Out, MarshalAs(UnmanagedType.LPStruct)]BININFO info);

    [DllImport("Ncprog.dll", EntryPoint="fbin_findfirst", CharSet = CharSet.Ansi)]
    public static extern int fbin_findfirst([In, MarshalAs(UnmanagedType.LPStr)]string name, [Out, MarshalAs(UnmanagedType.LPStruct)]NAMEINFO name_info, out IntPtr handle);

    [DllImport("Ncprog.dll", EntryPoint="fbin_findnext", CharSet = CharSet.Ansi)]
    public static extern int fbin_findnext(IntPtr handle, [Out, MarshalAs(UnmanagedType.LPStruct)]NAMEINFO name_info);

    [DllImport("Ncprog.dll", EntryPoint="fbin_findclose")]
    public static extern int fbin_findclose(IntPtr handle);

    [DllImport("Ncprog.dll", EntryPoint="fbin_open_program", CharSet = CharSet.Ansi)]
    public static extern int fbin_open_program([In, MarshalAs(UnmanagedType.LPStr)]string name, out IntPtr handle);

    [DllImport("Ncprog.dll", EntryPoint="fbin_close_program")]
    public static extern int fbin_close_program(IntPtr handle);

    [DllImport("Ncprog.dll", EntryPoint="fbin_seek")]
    public static extern int fbin_seek(IntPtr handle, int offset, int origin);

    [DllImport("Ncprog.dll", EntryPoint="fbin_tell")]
    public static extern int fbin_tell(IntPtr handle, out int offset);

    [DllImport("Ncprog.dll", EntryPoint="fbin_tell2")]
    public static extern int fbin_tell2(IntPtr handle, [Out, MarshalAs(UnmanagedType.LPArray , SizeParamIndex=1)] uint[] progid);

    [DllImport("Ncprog.dll", EntryPoint="fbin_read", CharSet = CharSet.Ansi)]
    public static extern int fbin_read(IntPtr handle, StringBuilder buffer, out int size);

    [DllImport("Ncprog.dll", EntryPoint="fbin_write")]
    public static extern int fbin_write(IntPtr handle, byte[] buffer, out int size);

    [DllImport("Ncprog.dll", EntryPoint="fbin_write2")]
    public static extern int fbin_write2(IntPtr handle, int length, byte[] buffer, out int size);

    [DllImport("Ncprog.dll", EntryPoint="fbin_get_program_entry")]
    public static extern int fbin_get_program_entry(IntPtr handle, out int entry);

    [DllImport("Ncprog.dll", EntryPoint="fbin_condense", CharSet = CharSet.Ansi)]
    public static extern int fbin_condense([In, MarshalAs(UnmanagedType.LPStr)]string name);

    [DllImport("Ncprog.dll", EntryPoint="fbin_get_progress")]
    public static extern int fbin_get_progress(out int progress, out int error);

    [DllImport("Ncprog.dll", EntryPoint="fbin_get_state")]
    public static extern int fbin_get_state(out int state, out int error);

    [DllImport("Ncprog.dll", EntryPoint="fbin_cancel")]
    public static extern int fbin_cancel();

    [DllImport("Ncprog.dll", EntryPoint="fbin_flush_program")]
    public static extern int fbin_flush_program(IntPtr handle);
}



