using UnityEngine;

using UnityEditor;

public class TestSaveSprite

{

    [MenuItem("Tools/导出精灵")]

    static void SaveSprite()

    {

        int num = 0;

        string resourcesPath = "Assets/Resources/";

        if (Selection.objects.Length == 0)

        {

            Debug.LogError("Please Select Picture");

            return;

        }

        foreach (Object obj in Selection.objects)

        {

            string selectionPath = AssetDatabase.GetAssetPath(obj);

            // 必须最上级是"Assets/Resources/"

            if (!selectionPath.StartsWith(resourcesPath))

            {

                continue;

            }

            string selectionExt = System.IO.Path.GetExtension(selectionPath);

            if (selectionExt.Length == 0)

            {

                continue;

            }

            // 从路径"Assets/Resources/UI/testUI.png"得到路径"UI/testUI"

            string loadPath = selectionPath.Remove(selectionPath.Length - selectionExt.Length);

            loadPath = loadPath.Substring(resourcesPath.Length);

            // 加载此文件下的所有资源

            Sprite[] sprites = Resources.LoadAll<Sprite>(loadPath);

            if (sprites.Length == 0)

            {

                continue;

            }

            // 创建导出文件夹

            string outPath = Application.dataPath + "/outSprite/" + loadPath;

            System.IO.Directory.CreateDirectory(outPath);

            foreach (Sprite sprite in sprites)

            {

                // 创建单独的纹理

                Texture2D myimage = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);

                //abc_0:(x:2.00, y:400.00, width:103.00, height:112.00)

                for (int y = (int)sprite.rect.y; y < sprite.rect.y + sprite.rect.height; y++)//Y轴像素

                {

                    for (int x = (int)sprite.rect.x; x < sprite.rect.x + sprite.rect.width; x++)

                        myimage.SetPixel(x - (int)sprite.rect.x, y - (int)sprite.rect.y, sprite.texture.GetPixel(x, y));

                }

                //转换纹理到EncodeToPNG兼容格式

                if (myimage.format != TextureFormat.ARGB32 && myimage.format != TextureFormat.RGB24)

                {

                    Texture2D newTexture = new Texture2D(myimage.width, myimage.height);

                    newTexture.SetPixels(myimage.GetPixels(0), 0);

                    myimage = newTexture;

                }

                myimage.alphaIsTransparency = true;

                System.IO.File.WriteAllBytes(outPath + "/" + sprite.name + ".png", myimage.EncodeToPNG());

                num++;

            }

            Debug.Log("SaveSprite to " + outPath);

        }

        Debug.Log("SaveSprite Finished  Export Num = " + num);

    }

}
