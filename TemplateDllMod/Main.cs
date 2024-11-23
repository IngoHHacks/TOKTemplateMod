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
            var piece = PieceAPI.BeginBuildPiece("testing", "test4");
            piece.WithMovement(Pieces.All["queen"].Movement);
            piece.WithSprites("kingd_w.png", "kingd_b.png", "king_outline.png");
            piece.WithDescription("Fake King", "This is a test piece.", "A queen disguised as a king.");
            piece.WithValue(1);
            piece.WithPrice(0);
            piece.WithPlayerProb(1000);
            piece.WithEnemyProbs((0, 1000));
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