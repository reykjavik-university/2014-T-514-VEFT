def euler1(value):
    total_sum = 0
    for n in range(1, value):
        if n % 3 == 0 or n % 5 == 0:
            total_sum += n
    return total_sum


def euler1_list_comp(value):
    return sum([x for x in range(1, value)
                if x % 3 == 0 or x % 5 == 0])


print euler1_list_comp(10)
