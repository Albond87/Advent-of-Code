public class CalculatorAOC // For Advent of Code 2020
{
    static int version = 2; // 1 for part 1, 2 for part 2

    public static double evaluate(String expression)
    {
        String[] args = expression.split(" ");
        return evaluate(args, 0, 0);
    }

    private static double evaluate(String[] tokens, int startIndex, int bracketLevel)
    {
        double[] operands = new double[3];
        int operandCount = 0;
        char[] operators = new char[2];
        int operatorCount = 0;
        int stopIndex = tokens.length;

        for (int n = startIndex; n < stopIndex; n++)
        {
            if (tokens[n].equals("+") || tokens[n].equals("*"))
            {
                operators[operatorCount] = tokens[n].charAt(0);
                operatorCount++;
            }
            else if (tokens[n].charAt(0) == '(')
            {
                tokens[n] = tokens[n].substring(1);
                operands[operandCount] = evaluate(tokens, n, bracketLevel+1);
                operandCount++;

                while (tokens[n].charAt(0) != '@') n++;
                if (tokens[n].length() > 1)
                {
                    tokens[n] = tokens[n].substring(0,tokens[n].length()-1);
                    stopIndex = n;
                }
                else
                {
                    tokens[n] = "#";
                }
            }
            else
            {
                if (tokens[n].charAt(tokens[n].length()-1) == ')')
                {
                    String token = tokens[n];
                    int numLength = 0;
                    while (token.charAt(numLength) != ')')
                    {
                        numLength++;
                    }
                    String strip = token.substring(0,numLength);
                    operands[operandCount] = Double.parseDouble(strip);
                    operandCount++;
                    stopIndex = n;

                    strip = "@" + token.substring(numLength,token.length()-1);
                    tokens[n] = strip;
                }
                else
                {
                    operands[operandCount] = Double.parseDouble(tokens[n]);
                    operandCount++;
                }
            }
            if (operandCount == 3)
            {
                if (version == 2 && operators[0] == '*' && operators[1] == '+')
                {
                    operands[1] = operands[1] + operands[2];
                }
                else
                {
                    switch (operators[0])
                    {
                        case '+':
                            operands[0] = operands[0] + operands[1];
                            break;

                        case '*':
                            operands[0] = operands[0] * operands[1];
                            break;
                    }
                    operands[1] = operands[2];
                    operators[0] = operators[1];
                }
                operandCount--;
                operatorCount--;
            }
        }

        switch (operators[0])
        {
            case '+':
                return operands[0] + operands[1];

            case '*':
                return operands[0] * operands[1];
        }

        return 0;
    }
}