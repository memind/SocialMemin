using SocialMemin.API.Consts;

namespace SocialMemin.API.Extensions
{
    public static class SecurityHeaderExtensions
    {
        public static IApplicationBuilder AddSecurity(this IApplicationBuilder app)
        {
            app.UseXContentTypeOptions();
            app.UseReferrerPolicy(options => options.NoReferrer());
            app.UseXXssProtection(options => options.EnabledWithBlockMode());
            app.UseXfo(options => options.Deny());
            app.UseCspReportOnly(options => options
                .BlockAllMixedContent()
                .StyleSources(option => option.Self().CustomSources(SecurityConsts.FontGoogle()))
                .FontSources(option => option.Self().CustomSources(SecurityConsts.FontGStatic(), SecurityConsts.Data()))
                .FormActions(option => option.Self())
                .FrameAncestors(option => option.Self())
                .ImageSources(option => option.Self().CustomSources(SecurityConsts.Blob(), SecurityConsts.Cloudinary()))
                .ScriptSources(option => option.Self()));

            return app;
        }
    }
}
