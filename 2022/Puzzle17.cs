public static class Puzzle17
{
    public static int Part1(string jets)
    {
        int[][][] rocks = new int[][][] {
            new int[][] {
                new int[] {0,0},
                new int[] {1,0},
                new int[] {2,0},
                new int[] {3,0}
            },
            new int[][] {
                new int[] {1,0},
                new int[] {0,1},
                new int[] {2,1},
                new int[] {1,2}
            },
            new int[][] {
                new int[] {0,0},
                new int[] {1,0},
                new int[] {2,0},
                new int[] {2,1},
                new int[] {2,2}
            },
            new int[][] {
                new int[] {0,0},
                new int[] {0,1},
                new int[] {0,2},
                new int[] {0,3}
            },
            new int[][] {
                new int[] {0,0},
                new int[] {1,0},
                new int[] {0,1},
                new int[] {1,1}
            }
        };
        HashSet<int> blocks = new HashSet<int>();
        int highest = -1;
        int jet=0;
        for (long i=0; i<2022; i++) {
            int[][] rock = rocks[i%5];
            int[] position = new int[]{2,highest+4};
            bool resting=false;
            while (!resting) {
                int push = jets[jet] == '>' ? 1 : -1;
                bool blocked = false;
                foreach (int[] r in rock) {
                    int[] r2 = new int[] {r[0] + position[0] + push, r[1] + position[1]};
                    if (r2[0] < 0 || r2[0] > 6) {
                        blocked=true;
                        break;
                    }
                    if (blocks.Contains(r2[1]*7+r2[0])) {
                        blocked=true;
                        break;
                    }
                }
                if (!blocked) position[0] += push;
                jet++;
                if (jet==jets.Length) jet=0;
                foreach (int[] r in rock) {
                    int[] r2 = new int[] {r[0] + position[0], r[1] + position[1]-1};
                    if (r2[1] < 0) {
                        resting=true;
                        break;
                    }
                    if (blocks.Contains(r2[1]*7+r2[0])) {
                        resting=true;
                        break;
                    }
                }
                if (!resting) position[1]--;
            }
            foreach (int[] r in rock) {
                int[] r2 = new int[] {r[0] + position[0], r[1] + position[1]};
                if (r2[1] > highest) highest=r2[1];
                blocks.Add(r2[1]*7+r2[0]);
            }
        }
        return highest+1;
    }

    public static long Part2(string jets)
    {
        int[][][] rocks = new int[][][] {
            new int[][] {
                new int[] {0,0},
                new int[] {1,0},
                new int[] {2,0},
                new int[] {3,0}
            },
            new int[][] {
                new int[] {1,0},
                new int[] {0,1},
                new int[] {2,1},
                new int[] {1,2}
            },
            new int[][] {
                new int[] {0,0},
                new int[] {1,0},
                new int[] {2,0},
                new int[] {2,1},
                new int[] {2,2}
            },
            new int[][] {
                new int[] {0,0},
                new int[] {0,1},
                new int[] {0,2},
                new int[] {0,3}
            },
            new int[][] {
                new int[] {0,0},
                new int[] {1,0},
                new int[] {0,1},
                new int[] {1,1}
            }
        };
        long simulationLength = 1000000000000;
        HashSet<int> blocks = new HashSet<int>();
        int highest = -1;
        int jet=0;
        List<int> heights = new List<int>();
        Dictionary<string,long[]> configurations = new Dictionary<string, long[]>();
        long extraHeight=0;
        for (long i=0; i<simulationLength; i++) {
            int[][] rock = rocks[i%5];
            int[] position = new int[]{2,highest+4};
            bool resting=false;
            if (extraHeight==0) {
                string c = (i%5).ToString()+","+jet.ToString();
                IEnumerable<int> hs = heights.TakeLast(11).SkipLast(1);
                foreach (int h in hs) c += ","+(highest-h);
                if (configurations.ContainsKey(c)) {
                    long cycle = i-configurations[c][0];
                    if (cycle!=1700 && cycle!=1695) {
                        long repeats = (simulationLength-1-i)/cycle;
                        i+=repeats*cycle;
                        extraHeight = (highest-configurations[c][1])*repeats;
                    }
                } 
                else configurations[c] = new long[] {i,highest};
            }
            
            while (!resting) {                
                int push = jets[jet] == '>' ? 1 : -1;
                bool blocked = false;
                foreach (int[] r in rock) {
                    int[] r2 = new int[] {r[0] + position[0] + push, r[1] + position[1]};
                    if (r2[0] < 0 || r2[0] > 6) {
                        blocked=true;
                        break;
                    }
                    if (blocks.Contains(r2[1]*7+r2[0])) {
                        blocked=true;
                        break;
                    }
                }
                if (!blocked) position[0] += push;
                jet++;
                if (jet==jets.Length) jet=0;
                foreach (int[] r in rock) {
                    int[] r2 = new int[] {r[0] + position[0], r[1] + position[1]-1};
                    if (r2[1] < 0) {
                        resting=true;
                        break;
                    }
                    if (blocks.Contains(r2[1]*7+r2[0])) {
                        resting=true;
                        break;
                    }
                }
                if (!resting) position[1]--;
            }
            foreach (int[] r in rock) {
                int[] r2 = new int[] {r[0] + position[0], r[1] + position[1]};
                if (r2[1] > highest) highest=r2[1];
                blocks.Add(r2[1]*7+r2[0]);
            }
            heights.Add(highest);
        }
        return highest+1+extraHeight;
    }
}