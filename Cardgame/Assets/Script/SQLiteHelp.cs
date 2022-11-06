using UnityEngine;
using Mono.Data.Sqlite;
using System;


///<summary>
///���ݿ⸨����
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

        //�����ݿ�
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

        //������
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

        //�������ݿ�
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

        //ִ��sqlQuery���� 
        public SqliteDataReader ExecuteQuery(string sqlQuery)
        {
            dbCommand = dbConnection.CreateCommand();
            dbCommand.CommandText = sqlQuery;
            dbReader = dbCommand.ExecuteReader();
         
            return dbReader;
        }

        //��ѯCards,�ѽ������dictionary
        public void LoadBookList(SqliteDataReader dbReader)
        {
            try
            {
                while (dbReader.Read())
                {

                    //�����ݿ��е����ݷ�װ��Cards����
                    //Books(string Id, string Name, string Description, string ImagePath, string Detail)
                    Cards cards = Cards.getCard(dbReader["Id"].ToString(), dbReader["Name"].ToString(), dbReader["Description"].ToString(), dbReader["ImagePath"].ToString(), dbReader["Detail"].ToString(), dbReader["Value"].ToString());
                    //Debug.Log(book);

                    //�����ֵ�,�������ʹ���鼮��Ϣ
                    string key = cards.getId();
                    DataStructure.cardsDictionary.Add(key, cards);

                }
            }
            catch
            {
                Debug.Log("�鼮�б��Ѽ������");
                ////ͨ�����ļ��ϱ������ͼƬ·��
                foreach (string key in DataStructure.cardsDictionary.Keys)

                {
                    Debug.Log(DataStructure.cardsDictionary[key].getImagePath());

                }
            }
        }
      

        //��ȡ�������
        public Cards PrintRandomBookResult(SqliteDataReader dbReader)
        {

            Debug.Log("�������" + DataStructure.cardsDictionary.Count);
            string random = Common.getRandomNumber(DataStructure.cardsDictionary.Count).ToString();
            //Debug.Log("�������" + random);
            dbReader.Close();

            return DataStructure.cardsDictionary[random];

        }

        //��ѯ�Ѿ���õ�books,PlayerbooksDictionary
        public void LoadObtainedBookList(SqliteDataReader dbReader)
        {
            Debug.Log("11111111111111111111111111111111111111111111111");

            // try
            // {
            while (dbReader.Read())
                {
                Debug.Log("2222222222222222222222222222222222");

                //�����ݿ��е����ݷ�װ��book����
                //Books(string Id, string Name, string Description, string ImagePath, string Detail)
                Cards cards = Cards.getCard(dbReader["Id"].ToString(), dbReader["Name"].ToString(), dbReader["Description"].ToString(), dbReader["ImagePath"].ToString(), dbReader["Detail"].ToString(), dbReader["Value"].ToString());
                Debug.Log("333333333333333333333333333333333");

                //�����û��ѻ�õ��ֵ�,�������ʹ���鼮��Ϣ
                string key = cards.getId();
                DataStructure.PlayerCardsDictionary.Add(key, cards);

            }
            //}

           //catch (Exception ex)
            //{
                //Debug.Log(ex.Message);
                //Debug.Log("Connect fail!");
                //Debug.Log("�ѻ���鼮�б��Ѽ������");
                ////ͨ�����ļ��ϱ������ͼƬ·��
                foreach (string key in DataStructure.cardsDictionary.Keys)

                {
                    Debug.Log(DataStructure.cardsDictionary[key].getImagePath());

                }
            //}

        }


        //�������������
        public object PrintCoinCount()
        {
            return dbReader["Count"];
        }



        //��������
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

        //���ұ�����������
        public SqliteDataReader ReadFullTable(string tableName)
        {
            string query = "SELECT * FROM " + tableName;
            Debug.Log(query);

            return ExecuteQuery(query);
        }

        //���ұ���ָ������
        public SqliteDataReader ReadSpecificData(string tableName, string selectkey, string selectvalue)
        {
            string query = "SELECT * FROM " + tableName + " where " + selectkey + " = " + selectvalue + " ";

            return ExecuteQuery(query);
        }

        //���ұ���һ������
        public SqliteDataReader ReadOneData(string selectName, string tableName)
        {
            string query = "SELECT " + selectName + " FROM " + tableName;
            return ExecuteQuery(query);
        }

        //��������  SQL�﷨��UPDATE table_name SET column1 = value1, column2 = value2....columnN = valueN[WHERE  CONDITION];
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



        //����һ������  SQL�﷨��UPDATE table_name SET column1 = value1, column2 = value2....columnN = valueN[WHERE  CONDITION];
        public SqliteDataReader UpdateOneInto(string tableName, string cols,int value,string selectkey, string selectvalue)
        {
            
            string query = "UPDATE " + tableName + " SET " + cols + "="+ value;

            query += " WHERE " + selectkey + " = " + selectvalue + " ";

           
            return ExecuteQuery(query);
        }


        //ɾ�����е�����  DELETE FROM table_name  WHERE  {CONDITION or CONDITION}(ɾ�����з������������ݣ�
        public SqliteDataReader Delete(string tableName, string[] cols, string[] colsvalues)
        {
            string query = "DELETE FROM " + tableName + " WHERE " + cols[0] + " = " + colsvalues[0];

            for (int i = 1; i < colsvalues.Length; ++i)
            {
                query += " or " + cols[i] + " = " + colsvalues[i];
            }

            return ExecuteQuery(query);
        }

        //����ָ��������
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

        //�ж���ָ���������Ƿ���������ֵ
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