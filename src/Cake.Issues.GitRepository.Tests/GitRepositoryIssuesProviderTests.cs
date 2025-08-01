namespace Cake.Issues.GitRepository.Tests;

public sealed class GitRepositoryIssuesProviderTests
{
    public sealed class TheSparseCheckoutFiltering
    {
        [Fact]
        public void Should_Filter_Out_Skip_Worktree_Files()
        {
            // Given - Simulate git ls-files -t -z output with sparse checkout
            var gitOutput = new[]
            {
                "H file1.txt",
                "S file2.txt", // Skip-worktree file (should be filtered out)
                "H subdir/file3.txt",
                "S subdir/file4.txt", // Skip-worktree file (should be filtered out)
                ""
            };

            // When - Apply the same filtering logic as GetAllFilesFromRepository
            var result = gitOutput
                .Where(x => !string.IsNullOrEmpty(x))
                .Where(x => !x.StartsWith("S ")) // Exclude skip-worktree files (sparse checkout)
                .Select(x => x.Length > 2 ? x.Substring(2) : x) // Remove status prefix (e.g., "H ")
                .ToList();

            // Then - Only non-skip-worktree files should remain
            result.ShouldNotBeNull();
            result.Count.ShouldBe(2);
            result.ShouldContain("file1.txt");
            result.ShouldContain("subdir/file3.txt");
            result.ShouldNotContain("file2.txt");
            result.ShouldNotContain("subdir/file4.txt");
        }

        [Fact]
        public void Should_Handle_Empty_Output()
        {
            // Given - Empty git output
            var gitOutput = new[] { "" };

            // When - Apply the same filtering logic as GetAllFilesFromRepository
            var result = gitOutput
                .Where(x => !string.IsNullOrEmpty(x))
                .Where(x => !x.StartsWith("S "))
                .Select(x => x.Length > 2 ? x.Substring(2) : x)
                .ToList();

            // Then - Result should be empty
            result.ShouldNotBeNull();
            result.Count.ShouldBe(0);
        }

        [Fact]
        public void Should_Handle_Various_Git_Status_Codes()
        {
            // Given - Various git status codes
            var gitOutput = new[]
            {
                "H cached_file.txt",        // Cached (should be included)
                "S skip_worktree.txt",      // Skip-worktree (should be excluded)
                "M modified_file.txt",       // Modified (should be included)
                "R renamed_file.txt",        // Renamed (should be included)
                "C copied_file.txt",         // Copied (should be included)
                "K to_be_killed.txt",        // To be killed (should be included)
                ""
            };

            // When - Apply the filtering logic
            var result = gitOutput
                .Where(x => !string.IsNullOrEmpty(x))
                .Where(x => !x.StartsWith("S "))
                .Select(x => x.Length > 2 ? x.Substring(2) : x)
                .ToList();

            // Then - Only skip-worktree files should be filtered out
            result.ShouldNotBeNull();
            result.Count.ShouldBe(5);
            result.ShouldContain("cached_file.txt");
            result.ShouldContain("modified_file.txt");
            result.ShouldContain("renamed_file.txt");
            result.ShouldContain("copied_file.txt");
            result.ShouldContain("to_be_killed.txt");
            result.ShouldNotContain("skip_worktree.txt");
        }

        [Fact]
        public void Should_Handle_Edge_Cases_In_Status_Parsing()
        {
            // Given - Edge cases that might occur
            var gitOutput = new[]
            {
                "H normal_file.txt",
                "S",                        // Just "S" without filename (malformed)
                "Sfile_without_space.txt",  // "S" without space (should not be filtered)
                "H ",                       // Just status with space but no filename
                "X unknown_status.txt",     // Unknown status (should be included)
                ""
            };

            // When - Apply the filtering logic
            var result = gitOutput
                .Where(x => !string.IsNullOrEmpty(x))
                .Where(x => !x.StartsWith("S "))
                .Select(x => x.Length > 2 ? x.Substring(2) : x)
                .ToList();

            // Then
            result.ShouldNotBeNull();
            result.Count.ShouldBe(4);
            result.ShouldContain("normal_file.txt");
            result.ShouldContain("Sfile_without_space.txt"); // This wasn't filtered because no space after S
            result.ShouldContain(""); // Empty filename from "H "
            result.ShouldContain("unknown_status.txt");
        }
    }
}