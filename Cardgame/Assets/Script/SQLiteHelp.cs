using UnityEngine;
using Mono.Data.Sqlite;
using System;


///<summary>
///数据库辅助类
///</summary>
///

namespace DataBaseUtils
{
    public class SQLiteHelp
    {
        private SqliteConnection dbConnection;
        private SqliteCommand dbCommand;
        private SqliteDataReader dbReader;

        public SQLiteHelp(string conStr)
        {
            OpenSQLite(conStr);
        }

        //打开数据库
        public void OpenSQLite(string conStr)
        {
            try
            {
                dbConnection = new SqliteConnection(conStr);
                dbConnection.Open();
                Debug.Log("Connect successful!");
               

            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
                Debug.Log("Connect fail!");

            }
        }

        //创建表
        public SqliteDataReader CreateTable(string tabName, string[] col, string[] colType)
        {
            if (col.Length != colType.Length)
            {
                throw new SqliteException("columns.Length != colType.Length");
            }

            string query = "CREATE TABLE " + tabName + " (" + col[0] + " " + colType[0];

            for (int i = 1; i < col.Length; ++i)
            {
                query += ", " + col[i] + " " + colType[i];
            }

            query += ")";

            return ExecuteQuery(query);
        }

        //连接数据库
        public void CloseSqlConnection()

        {
            if (dbCommand != null)
            {
                dbCommand.Dispose();
            }
            dbCommand = null;

            if (dbReader != null)
            {
                dbReader.Dispose();
            }
            dbReader = null;

            if (dbConnection != null)
            {
                dbConnection.Close();
            }
            dbConnection = null;

            Debug.Log("Disconnected from db.");
        }

        //执行sqlQuery操作 
        public SqliteDataReader ExecuteQuery(string sqlQuery)
        {
            dbCommand = dbConnection.CreateCommand();
            dbCommand.CommandText = sqlQuery;
            dbReader = dbCommand.ExecuteReader();
         
            return dbReader;
        }

        //查询Cards,把结果存入dictionary
        public void LoadBookList(SqliteDataReader dbReader)
        {
            try
            {
                while (dbReader.Read())
                {

                    //把数据库中的内容封装成Cards对象
                    //Books(string Id, string Name, string Description, string ImagePath, string Detail)
                    Cards cards = Cards.getCard(dbReader["Id"].ToString(), dbReader["Name"].ToString(), dbReader["Description"].ToString(), dbReader["ImagePath"].ToString(), dbReader["Detail"].ToString(), dbReader["Value"].ToString());
                    //Debug.Log(book);

                    //加入字典,方便程序使用书籍信息
                    string key = cards.getId();
                    DataStructure.cardsDictionary.Add(key, cards);

                }
            }
            catch
            {
                Debug.Log("书籍列表已加载完毕");
                ////通过键的集合遍历输出图片路径
                foreach (string key in DataStructure.cardsDictionary.Keys)

                {
                    Debug.Log(DataStructure.cardsDictionary[key].getImagePath());

                }
            }
        }
      

        //获取随机卡牌
        public Cards PrintRandomBookResult(SqliteDataReader dbReader)
        {

            Debug.Log("随机数是" + DataStructure.cardsDictionary.Count);
            string random = Common.getRandomNumber(DataStructure.cardsDictionary.Count).ToString();
            //Debug.Log("随机数是" + random);
            dbReader.Close();

            return DataStructure.cardsDictionary[random];

        }

        //查询已经获得的books,PlayerbooksDictionary
        public void LoadObtainedBookList(SqliteDataReader dbReader)
        {
            Debug.Log("11111111111111111111111111111111111111111111111");

            // try
            // {
            while (dbReader.Read())
                {
                Debug.Log("2222222222222222222222222222222222");

                //把数据库中的内容封装成book对象
                //Books(string Id, string Name, string Description, string ImagePath, string Detail)
                Cards cards = Cards.getCard(dbReader["Id"].ToString(), dbReader["Name"].ToString(), dbReader["Description"].ToString(), dbReader["ImagePath"].ToString(), dbReader["Detail"].ToString(), dbReader["Value"].ToString());
                Debug.Log("333333333333333333333333333333333");

                //加入用户已获得的字典,方便程序使用书籍信息
                string key = cards.getId();
                DataStructure.PlayerCardsDictionary.Add(key, cards);

            }
            //}

           //catch (Exception ex)
            //{
                //Debug.Log(ex.Message);
                //Debug.Log("Connect fail!");
                //Debug.Log("已获得书籍列表已加载完毕");
                ////通过键的集合遍历输出图片路径
                foreach (string key in DataStructure.cardsDictionary.Keys)

                {
                    Debug.Log(DataStructure.cardsDictionary[key].getImagePath());

                }
            //}

        }


