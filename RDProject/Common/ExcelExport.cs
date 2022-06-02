using RDProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;
using System.Windows;
using System.Collections.ObjectModel;

namespace RDProject.Common
{
    public static class ExcelExport
    {
        public enum ExportType
        {
            Trial,
            TrialEntry,
            TrialAndEntry,
            Manpower,
            ManpowerEntry,
            ManpowerAndEntry,
        }

        public static void Do(ExportType type, List<Object> list, string FileName)
        {
            IWorkbook workBook = null;
            FileStream fileStream = null;
            

            switch (type)
            {
                case ExportType.TrialAndEntry:
                    var Trial = list[0] as Trial;
                    var TrialEntries = list[1] as ObservableCollection<TrialEntry>;

                    string[] trialTitle = { "日期", "单据编号", "研发项目编号", "产品名称", "工单信息", "公司名称", "是否CNC", "是否喷涂", "是否镭雕", "是否组装", "CNC工序NPI", "喷涂工序NPI", "镭雕工序NPI", "组装工序NPI", "組裝车间", "基本信息", "制作要求", "表单创建人", "创建时间","标题" };
                    string[] trialEntryTitle = { "工单号", "工站", "工序", "投入时间", "结束时间", "投入数量", "投入人力", "投入工时" };
                    try
                    {
                        fileStream = new FileStream(FileName, FileMode.Create, FileAccess.ReadWrite);
                        workBook = new XSSFWorkbook();
                        ISheet sheet = workBook.CreateSheet("Sheet1");
                        if (Trial != null)
                        {
                            IRow row0 = sheet.CreateRow(0);
                            for (var i = 0; i < trialTitle.Length; i++)
                            {
                                row0.CreateCell(i).SetCellValue(trialTitle[i]);
                            }

                            IRow row1 = sheet.CreateRow(1);
                            row1.CreateCell(0).SetCellValue(Trial.FCreateDate.ToString("G"));
                            row1.CreateCell(1).SetCellValue(Trial.FBillNo);
                            row1.CreateCell(2).SetCellValue(Trial.FRDNo);
                            row1.CreateCell(3).SetCellValue(Trial.FProductName);
                            row1.CreateCell(4).SetCellValue(Trial.FWorkerOrderDescription);
                            row1.CreateCell(5).SetCellValue(Trial.FCompany);
                            row1.CreateCell(6).SetCellValue(Trial.FHasCNC ? "是" : "否");
                            row1.CreateCell(7).SetCellValue(Trial.FHasCoating ? "是" : "否");
                            row1.CreateCell(8).SetCellValue(Trial.FHasLaser ? "是" : "否");
                            row1.CreateCell(9).SetCellValue(Trial.FHasAssembly ? "是" : "否");
                            row1.CreateCell(10).SetCellValue(Trial.FCNCNPI);
                            row1.CreateCell(11).SetCellValue(Trial.FCoatingNPI);
                            row1.CreateCell(12).SetCellValue(Trial.FLaserNPI);
                            row1.CreateCell(13).SetCellValue(Trial.FAssemblyNPI);
                            row1.CreateCell(14).SetCellValue(Trial.FAssemblyFactory);
                            row1.CreateCell(15).SetCellValue(Trial.FInformation);
                            row1.CreateCell(16).SetCellValue(Trial.FRequire);
                            row1.CreateCell(17).SetCellValue(Trial.FCreateUser);
                            row1.CreateCell(18).SetCellValue(Trial.FCreateDate.ToString("G"));
                            row1.CreateCell(19).SetCellValue(Trial.FTitle);

                            IRow row2 = sheet.CreateRow(2);
                            for (var i = 0; i < trialEntryTitle.Length; i++)
                            {
                                row2.CreateCell(i).SetCellValue(trialEntryTitle[i]);
                            }
                            
                            if(TrialEntries != null)
                            {
                                for (var i = 0; i < TrialEntries.Count; i++)
                                {
                                    IRow row3 = sheet.CreateRow(3 + i);
                                    row3.CreateCell(0).SetCellValue(TrialEntries[i].FWorkOrder);
                                    row3.CreateCell(1).SetCellValue(TrialEntries[i].FStation);
                                    row3.CreateCell(2).SetCellValue(TrialEntries[i].FProcessName);
                                    row3.CreateCell(3).SetCellValue(TrialEntries[i].FBeginDate.ToString("G"));
                                    row3.CreateCell(4).SetCellValue(TrialEntries[i].FEndDate.ToString("G"));
                                    row3.CreateCell(5).SetCellValue(TrialEntries[i].FAmount.ToString());
                                    row3.CreateCell(6).SetCellValue(TrialEntries[i].FManPower.ToString());
                                    row3.CreateCell(7).SetCellValue(TrialEntries[i].FManHours.ToString());
                                }
                            }

                            workBook.Write(fileStream);
                        }
                    }
                    catch(Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }
                    finally
                    {
                        if(fileStream != null)
                        {
                            fileStream.Close();
                        }
                        if(workBook != null)
                        {
                            workBook.Close();
                        }
                    }
                    
                    break;
                case ExportType.ManpowerAndEntry:
                    var Manpower = list[0] as Manpower;
                    var ManpowerEntries = list[1] as ObservableCollection<ManpowerEntry>;

                    string[] manpowerTitle = {"日期", "单位", "创建人", "标题" };
                    string[] ManpowerEntryTitle = { "创建时间", "工号", "部门", "员工姓名", "出勤工时", "平时加班", "休息加班", "法定加班", "总工时", "RD28工时", "RD30工时", "RD31工时", "RD32工时", "RD33工时", "RD34工时", "工时差异" };

                    try
                    {
                        fileStream = new FileStream(FileName, FileMode.Create, FileAccess.ReadWrite);
                        workBook = new XSSFWorkbook();
                        ISheet sheet = workBook.CreateSheet("Sheet1");

                        if(Manpower != null)
                        {
                            IRow row0 = sheet.CreateRow(0);
                            for (var i = 0; i < manpowerTitle.Length; i++)
                            {
                                row0.CreateCell(i).SetCellValue(manpowerTitle[i]);
                            }

                            IRow row1 = sheet.CreateRow(1);
                            row1.CreateCell(0).SetCellValue(Manpower.FCreateDate.ToString("G"));
                            row1.CreateCell(1).SetCellValue(Manpower.FCompany);
                            row1.CreateCell(2).SetCellValue(Manpower.FCreateUser);
                            row1.CreateCell(3).SetCellValue(Manpower.FTitle);

                            IRow row2 = sheet.CreateRow(2);
                            for (var i = 0; i < ManpowerEntryTitle.Length; i++)
                            {
                                row2.CreateCell(i).SetCellValue(ManpowerEntryTitle[i]);
                            }

                            if(ManpowerEntries != null)
                            {
                                for(var i = 0; i < ManpowerEntries.Count; i++)
                                {
                                    IRow row3 = sheet.CreateRow(3 + i);
                                    row3.CreateCell(0).SetCellValue(ManpowerEntries[i].FCreateDate.ToString("G"));
                                    row3.CreateCell(1).SetCellValue(ManpowerEntries[i].FEmpId);
                                    row3.CreateCell(2).SetCellValue(ManpowerEntries[i].FDeptName);
                                    row3.CreateCell(3).SetCellValue(ManpowerEntries[i].FEmpName);
                                    row3.CreateCell(4).SetCellValue(ManpowerEntries[i].FAttendanceHours.ToString());
                                    row3.CreateCell(5).SetCellValue(ManpowerEntries[i].FNormalOvertimeHours.ToString());
                                    row3.CreateCell(6).SetCellValue(ManpowerEntries[i].FWeekendOvertimeHours.ToString());
                                    row3.CreateCell(7).SetCellValue(ManpowerEntries[i].FHolidayOvertimeHours.ToString());
                                    row3.CreateCell(8).SetCellValue(ManpowerEntries[i].FTotalHours.ToString());
                                    row3.CreateCell(9).SetCellValue(ManpowerEntries[i].FRD28Hours.ToString());
                                    row3.CreateCell(10).SetCellValue(ManpowerEntries[i].FRD30Hours.ToString());
                                    row3.CreateCell(11).SetCellValue(ManpowerEntries[i].FRD31Hours.ToString());
                                    row3.CreateCell(12).SetCellValue(ManpowerEntries[i].FRD32Hours.ToString());
                                    row3.CreateCell(13).SetCellValue(ManpowerEntries[i].FRD33Hours.ToString());
                                    row3.CreateCell(14).SetCellValue(ManpowerEntries[i].FRD34Hours.ToString());
                                    row3.CreateCell(15).SetCellValue(ManpowerEntries[i].FVarianceHours.ToString());
                                }
                            }

                            workBook.Write(fileStream);
                        }
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }
                    finally
                    {
                        if (fileStream != null)
                        {
                            fileStream.Close();
                        }
                        if (workBook != null)
                        {
                            workBook.Close();
                        }
                    }
                    break;
                default:
                    break;
            }
        }


    }
}
