import sys
def calc_cache_pos(strings, indexes):
    factor = 1
    pos = 0
    for s, i in zip(strings, indexes):
        pos += i * factor
        factor *= len(s)
    return pos

	
def lcs_back(strings, indexes, cache):
    if -1 in indexes:
        return ""
    match = all(strings[0][indexes[0]] == s[i]
                for s, i in zip(strings, indexes))
    if match:
        new_indexes = [i - 1 for i in indexes]
        result = lcs_back(strings, new_indexes, cache) + strings[0][indexes[0]]
    else:
        substrings = [""] * len(strings)
        for n in range(len(strings)):
            if indexes[n] > 0:
                new_indexes = indexes[:]
                new_indexes[n] -= 1
                cache_pos = calc_cache_pos(strings, new_indexes)
                if cache[cache_pos] is None:
                    substrings[n] = lcs_back(strings, new_indexes, cache)
                else:
                    substrings[n] = cache[cache_pos]
        result = max(substrings, key=len)
    cache[calc_cache_pos(strings, indexes)] = result
    return result


def lcs(strings):
    if len(strings) == 0:
        return ""
    elif len(strings) == 1:
        return strings[0]
    else:
        cache_size = 1
        for s in strings:
            cache_size *= len(s)
        cache = [None] * cache_size
        indexes = [len(s) - 1 for s in strings]
        return lcs_back(strings, indexes, cache)

def main():
	s=sys.argv[1:]
    contents = s.split(" ")
    result = lcs(contents)
    print result

if __name__ == "__main__":
    main()