        //输出金币数量结果
        public object PrintCoinCount()
        {
            return dbReader["Count"];
        }



        //插入数据
        public SqliteDataReader InsertInto(string tableName, string[] values)
        {
            string query = "INSERT INTO " + tableName + " VALUES (" + values[0];

            for (int i = 1; i < values.Length; ++i)
            {
                query += ", " + values[i];
            }

            query += ")";

            Debug.Log(query);
            return ExecuteQuery(query);
        }

        //查找表中所有数据
        public SqliteDataReader ReadFullTable(string tableName)
        {
            string query = "SELECT * FROM " + tableName;
            Debug.Log(query);

            return ExecuteQuery(query);
        }

        //查找表中指定数据
        public SqliteDataReader ReadSpecificData(string tableName, string selectkey, string selectvalue)
        {
            string query = "SELECT * FROM " + tableName + " where " + selectkey + " = " + selectvalue + " ";

            return ExecuteQuery(query);
        }

        //查找表中一列数据
        public SqliteDataReader ReadOneData(string selectName, string tableName)
        {
            string query = "SELECT " + selectName + " FROM " + tableName;
            return ExecuteQuery(query);
        }

        //更新数据  SQL语法：UPDATE table_name SET column1 = value1, column2 = value2....columnN = valueN[WHERE  CONDITION];
        public SqliteDataReader UpdateInto(string tableName, string[] cols, string[] colsvalues, string selectkey, string selectvalue)
        {
            string query = "UPDATE " + tableName + " SET " + cols[0] + " = " + colsvalues[0];

            for (int i = 1; i < colsvalues.Length; ++i)
            {
                query += ", " + cols[i] + " =" + colsvalues[i];
            }

            query += " WHERE " + selectkey + " = " + selectvalue + " ";

            return ExecuteQuery(query);
        }



        //更新一条数据  SQL语法：UPDATE table_name SET column1 = value1, column2 = value2....columnN = valueN[WHERE  CONDITION];
        public SqliteDataReader UpdateOneInto(string tableName, string cols,int value,string selectkey, string selectvalue)
        {
            
            string query = "UPDATE " + tableName + " SET " + cols + "="+ value;

            query += " WHERE " + selectkey + " = " + selectvalue + " ";

           
            return ExecuteQuery(query);
        }


        //删除表中的内容  DELETE FROM table_name  WHERE  {CONDITION or CONDITION}(删除所有符合条件的内容）
        public SqliteDataReader Delete(string tableName, string[] cols, string[] colsvalues)
        {
            string query = "DELETE FROM " + tableName + " WHERE " + cols[0] + " = " + colsvalues[0];

            for (int i = 1; i < colsvalues.Length; ++i)
            {
                query += " or " + cols[i] + " = " + colsvalues[i];
            }

            return ExecuteQuery(query);
        }

        //插入指定的数据
        public SqliteDataReader InsertIntoSpecific(string tableName, string[] cols, string[] values)
        {
            if (cols.Length != values.Length)
            {
                throw new SqliteException("columns.Length != values.Length");
            }

            string query = "INSERT INTO " + tableName + "(" + cols[0];

            for (int i = 1; i < cols.Length; ++i)
            {
                query += ", " + cols[i];
            }

            query += ") VALUES (" + values[0];

            for (int i = 1; i < values.Length; ++i)
            {
                query += ", " + values[i];
            }

            query += ")";

            return ExecuteQuery(query);
        }

        //判断在指定列名中是否存在输入的值
        public bool ExitItem(string tableName, string itemName, string itemValue)
        {
            bool flag = false;

            dbReader = ReadFullTable(tableName);

            while (dbReader.Read())
            {
                for (int i = 0; i < dbReader.FieldCount; i++)
                {
                    if (dbReader.GetName(i) == itemName)
                    {
                        if (dbReader.GetValue(i).ToString() == itemValue)
                        {
                            flag = true;
                            break;
                        }
                    }
                }
            }

            return flag;
        }
    }
}