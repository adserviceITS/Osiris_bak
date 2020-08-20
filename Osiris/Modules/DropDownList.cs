using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Osiris.Modules
{
    public class DropDownList
    {
        // ASUS様指示
        public List<CombInstruction> GetDropDownListInstruction()
        {
            DSNLibrary dsnLib = new DSNLibrary();
            StringBuilder stbSql = new StringBuilder();

            stbSql.Append("SELECT ");
            stbSql.Append("    M_INSTRUCTION.INSTRUCTION_ID, ");
            stbSql.Append("    M_INSTRUCTION.INSTRUCTION ");
            stbSql.Append("FROM ");
            stbSql.Append("    M_INSTRUCTION ");
            stbSql.Append("ORDER BY ");
            stbSql.Append("    M_INSTRUCTION.INSTRUCTION_ID ");

            dsnLib.ExecSQLRead(stbSql.ToString());

            List<CombInstruction> DropDownListInstruction = new List<CombInstruction>();

            while (dsnLib.Reader.Read())
            {
                DropDownListInstruction.Add(new CombInstruction
                {
                    InstructionID = dsnLib.Reader["INSTRUCTION_ID"].ToString(),
                    Instruction = dsnLib.Reader["INSTRUCTION"].ToString()
                });
            }
            dsnLib.DB_Close();

            return DropDownListInstruction;
        }
    }

    public class CombInstruction
    {
        public string InstructionID { get; set; }
        public string Instruction { get; set; }
    }

    public class CombLine
    {
        public string LineID { get; set; }
        public string LineName { get; set; }
    }

    public class CombSerialStatus
    {
        public string SerialStatusID { get; set; }
        public string SerialStatusName { get; set; }
    }

    public class CombAuthorityName
    {
        public string ID { get; set; }
        public string AuthorityName { get; set; }
    }

}
