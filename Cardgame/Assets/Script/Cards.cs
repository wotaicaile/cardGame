using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 数据库表格的映射
/// </summary>
public class Cards
{
    private string Id;
    private string Name;
    private string Description;
    private string ImagePath;
    private string Detail;
    private string Value;

    private Cards()
    {


    }

    private Cards(string Id, string Name, string Description, string ImagePath, string Detail, string Value)
    {
        this.Id = Id;
        this.Name = Name;
        this.Description = Description;
        this.ImagePath = ImagePath;
        this.Detail = Detail;
        this.Value = Value;

    }

    

    public static Cards getCard(string Id, string Name, string Description, string ImagePath, string Detail, string Value)
    {
        return new Cards(Id, Name, Description, ImagePath, Detail,Value);
    }

    public static Cards getBookNull()
    {
        return new Cards();
    }


    public override string ToString()
    {
        return "Books: "+ Id +","+ Name + "," + Description + ","+ ImagePath+" "+ Detail;
    }


    public void setId(string Id)
    {
        this.Id = Id;
    }
    public string getId()
    {
        return this.Id;
    }
    public void setName(string Name)
    {
        this.Name = Name;
    }

    public string getName()
    {
        return this.Name;
    }

    public void setDescription(string Description)
    {
        this.Description = Description;
    }

    public string getDescription()
    {
        return this.Description;
    }

    public void setImagePath(string ImagePath)
    {
        this.ImagePath = ImagePath;

    }
    public string getImagePath()
    {
        return this.ImagePath;
    }

    public string getDetail()
    {
        return this.Detail;
    }

    public string getValue()
    {
        return this.Value;
    }
}
