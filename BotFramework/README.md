# Requisitos

Template para projeto do Bot Framework no Visual Studio
https://bit.ly/vivaoie

Bot Emulator
https://bit.ly/morrachrome

# Passos

> Colar o template em Documentos/Visual Studio 2017/Templates/ProjectTemplates/Visual C#/
> Visual Studio > New > Project > Search "Bot Application"
> Code, code, code!
> F5! F5! F5!

## FormFlow

> Adiciona o Model
    ```csharp
    [Serializable]
    public class Pedido{
        public string Nome {get; set;}
        public string Endereco {get; set;}
        public Sabores Sabores {get; set;}

        public static IForm<Pizza> BuildForm(){
            return new FormBuilder<Pizza>().Message("Realize o pedido").Build();
        }
    }
    public enum Sabores{
        Bacon,
        Portuguesa,
        Calabresa
    }
    ```
> Chame o formulário quando for preciso (Na Controller)

    ```csharp
    internal static IDialog<Pizza> MakeRoot(){
        return Chain.From(() => FormDialog.FromForm(Pizza.BuildForm));
    }
    ```
> Chame o `MakeRoot()` quando quiser o formulário

## LUIS

> Adicionar o Luis Model

    ```csharp
    [Serializable]
    [LuisModel("AppId", "Key")]
    public Class RootLuisDialog : LuisDialog<object>
    ```

> Adicionar a Intent pra cada diálogo

    ```csharp
    [LuisIntent("Pedido")]
    public async Task Pedido(IDialogContext context, LuisResult result){
        await context.PostAsync("Resposta");
        context.Wait(this.MessageReceived);
    }
    ```