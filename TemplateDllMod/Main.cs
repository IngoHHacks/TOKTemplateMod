using HarmonyLib;
using TOKModdingAPI;

using static TemplateDllMod.Main;

namespace TemplateDllMod
{
    public class Main : TOKMod
    {
        public static Main Instance { get; private set; }

        public override TOKModInfo Init() {
            Instance = this;
            var harmony = new Harmony("ingoh.ouroborosking.template");
            harmony.PatchAll();
            LogInfo("Hello from the template mod!");
            return new TOKModInfo("Template Mod", "ingoh.ouroborosking.template", "IngoH", "1.0.0");
        }
        
        public override void LoadContent()
        {
            var piece = PieceAPI.BeginBuildPiece("testing", "test4")
                .WithMovement(Pieces.All["queen"].Movement)
                .WithSprites("kingd_w.png", "kingd_b.png", "king_outline.png")
                .WithDescription("Fake King", "This is a test piece.", "A queen disguised as a king.")
                .WithValue(1)
                .WithPrice(0)
                .WithPlayerProb(1000)
                .WithEnemyProbs((0, 5), (1, 10), (2, 15), (3, 20), (4, 20));
                
            piece.BuildPiece();
            
            var piece2 = PieceAPI.BeginBuildPiece("TOK", "king");
            piece2.WithMovement(Pieces.All["queen"].Movement);
            piece2.BuildOverride();
        }
    }

    [HarmonyPatch]
    public class Patcher
    {
        [HarmonyPatch(typeof(ModManager), nameof(ModManager.LoadMods))]
        [HarmonyPostfix]
        public static void TemplatePatch()
        {
            Instance.LogInfo("The patcher is working!");
        }
    }
}