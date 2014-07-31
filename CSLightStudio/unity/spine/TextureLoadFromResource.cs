using UnityEngine;
class TextureLoadFromResource : Spine.TextureLoader
{
    string basepath;

    public TextureLoadFromResource(string basepath)
    {
        this.basepath = basepath;
    }
    public void Load(Spine.AtlasPage page, string path)
    {
        Debug.Log("load page:" + page.name);
        var mat = new Material(Shader.Find("Spine/Skeleton"));
        int right = path.LastIndexOf('.');
        path = path.Substring(0, right);
        string file = System.IO.Path.Combine(basepath, path);
        Debug.Log("Load pic:" + file + "," + path);
        mat.mainTexture = Resources.Load(file) as Texture2D;
        page.rendererObject = mat;
        page.width = mat.mainTexture.width;
        page.height = mat.mainTexture.height;
    }

    public void Unload(object mat)
    {
        GameObject.Destroy(mat as Material);
    }
}
