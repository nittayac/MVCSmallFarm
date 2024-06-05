using Microsoft.Extensions.FileProviders;


namespace MVCSmallFarm.ViewModels;
public class DefaultValue
{
    public static string DefaultImg {
        get
        {

            //  return Url.Content("~/img/" + "productnopic.png");
            var filepath = new PhysicalFileProvider(
                       Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img")
                       ).Root + $@"\{"productnopic.png"}";


            return filepath;
        } 
    }
}
