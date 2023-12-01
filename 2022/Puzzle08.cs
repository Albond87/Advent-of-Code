public static class Puzzle08
{
    public static int Part1(string[] lines)
    {
        int width = lines[0].Length;
        int depth = lines.Length;
        int visible = width * 2 + depth * 2 - 4;
        for (int i=1; i<depth-1; i++) {
            for (int j=1; j<width-1; j++) {
                int height = lines[i][j];
                bool blocked = false;
                for (int k=j-1; k>=0; k--) {
                    if (lines[i][k] >= height) {
                        blocked = true;
                        break;
                    }
                }
                if (!blocked) {
                    visible++;
                    continue;
                }
                blocked = false;
                for (int k=j+1; k<width; k++) {
                    if (lines[i][k] >= height) {
                        blocked = true;
                        break;
                    }
                }
                if (!blocked) {
                    visible++;
                    continue;
                }
                blocked = false;
                for (int k=i-1; k>=0; k--) {
                    if (lines[k][j] >= height) {
                        blocked = true;
                        break;
                    }
                }
                if (!blocked) {
                    visible++;
                    continue;
                }
                blocked = false;
                for (int k=i+1; k<depth; k++) {
                    if (lines[k][j] >= height) {
                        blocked = true;
                        break;
                    }
                }
                if (!blocked) {
                    visible++;
                }
            }
        }
        return visible;
    }

    public static int Part2(string[] lines)
    {
        int width = lines[0].Length;
        int depth = lines.Length;
        int maxScore = 0;
        for (int i=1; i<depth-1; i++) {
            for (int j=1; j<width-1; j++) {
                int height = lines[i][j];
                int left=0, right=0, up=0, down=0;
                for (int k=j-1; k>=0; k--) {
                    left++;
                    if (lines[i][k] >= height) {
                        break;
                    }
                }
                for (int k=j+1; k<width; k++) {
                    right++;
                    if (lines[i][k] >= height) {
                        break;
                    }
                }
                for (int k=i-1; k>=0; k--) {
                    up++;
                    if (lines[k][j] >= height) {
                        break;
                    }
                }
                for (int k=i+1; k<depth; k++) {
                    down++;
                    if (lines[k][j] >= height) {
                        break;
                    }
                }
                int scenicScore = left*right*up*down;
                if (scenicScore>maxScore) maxScore=scenicScore;
            }
        }
        return maxScore;
    }
}